using System.Globalization;

namespace GB_API.Server.Domain.Traffic;

//https://stackoverflow.com/questions/6151625/should-i-use-a-struct-or-a-class-to-represent-a-lat-lng-coordinate
public struct GeoCoordinate
{
    private readonly double _latitude;
    private readonly double _longitude;

    public double Latitude => _latitude;
    public double Longitude => _longitude;

    public GeoCoordinate(double latitude, double longitude)
    {
        this._latitude = latitude;
        this._longitude = longitude;
    }

    public override string ToString()
    {
        var info = new NumberFormatInfo
        {
            NumberDecimalSeparator = "."
        };
        return $"{Latitude.ToString(info)},{Longitude.ToString(info)}";
    }
}