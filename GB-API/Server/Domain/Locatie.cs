namespace GB_API.Server.Domain;

public class Locatie
{
    public long Id { get; set; }
    public string Woonplaats { get; set; }
    public string Straat { get; set; }
    public int Huisnummer { get; set; }
    public string Huisletter { get; set; }
    public double Latitude { get; set; }
    public double Longtitude { get; set; }

    public Locatie() { }
    public Locatie(string woonplaats, string straat, int huisnummer, string huisletter, double latitude, double longtitude)
    {
        Woonplaats = woonplaats;
        Straat = straat;
        Huisnummer = huisnummer;
        Huisletter = huisletter;
        Latitude = latitude;
        Longtitude = longtitude;
    }
}