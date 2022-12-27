using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace UtilityBot.Services
{
    public interface IFunction
    {
        void Process(string param, Message message, CancellationToken ct);
    }
}
