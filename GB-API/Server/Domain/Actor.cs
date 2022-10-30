namespace GB_API.Server.Domain;

public class Actor
{
    public long Id { get; set; }
    public string Name { get; set; }


    public Actor(){}

    public Actor(string name)
    {
        Name = name;
    }
}