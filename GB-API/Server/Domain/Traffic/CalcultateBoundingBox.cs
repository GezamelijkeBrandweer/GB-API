namespace GB_API.Server.Domain.Traffic;

public static class CalculateBoundingBox
{
    private const double LatitudeValue = 0.009;
    // private const double LongitudeValue = 0.018;
    private const double LongitudeValue = 0.018;
        
    public static BoundingBox GetBoundingBox(GeoCoordinate point, double kilometerRadius)
    {
        var lat = point.Latitude;
        var lon = point.Longitude;
        
        var latMin = lat - (LatitudeValue * kilometerRadius);
        var latMax = lat + (LatitudeValue * kilometerRadius);
        var lonMin = lon - (LongitudeValue * kilometerRadius);
        var lonMax = lon + (LongitudeValue * kilometerRadius);

        return new BoundingBox { 
            MinPoint = new GeoCoordinate(latMin, lonMin),
            MaxPoint = new GeoCoordinate(latMax, lonMax)
        };  
    }
}