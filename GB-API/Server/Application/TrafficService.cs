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

        _key = new Key("ntW0IQTDLc9EBgSzbrmjTCR5DBrSuIgH");
        _tomtomTrafficApi = new Dataset
        {
            Name = "TomTom-Traffic",
            KeyInHeader = false,
            BaseUrl = "https://api.tomtom.com/traffic/services/5/incidentDetails"
        };

        _geocoding = new Dataset
        {
            Name = "TomTom-Geocoding",
            KeyInHeader = false,
            BaseUrl = "https://api.tomtom.com/search/2/geocode/"
        };
    }

    public async Task<List<TrafficIncident>?> GetTrafficIncidentsIn(string query)
    {
        //this.GetBoundingBoxFromQuery(query, 10).ToString();
        
        var bbox = "4.8854592519716675,52.36934334773164,4.897883244144765,52.37496348620152";
        Dictionary<string, string> queryParams = new()
        {
            { "bbox", bbox },
            {
                "fields", "{incidents{geometry{type,coordinates},properties{iconCategory,events{description,code}," +
                          "startTime,endTime,from,to,length,delay,roadNumbers,probabilityOfOccurrence," +
                          "lastReportTime}}}"
            },
            { "key", _key.Value }
        };

        var client = RequestSender.HttpClientBuilder(_tomtomTrafficApi.BaseUrl);
        var optionalUri = "?language=en-GB&categoryFilter=0,1,2,3,4,5,6,7,8,9,10,11,14&timeValidityFilter=present&";
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

    //TODO Deze week fixen, gebruik de bbox hiervan bij traffic incidenten ophalen
    private async Task<Dictionary<string, object>> GetBoundingBoxFromQuery(string query, int kilometerRadius = 0)
    {
        var queryWithFormat = query + ".json?limit=1&countrySet=NL&&minFuzzyLevel=1&maxFuzzyLevel=2&view=Unified&relatedPois=off&";
        Dictionary<string, string> queryParams = new()
        {
            { "key", _key.Value }
        };
        
        var client = RequestSender.HttpClientBuilder(_geocoding.BaseUrl);
        JsonNode node = await RequestSender.SendRequest(client, RequestMethod.Get, queryWithFormat, queryParams);

        //Dictionary<string, object> dict = GetJsonData.ReadJsonDataFromString(node, new() { "results" });
        //return dict;
        return null;
    }

    //longlat - (0.009 * kilometerRadius); voor elke long of lat doen
    //TODO Hoogst waarschijnlijk gaat dit weg
    private void AddRadiusToLongLat(Dictionary<string, dynamic> dictionary, int kilometerRadius)
    {
        var topLeft = dictionary["topLeftPoint"];
        var bottomRight = dictionary["btmRightPoint"];
        
        topLeft.lat -= (0.009 * kilometerRadius);
        topLeft.lon -= (0.009 * kilometerRadius);
        
        bottomRight.lat += (0.009 * kilometerRadius);
        bottomRight.lon += (0.009 * kilometerRadius);
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
             
             //TODO Deze velden implementeren
             //incident.Events = dictionary["events"];
             //incident.Coordinates = dictionary["coordinates"];
            
             trafficIncidents.Add(trafficIncident);
        }
        return trafficIncidents;
    }
}