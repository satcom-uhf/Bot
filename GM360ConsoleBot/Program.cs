// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Serilog;
using System.IO.Ports;
using Telegram.Bot;
Log.Logger = new LoggerConfiguration()
               .WriteTo.Console()
               .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
               .CreateLogger();
IConfiguration cfg = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
var portName = cfg["port"];
var serial = new SerialPort(portName, 115200);
serial.Open();
Log.Information("Port {port} opened", portName);
var bot = new TelegramBotClient(cfg["token"]);
bot.StartReceiving<TelegramHandler>();
await bot.GetMeAsync();
Log.Information("Bot started");
