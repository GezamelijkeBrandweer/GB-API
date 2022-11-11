namespace GB_API.Server.Domain;

public class Meldingsclassificatie
{
    public long Id { get; set; }
    public string Niveau1 { get; set; }
    public string Niveau2 { get; set; }
    public string Niveau3 { get; set; }
    public string Afkorting { get; set; }
    public string Definitie { get; set; }
    public string  PresentatieTekst { get; set; }
    
    public Meldingsclassificatie(){ }
    
    public Meldingsclassificatie(string niveau1, string niveau2, string niveau3, string afkorting, string definitie, string presentatieTekst)
    {
        Niveau1 = niveau1;
        Niveau2 = niveau2;
        Niveau3 = niveau3;
        Afkorting = afkorting;
        Definitie = definitie;
        PresentatieTekst = presentatieTekst;
    }
}