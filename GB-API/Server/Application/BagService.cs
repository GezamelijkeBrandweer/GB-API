using MIC_RequestSender.Domain;

namespace GB_API.Server.Application;

public class BagService
{
    private readonly Key _key = new("l7e6e43c660b6f46d6a0c7ddd72a159030", KeyType.Primary);

    private readonly Dataset bagDataset =
        new("BAG-API", true, "https://api.bag.kadaster.nl/lvbag/individuelebevragingen/v2/");
}