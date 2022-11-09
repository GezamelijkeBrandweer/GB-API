namespace GB_API.Server.Domain;

public class Locatie
{
    public long Id { get; set; }
    public string Straat { get; set; }
    public string Postcode { get; set; }
    public string Huisnummer { get; set; }
    public double Latitude { get; set; }
    public double Longtitude { get; set; }

    public Locatie() { }

    public Locatie(string straat, string postcode, string huisnummer, double latitude, double longtitude)
    {
        Straat = straat;
        Postcode = postcode;
        Huisnummer = huisnummer;
        Latitude = latitude;
        Longtitude = longtitude;
    }
}