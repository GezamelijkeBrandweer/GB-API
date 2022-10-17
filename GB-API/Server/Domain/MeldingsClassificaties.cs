namespace GB_API.Server.Domain;

public class MeldingsClassificaties
{
    public long Id { get; set; }
    public string Niveau1 { get; set; }
    public string Niveau2 { get; set; }
    public string Niveau3 { get; set; }
    public string Afkorting { get; set; }
    public string PresentatieTekst { get; set; }
    public string Definitie { get; set; }
    public int IntensiteitPunten { get; set; }

    public MeldingsClassificaties(){ }

    public MeldingsClassificaties(string niveau1, string niveau2, string niveau3, string afkorting, string presentatieTekst, string definitie, int intensiteitPunten)
    {
        Niveau1 = niveau1;
        Niveau2 = niveau2;
        Niveau3 = niveau3;
        Afkorting = afkorting;
        PresentatieTekst = presentatieTekst;
        Definitie = definitie;
        IntensiteitPunten = intensiteitPunten;
    }
}