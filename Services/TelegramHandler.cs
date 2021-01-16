using System;
using System.Threading.Tasks;
using Telegram.Bot;

namespace CryptoPortfolioReader.Services
{
    public class TelegramHandler
    {
        private TelegramBotClient botClient;
        private readonly String chatId;

        public TelegramHandler(TelegramConfiguration config)
        {
            botClient = new TelegramBotClient(config.ApiAccessToken);
            chatId = config.ChatId;
        }

        public async Task SendMessage(String message)
        {
            var res = await botClient.SendTextMessageAsync(chatId, message);
        }
    }
}