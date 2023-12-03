using System.Net.Mime;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Services.Keyboard;

public class UserInline
{
    static async Task<InlineKeyboardMarkup> CatalogBoard()
    {
        Dictionary<InlineKeyboardButton, List<InlineKeyboardButton>> main_catalog =
            new Dictionary<InlineKeyboardButton, List<InlineKeyboardButton>>();
        return new InlineKeyboardMarkup(InlineKeyboardButton.WithPayment(""));

    } 
}