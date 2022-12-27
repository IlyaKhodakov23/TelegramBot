using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using UtilityBot.Configuration;

namespace UtilityBot.Services
{
    public class Function : IFunction
    {
        private readonly ITelegramBotClient _telegramBotClient;
        public Function(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }

        public void Process(string functiontype, Message message, CancellationToken ct)
        {
            if (functiontype == "lenString")
            {
                _telegramBotClient.SendTextMessageAsync(message.From.Id, $"Длина сообщения: {message.Text.Length} знаков", cancellationToken: ct);
            }
            else if (functiontype == "GetSum")
            {
                try
                {
                    int[] values = message.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToArray();
                    int sum = values.Sum();
                    _telegramBotClient.SendTextMessageAsync(message.From.Id, $"Сумма чисел: {sum}", cancellationToken: ct);
                }
                catch(Exception ex) when (ex is FormatException)
                {
                    _telegramBotClient.SendTextMessageAsync(message.From.Id, $"Введите числа через пробел", cancellationToken: ct);
                }
                
            }
        }
    }
}
