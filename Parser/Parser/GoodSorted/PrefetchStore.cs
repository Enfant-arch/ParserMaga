using System.Text.Json;
using System.Text.Json.Serialization;

namespace Parser.Parser.GoodSorted;
class PrefetchStoreObject
{
    [JsonPropertyName("hydratorState")]
    public PrefetchStoreHydratorState PrefetchStoreHydratorState { get; set; }
}


public  class PrefetchStoreHydratorState
{
    [JsonPropertyName("PrefetchStore")]
    public PrefetchStore PrefetchStore { get; set; }
}



public class PrefetchStore
{
    [JsonPropertyName("componentsInitialState")]
    public ComponentsInitialState ComponentsInitialState { get; set; }
}


public class ComponentsInitialState
{
    [JsonPropertyName("main")]
    public Main Main { get; set; }
}

public  class Main
{
    [JsonPropertyName("banners")]
    public List<Banner> Banners { get; set; }
}

public  class Banner
{
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("goods")]
    public JsonElement  Goods { get; set; }
}


public  class GoodWrapper
{
    [JsonPropertyName("goods")]
    public GoodP Good { get; set; }
}


public  class GoodP
{
    public string goodsId { get; set; }
    public string title { get; set; }
    public string titleImage { get; set; }
    public string webUrl { get; set; }
    public short bonusPercent { get; set; }
    public int price { get; set; }
    public int lastPrice { get; set; }
    public int bonusAmount { get; set; }
    public string advertiserName { get; set; }
   
    
}