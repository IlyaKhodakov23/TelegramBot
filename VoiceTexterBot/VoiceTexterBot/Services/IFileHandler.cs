using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceTexterBot.Services
{
    //Добавим новый сервис, который будет в нашем приложении отвечать за обработку файлов.
    //Проектирование начнём с интерфейса:
    public interface IFileHandler
    {
        //Метод Download(...) будет отвечать за первичное скачивание файла.
        //Скачивание — это длительная асинхронная операция, поэтому он будет возвращать объект Task,
        //а принимать идентификатор fileId и уже знакомый вам токен отмены CancellationToken.
        Task Download(string fileId, CancellationToken ct);
        //Второй метод интерфейса — Process(...) — будет обрабатывать файл (конвертировать и распознавать).
        string Process(string param);
    }
}
