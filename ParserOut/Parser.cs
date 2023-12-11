using Parser.Parser;
using Parser.Parser.Global;

namespace ParserOut;

public class Parser
{
    public async Task<string> Parse(string query)
    {
        var file = File.Create(Path.Combine(Directory.GetCurrentDirectory(), $"{query}-{DateTime.Now.ToString()}"));
        database.FileName = file.Name;
        var browserManager = new BrowserManager();
        var parser = new QueryParser(browserManager);
        string url = $"https://megamarket.ru/catalog/?q={query}";
        parser.ParseProductAsync(url).GetAwaiter().GetResult();
        return "string";

    }
}