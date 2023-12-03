using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Parser.Parser.GoodSorted;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Parser.Parser;


public class FetchProducts
{
    public static async Task SortWithPrefetchStore(string json)
    {
            var serializerOptions = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                
            };

            var root = JsonSerializer.Deserialize<PrefetchStoreObject>(json, serializerOptions);
            
            if (root?.PrefetchStoreHydratorState?.PrefetchStore?.ComponentsInitialState?.Main?.Banners != null)
            {
                foreach (var banner in root.PrefetchStoreHydratorState.PrefetchStore.ComponentsInitialState.Main.Banners)
                {
                    if (banner.Goods.ValueKind == JsonValueKind.Array)
                    {
                        var goodsList = JsonSerializer.Deserialize<List<GoodWrapper>>(banner.Goods.GetRawText());
                        Console.WriteLine(goodsList);
                        
                        
                    }
                }
            }


    }
    
    public static async Task SortWithPlpStore(string json)
    {
        var serializerOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
                
        };

        var root = JsonSerializer.Deserialize<PlpStoreObject>(json, serializerOptions);

            
        if (root?.PlpStoreHydratorState?.PlpStore.ListingData.items != null)
        {
            foreach (var ItemWrapper  in root.PlpStoreHydratorState.PlpStore.ListingData.items)
            {
                Good goods = ItemWrapper.DeserializeGood();
                if (goods != null)
                {
                    string message = $"Goods ID: {goods.goodsId}\n" + $"Title: {goods.title}\n" +
                                    $"Title Image: {goods.titleImage}\n" + $"webUrl: {goods.webUrl}\n" + $"webUrl: {goods.webUrl}\n" + $"price: {ItemWrapper.price}\n" +
                                    $"price: {ItemWrapper.price}\n" + $"bonus percent: {ItemWrapper.bonusPercent}\n" + $"bonus amount: {ItemWrapper.bonusAmount}\n";

                    var bot = new TelegramBotClient("5642967169:AAE5tWm8uXqpK2O8X4_4_7sQerNcSm4Ev_c");
                    Thread.Sleep(500);
                    await bot.SendMessageAsync(5090531675, message);

                    
                    
                }
                

            }
        }


    }
}