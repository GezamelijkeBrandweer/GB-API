using System.Globalization;
using System.Text.Json.Nodes;
using GB_API.Server.Domain.Traffic;
using MIC_RequestSender;
using MIC_RequestSender.Domain;
using Newtonsoft.Json.Linq;

namespace GB_API.Server.Application;

public class TrafficService
{
    private readonly Dataset _tomtomTrafficApi;
    private readonly Dataset _geocoding;
    private readonly Key _key;
    
    public TrafficService()
    {
        // uiteindelijk komt dit ergens anders

        _key = new Key("ntW0IQTDLc9EBgSzbrmjTCR5DBrSuIgH", KeyType.Primary);
        // _tomtomTrafficApi = new Dataset
        // {
        //     Name = "TomTom-Traffic",
        //     KeyInHeader = false,
        //     BaseUrl = "https://api.tomtom.com/traffic/services/5/incidentDetails"
        // };
        //
        // _geocoding = new Dataset
        // {
        //     Name = "TomTom-Geocoding",
        //     KeyInHeader = false,
        //     BaseUrl = "https://api.tomtom.com/search/2/geocode/"
        // };
        _tomtomTrafficApi = new Dataset("TomTom-Traffic", false, "https://api.tomtom.com/traffic/services/5/incidentDetails");
        _geocoding = new Dataset("TomTom-Geocoding", false, "https://api.tomtom.com/search/2/geocode/");
    }

    public async Task<List<TrafficIncident>?> GetTrafficIncidentsIn(string query, 
        double latitude = 0.0, double longitude = 0.0, double kilometerRadius = 0.5)
    {
        // Als een incident locatie altijd accurate coordinaten bevat kunnen deze regels weggehaald worden
        var queryCoordinates = await GetCoordinatesFromQuery(query, latitude, longitude);
        
        var lat = queryCoordinates["lat"].ToObject<double>();
        var lon = queryCoordinates["lon"].ToObject<double>();
        
        //MinMax waarden voor de BoundingBox
        var bbox = CalculateBoundingBox.GetBoundingBox(new GeoCoordinate(lat, lon), kilometerRadius);
        
        // BBOX tekenen: http://bboxfinder.com
        //Console.WriteLine(bbox.ToStringLonFirst());
        
        Dictionary<string, string> queryParams = new()
        {
            { "bbox", bbox.ToStringLonFirst() },
            {
                "fields", "{incidents{geometry{type,coordinates},properties{iconCategory,events{description,code}," +
                          "startTime,endTime,from,to,length,delay,roadNumbers,probabilityOfOccurrence," +
                          "lastReportTime}}}"
            },
            { "key", _key.Value }
        };

        // RoadWorks (nummer 9 bij categoryFilter) zou als het niet nodig is weggehaald kunnen worden
        var client = RequestSender.HttpClientBuilder(_tomtomTrafficApi.BaseUrl);
        const string optionalUri = "?language=en-GB&categoryFilter=0,1,2,3,4,5,6,7,8,9,10,11,14&timeValidityFilter=present&";
        var node = await RequestSender.SendRequest(client, RequestMethod.Get, optionalUri, queryParams);
        
        var properties = new List<string>()
        {
            "properties/iconCategory", "properties/startTime", "properties/endTime", "properties/from", "properties/to",
            "properties/length", "properties/delay", "properties/roadNumbers", "properties/probabilityOfOccurrence",
            "properties/lastReportTime", "properties/events"
        };
        var dictionaryList = GetJsonData.ReadJsonArrayDataFromString(node, 
            new List<string>(properties) { "geometry/coordinates" }, "incidents");

        return FromDictionaryListToObjectList(dictionaryList);
    }
    
    private async Task<Dictionary<string, JToken>> GetCoordinatesFromQuery(string query, 
        double latitude = 0.0, double longitude = 0.0)
    {
        var queryWithFormat = query + ".json?limit=1&countrySet=NL&&minFuzzyLevel=1&maxFuzzyLevel=2&view=Unified&relatedPois=off&";
        Dictionary<string, string> queryParams = new()
        {
            { "key", _key.Value }
        };
        
        // Lat en Lon kunnen meegegeven worden voor meer precisie
        if(latitude > 0.0 && longitude > 0.0)
        {
            var info = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            queryParams.Add("lat", latitude.ToString(info));
            queryParams.Add("lon", longitude.ToString(info));
        }
        
        var client = RequestSender.HttpClientBuilder(_geocoding.BaseUrl);
        JsonNode node = await RequestSender.SendRequest(client, RequestMethod.Get, queryWithFormat, queryParams);

        var dictionaryList = GetJsonData.ReadJsonArrayDataFromString(node, 
            new List<string>() { "position/lat", "position/lon" }, "results");
        
        return dictionaryList[0];
    }
    
    private List<TrafficIncident> FromDictionaryListToObjectList(List<Dictionary<string, JToken>> dictionaryList)
    {
        List<TrafficIncident> trafficIncidents = new();
        foreach (var dictionary in dictionaryList)
        {
             TrafficIncident trafficIncident = new();

             var probability = dictionary["probabilityOfOccurrence"].ToString();
             var isProbable = probability is "probable" or "certain";
             if (!isProbable) continue;
             
             trafficIncident.IconCategory = dictionary["iconCategory"].ToObject<int>();
             trafficIncident.StartTime = dictionary["startTime"].ToObject<DateTime>();
             trafficIncident.EndTime = dictionary["endTime"].ToObject<DateTime?>();
             trafficIncident.From = dictionary["from"].ToString();
             trafficIncident.To = dictionary["to"].ToString();
             trafficIncident.Length = dictionary["length"].ToObject<double>();
             trafficIncident.Delay = dictionary["delay"].ToObject<int>();
             trafficIncident.RoadNumbers = dictionary["roadNumbers"].ToObject<string[]?>();
             trafficIncident.LastReportTime = dictionary["lastReportTime"].ToString();
            
             var coordinateList = dictionary["coordinates"].ToObject<List<List<double>>>();
             foreach (var coordinates in coordinateList!)
             {
                 var longitude = coordinates[0];
                 var latitude = coordinates[1];

                 trafficIncident.Coordinates.Add(new GeoCoordinate(latitude, longitude));
             }

             var eventList = dictionary["events"].ToObject<List<TrafficIncidentEvent>>();
             eventList!.ForEach(tEvent => trafficIncident.Events.Add(tEvent));

             trafficIncidents.Add(trafficIncident);
        }
        return trafficIncidents;
    }
}