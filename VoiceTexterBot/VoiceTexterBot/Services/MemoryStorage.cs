using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoiceTexterBot.Models;

namespace VoiceTexterBot.Services
{
    public class MemoryStorage : IStorage
    {
        /// <summary>
        /// Хранилище сессий
        /// </summary>
        ///
        //Коллекция-хранилище _sessions имеет тип данных ConcurrentDictionary.
        //Более подробно вы познакомитесь с коллекциями в одноимённом модуле позже.
        //А пока запомните, что тип ConcurrentDictionary аналогичен обычному Dictionary,
        //но позволяет одновременный безопасный доступ из разных потоков (параллельный доступ и выполнение нескольких операций одновременно).
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        //Метод GetSession(...) работает с хранилищем сессий, позволяя нам при подключении клиента создать новую сессию или обновить существующую.
        public Session GetSession(long chatId)
        {
            // Возвращаем сессию по ключу, если она существует
            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];

            // Создаем и возвращаем новую, если такой не было
            var newSession = new Session() { LanguageCode = "ru" };
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}
