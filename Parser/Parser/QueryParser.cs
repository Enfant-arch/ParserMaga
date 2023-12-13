using System.Text;
using System.Text.Json;
using HtmlAgilityPack;

using Parser.Parser.GoodSorted;
using Parser.utlis;

namespace Parser.Parser;

public class QueryParser
{
    private BrowserManager _browserManager;

    public QueryParser(BrowserManager browserManager)
    {
        _browserManager = browserManager;
    }
    
    public async Task<bool> ParseProductAsync(string query)
    {
        await _browserManager.UseBrowserAsync(async (context) =>
        {
            try
            {
                string url = $"https://megamarket.ru/catalog/?q={query}";
                Console.WriteLine($"URL PARSING OF : {url}");
                
                var page = await context.NewPageAsync();
                Random random = new Random();
                int pause = random.Next(500, 800);
                int counter = 1;
                await Task.Delay(pause);
                await page.GotoAsync(url);
                await Task.Delay(pause);
                var element = await page.ContentAsync();
                var parsed = await  Get_Content(element);
                while((parsed == true) && (counter <= 5))
                {
                    counter++;
                    url = $"https://megamarket.ru/catalog/" + $"page-{counter}" + $"/?q={query}";
                    await Task.Delay(pause);
                    await page.GotoAsync(url);
                    await Task.Delay(pause);
                    Console.WriteLine($"Parsing next page [{counter}] - {url}");
                    parsed = await  Get_Content(element);
                }
                

                await page.CloseAsync();
        
            }
            catch (TimeoutException exception)
            {
                Console.WriteLine($"Can't load : {query} - TIMEOUT EXCEPTION");
                
            }

        });
        return true;
    }
    

    public static async Task<bool> Get_Content(string element)
    {
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(element);
        if (!CheckForBad(doc))
        {
            await ParseAndSendData(doc);
            return true;
            
        }
        else return false;
    }


    public static async Task ParseAndSendData(HtmlDocument doc)
    {
        var htmlNodeCollection = doc.DocumentNode.SelectNodes("//script[contains(text(), 'window.__APP__')]");
            if (htmlNodeCollection != null)
            {
                var x = htmlNodeCollection[2];
                var json_products = x.InnerText;
                var removedWA = json_products.Skip(15);
                string result = string.Concat(removedWA);
                var json = result.Replace("undefined", "\"undefined\"");
                
                var serializerOptions = new JsonSerializerOptions
                {
                    IgnoreNullValues = true,
                   
                    
                };
             
                
                var root = JsonSerializer.Deserialize<PlpStoreObject>(json, serializerOptions);

                
                if (root?.PlpStoreHydratorState?.PlpStore.ListingData.items != null)
                {
               
                    foreach (var ItemWrapper  in root.PlpStoreHydratorState.PlpStore.ListingData.items)
                    {
                        DatabaseManager.InsertProductAsync(ItemWrapper);
                        
                        Good goods = ItemWrapper.DeserializeGood();
                        if (goods != null && (ItemWrapper.bonusPercent >= 10 || ItemWrapper.oldPriceChangePercentage >= 10))
                        {
                            using (var file = new FileStream(Global.database.FileName, FileMode.Append))
                            {
                                string message = $"Goods ID: {goods.goodsId}\n" + $"Title: {goods.title}\n" +
                                                 $"Title Image: {goods.titleImage}\n" + $"webUrl: {goods.webUrl}\n" +
                                                 $"webUrl: {goods.webUrl}\n" + $"price: {ItemWrapper.price}\n" +
                                                 $"price: {ItemWrapper.price}\n" +
                                                 $"description: {goods.description}\n" +
                                                 $"bonus percent: {ItemWrapper.bonusPercent}\n" +
                                                 $"bonus amount: {ItemWrapper.bonusAmount}\n";
                                Console.WriteLine(message);
                                file.WriteAsync(Encoding.UTF8.GetBytes(message), 0, message.Length);
                            }

                            
                            
                        }
                    }
                }

            }
    }
    
    
    public static bool CheckForBad(HtmlDocument htmlDocument)
    {
        var nodesWithClass = htmlDocument.DocumentNode.SelectNodes("//*[contains(@class, 'catalog-category-cell') and contains(@class, 'catalog-department__category-item')]");
        return nodesWithClass != null && nodesWithClass.Count > 0;
    }
}