using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class Host
    {
        public Action<ITelegramBotClient, Update>? OnMessage;  

        private TelegramBotClient _bot;
        public Host(string token)
        {
            _bot = new TelegramBotClient(token);
        }
        public TelegramBotClient Run()
        {
            _bot.StartReceiving(UpdateHandler, ErrorHandler);
            return _bot;
        }

        private async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken token)
        {
            Console.WriteLine($"{update.Message?.Chat.FirstName??"Нет имени"} {update.Message?.Chat.LastName ?? "Нет фамилии"}||{update.Message?.Chat.Username ?? "Нет имени пользователя"} {update.Message?.Date} написал " +update.Message?.Text ?? "[Not text]");
            OnMessage?.Invoke(client, update);
            await Task.CompletedTask;
        }

        private async Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            Console.WriteLine("Ошибка: " + exception.Message);
            await Task.CompletedTask;
        }
    }
}
