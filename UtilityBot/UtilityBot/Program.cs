using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;
using UtilityBot.Configuration;
using UtilityBot.Controllers;
using UtilityBot.Services;


namespace UtilityBot
{
    internal class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            //И обновим метод ConfigureServices(...), добавив инициализацию конфигурации в его начало:
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());
            //Для того чтобы сделать хранилище сессий доступным всем компонентам приложения, его нужно добавить в контейнер зависимостей:
            services.AddSingleton<IStorage, MemoryStorage>();
            // Подключаем контроллеры сообщений и кнопок
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
            // Регистрируем объект TelegramBotClient c токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("5632358412:AAEA0WWVCi2OYraXTsei3WgOOq9VToT7J9k"));
            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
            services.AddSingleton<IFunction, Function>();
        }
        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            { 
                BotToken = "5632358412:AAEA0WWVCi2OYraXTsei3WgOOq9VToT7J9k"
            };
        }
    }
}