using System.Text.Json;
using System.Text.Json.Serialization;

namespace Parser.Parser.GoodSorted;

public class PlpStoreObject
{
    [JsonPropertyName("hydratorState")]
    public PlpStoreHydratorState PlpStoreHydratorState { get; set; }
}


public class PlpStoreHydratorState
{
    [JsonPropertyName("PlpStore")]
    public PlpStore PlpStore { get; set; }  
}



public class PlpStore
{
    [JsonPropertyName("listingData")]
    public ListingData ListingData { get; set; }
}

public class ListingData
{
    [JsonPropertyName("items")]
    public List<Items> items { get; set; }
}



public class Items
{
    [JsonPropertyName("goods")]
    public JsonElement Goods { get; set; }
    
    public Good DeserializeGood()
    {
        if (Goods.ValueKind == JsonValueKind.Object)
        {
            return JsonSerializer.Deserialize<Good>(Goods.GetRawText());
        }
        return null;
    }
    public short bonusPercent { get; set; }
    public int price { get; set; }
    public int lastPrice { get; set; }
    public int oldPriceChangePercentage { get; set; }
    public int review_count { get; set; }
    
    public int bonusAmount { get; set; }
    public string advertiserName { get; set; }
}


public  class Good
{
    public string goodsId { get; set; }
    public string title { get; set; }
        
    public string description { get; set; }
    public string titleImage { get; set; }
    public string webUrl { get; set; }
    
    
}
