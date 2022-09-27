using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB_API.Server.Domain;

public class Locatie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Woonplaats { get; set; }
    public string Straat { get; set; }
    public int Huisnummer { get; set; }
    public string Huisletter { get; set; }
    public long Latitude { get; set; }
    public long Longtitude { get; set; }

    public Locatie(string woonplaats, string straat, int huisnummer, string huisletter, long latitude, long longtitude)
    {
        Woonplaats = woonplaats;
        Straat = straat;
        Huisnummer = huisnummer;
        Huisletter = huisletter;
        Latitude = latitude;
        Longtitude = longtitude;
    }
}