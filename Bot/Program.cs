using Bot.Services;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;


var botClient = new TelegramBotClient("5642967169:AAE5tWm8uXqpK2O8X4_4_7sQerNcSm4Ev_c");
var cts = new CancellationTokenSource();

var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = { } // receive all update types
};

botClient.StartReceiving(
    HandleUpdateAsync,
    HandleErrorAsync,
    receiverOptions,
    cancellationToken: cts.Token);

Console.WriteLine($"Bot is up and running.");
Console.ReadLine();


cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    
    if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
    {
        var messageService = new AdminServices(botClient);
        await messageService.HandleMessageAsync(update.Message);
    }
}

Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    // Handle the error here
    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
    return Task.CompletedTask;
}