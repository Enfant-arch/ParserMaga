using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.Services;

public class AdminServices
{
    private readonly ITelegramBotClient _botClient;

    public AdminServices(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task HandleMessageAsync(Message message)
    {
        if (message.Text is null)
            return;
        if(message.From.Id != 5090531675)
            return;
        
        switch (message.Text.Split(' ').First().ToLower())
        {
            case "/start":
                await _botClient.SendTextMessageAsync(chatId: message.Chat.Id, text: "Welcome to the bot!");
                break;
            case "/help":
                await _botClient.SendTextMessageAsync(chatId: message.Chat.Id, text: "How can I assist you?");
                break;
            // More commands here
            default:
                await _botClient.SendTextMessageAsync(chatId: message.Chat.Id, text: "Sorry, I didn't understand that command.");
                break;
        }
        
    }
}
