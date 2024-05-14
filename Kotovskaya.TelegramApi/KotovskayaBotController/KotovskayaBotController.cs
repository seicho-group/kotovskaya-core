using Telegram.Bot;

namespace Kotovskaya.TelegramApi.KotovskayaBotController;

public class KotovskayaBotController(string token) : TelegramBotClient(token)
{
    public async Task SendMessageToChat(string message)
    {
        await this.SendTextMessageAsync(Environment.GetEnvironmentVariable("TG_CHAT")!, message);
    }
};
