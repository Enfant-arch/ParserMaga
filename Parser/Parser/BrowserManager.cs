using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Microsoft.Playwright;
using Newtonsoft.Json;
using Parser.Parser.Global;
using Parser.utlis;

namespace Parser.Parser;

public class BrowserManager
{
    private SemaphoreSlim _semaphore;
    private IPlaywright _playwright;
    private IBrowser _browser;

    public BrowserManager()
    {
        _semaphore = new SemaphoreSlim(6);
        InitializePlaywrightAsync();
    }

    private async Task InitializePlaywrightAsync()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true,
        });
    }
    internal async Task DisposePlaywrightAsync()
    {
        _browser.CloseAsync();
    }
    

    public async Task UseBrowserAsync(Func<IBrowserContext, Task> action)
    {
        await _semaphore.WaitAsync();
        try
        {
            var contextOptions = new BrowserNewContextOptions()
            {
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Safari/537.36",
                IsMobile = true,
                JavaScriptEnabled = true

            };
            
            var context = await _browser.NewContextAsync(contextOptions);
            await action(context);
            await context.CloseAsync();
            
        }
        finally
        {
            _semaphore.Release();
        }
    }












    public static async Task<string> main_categories()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        var page = await browser.NewPageAsync();
        
        Random random = new Random();
        int pause = random.Next(500, 1100);
        await Task.Delay(pause);
        await page.GotoAsync("https://megamarket.ru/catalog");
        await Task.Delay(pause);

        var element = await page.ContentAsync();
        if (element != null)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(element);
            HtmlNodeCollection names =
                doc.DocumentNode.SelectNodes(
                    "//h3[contains(@class, 'inverted-catalog-category__title')]");
            if (names != null)
            {
                foreach (HtmlNode div in names)
                {
                    string divContent = div.InnerHtml;
                    Console.WriteLine(divContent);
                }
            }
        }

        await browser.CloseAsync();
        return "";
    }


    public static async Task advancedGenerationUrl(HtmlDocument doc)
    {
        HtmlNodeCollection links =
            doc.DocumentNode.SelectNodes(
                "//div[contains(@class, 'catalog-category-cell catalog-department__category-item')]");
        if (links != null)
        {
            foreach (HtmlNode div in links)
            { 
                string divContent = div.InnerHtml;
                Regex data = new Regex(@"<a href=""(.*?)"">(.*?)</a>");
                Regex title = new Regex(@"<h3 class=""catalog-category__title"">(.*?)<\/h3>");
                foreach (Match match in data.Matches(divContent.ToString()))
                {

                    if (match.Success)
                    {
                    string titli = title.Match(divContent).Groups[1].Value;
                    string urli = "https://megamarket.ru" + match.Groups[1].Value.ToString()
                        .Substring(0, match.Groups[1].Value.ToString().Length - 36);
                    Console.WriteLine($"Added to list urls - {titli} : {urli}");
                        GoodPlace.NameUrls.Add(match.Groups[2].Value, urli);
                        
                        if (match.Groups[1].Value is not null)
                        {
                            GoodPlace.GlobalUrls.Add(urli);
                        }
                    }

                }
            }
        }
    }

    public static async Task generator_forButtons()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true,
            
        });

        var page = await browser.NewPageAsync();
        
        Random random = new Random();
        int pause = random.Next(500, 1100);
        await Task.Delay(pause);
        await page.GotoAsync("https://megamarket.ru/catalog");
        await Task.Delay(pause);


        var element = await page.ContentAsync();
        if (element != null)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(element);
            HtmlNodeCollection links =
                doc.DocumentNode.SelectNodes("//div[contains(@class, 'inverted-catalog-category__title')]");


            if (links != null)
            {
                foreach (HtmlNode div in links)
                {
                    string divContent = div.InnerHtml;
                    Regex data = new Regex(@"<h3 class=""inverted-catalog-category__title""[^>]*>(.*?)<\/h3>");

                    foreach (Match match in data.Matches(divContent.ToString()))
                    {
                        if (match.Success)
                        {
                            string urli = "https://megamarket.ru" + match.Groups[1].Value.ToString()
                                .Substring(0, match.Groups[1].Value.ToString().Length - 9);
                            try
                            {
                                GoodPlace.NameUrls.Add(match.Groups[2].Value, urli);
                            }
                            catch (ArgumentException error)
                            {
                                Console.WriteLine(match.Groups[2].Value);
                                continue;
                            }
                            if (match.Groups[1].Value is not null) { GoodPlace.GlobalUrls.Add(urli); }
                        }

                    }
                }
            }
        }
    }

    public static async Task generator_of_urls() {
        
            using var playwright = await Playwright.CreateAsync();
            var contextOptions = new BrowserNewContextOptions()
            {
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Safari/537.36",
                IsMobile = true,
                JavaScriptEnabled = true

            };
            
        
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true
            });
            
            var context = await browser.NewContextAsync(contextOptions);
            var page = await context.NewPageAsync();

            await page.GotoAsync("https://megamarket.ru/catalog");
            
            var element = await page.ContentAsync();
            if (element != null)
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(element);
                HtmlNodeCollection links =
                    doc.DocumentNode.SelectNodes(
                        "//div[contains(@class, 'inverted-catalog-category__subcategories-item')]");


                if (links != null)
                {
                    foreach (HtmlNode div in links)
                    {
                        string divContent = div.InnerHtml;
                        Regex data = new Regex(@"<a href=""(.*?)"">(.*?)</a>");

                        foreach (Match match in data.Matches(divContent.ToString()))
                        {

                            if (match.Success)
                            {
                                string urli = "https://megamarket.ru" + match.Groups[1].Value
                                    .Substring(0, match.Groups[1].Value.Length - 9);
                               
                                try
                                {
                                    Console.WriteLine($"Taked url : {urli}");
                                    GoodPlace.NameUrls.Add(match.Groups[2].Value, urli);
                                }
                                catch (ArgumentException error)
                                {
                                    Console.WriteLine(match.Groups[2].Value);
                                    continue;
                                }

                                if (match.Groups[1].Value is not null)
                                {
                                    GoodPlace.GlobalUrls.Add(urli);
                                }
                            }

                        }
                    }
                }
            }

            if (GoodPlace.NameUrls.Count > 2)
            {
                string json = JsonConvert.SerializeObject(GoodPlace.NameUrls, Formatting.Indented);
                File.WriteAllText("catalog_dict.json", json);
            }
            else
            {
                throw new GenerateDictJson();
            }

            await page.CloseAsync();
            await context.CloseAsync();
            await browser.CloseAsync();
        }
    }

