using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Npgsql;
using NUnit.Framework;
using Parser;
using Parser.Parser;
using Parser.Parser.Global;


class Program
{

    static async Task Main()
    {
       
        var browserManager = new BrowserManager();
        var parser = new ProductParser(browserManager);
        database.ConnectString = "server=localhost;port=5432;database=postgres;username=postgres;password=root";
        using var connection = new NpgsqlConnection(database.ConnectString);
        await connection.OpenAsync();
        GoodPlace.GlobalUrls = new List<string>();
        GoodPlace.NameUrls = new Dictionary<string, string>();
        await BrowserManager.generator_of_urls();
        var parseTasks = GoodPlace.GlobalUrls.Select(url => parser.ParseProductAsync(url));
       
        await Task.WhenAll(parseTasks);
        await browserManager.DisposePlaywrightAsync();

        

    }
    
}