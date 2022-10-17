using System.Globalization;

namespace GB_API.Server.Domain.Traffic;

public readonly struct GeoCoordinate
{
    public double Latitude { get; }
    public double Longitude { get; }

    public GeoCoordinate(double latitude, double longitude)
    {
        this.Latitude = latitude;
        this.Longitude = longitude;
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