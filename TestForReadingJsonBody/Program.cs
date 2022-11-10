// See https://aka.ms/new-console-template for more information

using System.Text.Json.Nodes;
using MIC_RequestSender;

var file = File.ReadAllText("C:\\Users\\Dylla\\RiderProjects\\GB-API\\MIC-RequestSender\\DummyData.json");
var jsonKeys = new List<string>()
{
    "_embedded/dag","_embedded/week/nummer", "_embedded/week/lengte","_embedded/week/weer", "_embedded/week"
};
var data = GetJsonData.ReadJsonDataFromString(JsonNode.Parse(file), jsonKeys);
foreach (var keyValuePair in data)
{
    Console.WriteLine(keyValuePair.ToString());
}
