using System.Text.Json;
using System.Text.Json.Nodes;
using GB_API.Server.Domain.Traffic;
using MIC_RequestSender;
using MIC_RequestSender.Domain;

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
                          "startTime,endTime,from,to,length,delay,roadNumbers,timeValidity,probabilityOfOccurrence," +
                          "numberOfReports,lastReportTime}}}"
            },
            { "key", _key.Value }
        };

        var client = RequestSender.HttpClientBuilder(_tomtomTrafficApi.BaseUrl);
        var node = await RequestSender.SendRequest(client, RequestMethod.Get,
            "?language=en-GB&categoryFilter=0,1,2,3,4,5,6,7,8,9,10,11,14&timeValidityFilter=present&",  
            queryParams);
        
        // TODO Hardgecode code vervangen
        var listIncidents = this.ReadJsonDataFromStringAsList(node, "incidents");
        return listIncidents;
    }

    private async Task<Dictionary<string, object>> GetBoundingBoxFromQuery(string query, int kilometerRadius = 0)
    {
        var queryWithFormat = query + ".json?limit=1&countrySet=NL&&minFuzzyLevel=1&maxFuzzyLevel=2&view=Unified&relatedPois=off&";
        Dictionary<string, string> queryParams = new()
        {
            { "key", _key.Value }
        };
        
        var client = RequestSender.HttpClientBuilder(_geocoding.BaseUrl);
        JsonNode node = await RequestSender.SendRequest(client, RequestMethod.Get, queryWithFormat, queryParams);

        Dictionary<string, object> dict = GetJsonData.ReadJsonDataFromString(node, new() { "results" });
        return dict;
    }

    //longlat - (0.009 * kilometerRadius); voor elke long of lat doen
    private void AddRadiusToLongLat(Dictionary<string, dynamic> dictionary, int kilometerRadius)
    {
        var topLeft = dictionary["topLeftPoint"];
        var bottomRight = dictionary["btmRightPoint"];
        
        topLeft.lat -= (0.009 * kilometerRadius);
        topLeft.lon -= (0.009 * kilometerRadius);
        
        bottomRight.lat += (0.009 * kilometerRadius);
        bottomRight.lon += (0.009 * kilometerRadius);
    }
    
    //TODO voor nu snel opgezet uiteindelijk vervangen
    private List<TrafficIncident> ReadJsonDataFromStringAsList(JsonNode body, string jsonKey)
    {
        var filteredResponse = body[jsonKey]?.ToJsonString();
        return JsonSerializer.Deserialize<List<TrafficIncident>>(filteredResponse);
    }
}