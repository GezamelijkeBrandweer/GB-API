using System.Globalization;

namespace GB_API.Server.Domain.Traffic;

public class BoundingBox
{
    private static readonly NumberFormatInfo Info = new() { NumberDecimalSeparator = "." };
    public GeoCoordinate MinPoint { get; set; }
    public GeoCoordinate MaxPoint { get; set; }
    
    // Direct gebruiken voor TomTom bbox parameter
    public string ToStringLonFirst()
    {
        return $"{MinPoint.Longitude.ToString(Info)},{MinPoint.Latitude.ToString(Info)},{MaxPoint.Longitude.ToString(Info)},{MaxPoint.Latitude.ToString(Info)}";
    }
    
    public string ToStringLatFirst()
    {
        return $"{MinPoint.Latitude.ToString(Info)},{MinPoint.Longitude.ToString(Info)},{MaxPoint.Latitude.ToString(Info)},{MaxPoint.Longitude.ToString(Info)}";
    }
}