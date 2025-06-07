using StatisticsBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

internal class Program
{
    private static async Task Main(string[] args)
    {
        using var cts = new CancellationTokenSource();
        var bot = new TelegramBotClient("", cancellationToken: cts.Token);
        var me = await bot.GetMe();
        var stats = new StatisticsCollector();
        bot.OnMessage += OnMessage;

        Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
        Console.ReadLine();
        cts.Cancel(); // stop the bot
        // method that handle messages received by the bot:
        async Task OnMessage(Message msg, UpdateType type)
        {
            if (msg.Text is null) return;   // we only handle Text messages here

            if (msg.Text.Contains("/getstats"))
            {
                await bot.SendMessage(msg.Chat, $"{stats.GetStatistics()}");
                return;
            }

            stats.UpdateUserStatistics(msg.From.Id, msg);
        }
    }
}