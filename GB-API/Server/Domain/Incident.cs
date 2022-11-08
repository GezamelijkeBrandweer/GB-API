﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GB_API.Server.Domain;

public class Incident
{
    public long Id { get; set; }
    public string Name { get; set; }
    public Locatie Locatie { get; set; }
    public ICollection<Intensiteit> Intensiteiten { get; set; } 
    public ICollection<Karakteristiek> Karakteristieken { get; set; } 
    public ICollection<Meldingsclassificatie> Meldingsclassificaties { get; set; }

    public Incident(){}

    public Incident(string name, Locatie locatie)
    {
        Name = name;
        Locatie = locatie;
        Karakteristieken = new List<Karakteristiek>();
        Meldingsclassificaties = new List<Meldingsclassificatie>();
        Intensiteiten = new List<Intensiteit>();
    }

    public void AddIntensiteit(Dienst dienst)
    {
        Intensiteiten.Add(new Intensiteit(dienst));
    }
}