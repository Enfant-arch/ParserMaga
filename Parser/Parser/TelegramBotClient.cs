namespace Parser;

public class TelegramBotClient
{
    private string _token;
    private HttpClient _httpClient;

    public TelegramBotClient(string token)
    {
        _token = token;
        _httpClient = new HttpClient();
    }

    public async Task SendMessageAsync(long chatId, string message)
    {
        var url = $"https://api.telegram.org/bot{_token}/sendMessage";
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "chat_id", chatId.ToString() },
            { "text", message }
        });
        
        var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
    }
    
}