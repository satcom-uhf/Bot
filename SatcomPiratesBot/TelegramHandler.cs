using Serilog;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Extensions.Polling;
using System.Threading;
using System.IO;
using OpenCvSharp.Extensions;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using System.Linq;

namespace SatcomPiratesBot
{
    class TelegramHandler : IUpdateHandler
    {
        public UpdateType[] AllowedUpdates => new[] {
            UpdateType.CallbackQuery,
                    UpdateType.Message,
                    UpdateType.Poll,
                    UpdateType.PollAnswer};

        private const string README_TRANSMIT = "Я всегда готов дудеть. Убедись, что отключен TMR и выбран нужный канал. " +
                                               "После этого отправь мне голосовое сообщение, я передам его в эфир как только наступит пауза в разговоре.";

        public Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Log.Error(exception, "Telegram error handled");
            return Task.CompletedTask;
        }

        public async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                if (update.Type == UpdateType.CallbackQuery)
                {
                    var callbackQuery = update.CallbackQuery;
                    var message = callbackQuery.Message;
                    var from = callbackQuery.From;
                    await botClient.SendChatActionAsync(message.Chat, ChatAction.Typing);
                    try
                    {
                        await botClient.DeleteMessageAsync(message.Chat, message.MessageId); // try to delete previous message
                    }
                    catch
                    {
                        // ignore
                    }
                    Log.Information("Callback {Data} from {User} ({FirstName},{LastName})", callbackQuery.Data, from, from.FirstName, from.LastName);
                    if (callbackQuery.Data == TelegramCommands.Freq)
                    {
                        await HandleFreq(botClient, callbackQuery);
                    }
                    else if (callbackQuery.Data.StartsWith(TelegramCommands.Qyt))
                    {
                        await HandleQyt(botClient, callbackQuery);
                    }
                    else if (callbackQuery.Data == TelegramCommands.TransmitVoice)
                    {
                        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat, README_TRANSMIT);
                        await botClient.SendInlineKeyboard(message.Chat, callbackQuery.From);
                    }
                }
                else if (update.Message is Message message)
                {
                    if (message.Voice != null)
                    {
                        Log.Information("Voice from {User} ({FirstName},{LastName})", message.From, message.From.FirstName, message.From.LastName);
                        await HandleVoiceMessage(botClient, message);
                    }
                    else
                    {
                        Log.Information("Message from {User} ({FirstName},{LastName})", message.From, message.From.FirstName, message.From.LastName);
                        await botClient.SendInlineKeyboard(message.Chat, message.From);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Cannot handle update");
            }
        }

        private async Task HandleVoiceMessage(ITelegramBotClient botClient, Message message)
        {
            if (!message.From.IsAdmin())
            {
                await botClient.SendTextMessageAsync(message.Chat, "Функция доступна только админам");
                return;
            }
            var now = DateTime.Now;
            var waitFor = TimeSpan.FromSeconds(30);
            while (Transmitter.TransmitterBusy && (DateTime.Now - now < waitFor))
            {
                await botClient.SendTextMessageAsync(message.Chat, "Ждите, передатчик занят..");
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
            if (Transmitter.TransmitterBusy)
            {
                await botClient.SendTextMessageAsync(message.Chat, "Увы, передатчик пока не освободился, повторите попытку позже");
                return;
            }
            Transmitter.TransmitterBusy = true;
            await botClient.SendTextMessageAsync(message.Chat, "Сообщение принято обрабатывается...");
            var voice = message.Voice;
            var fileInfo = await botClient.GetFileAsync(voice.FileId);
            var currentDir = System.Windows.Forms.Application.StartupPath;
            var voiceDir = Path.Combine(currentDir, "voice");
            if (!Directory.Exists(voiceDir)) { Directory.CreateDirectory(voiceDir); }
            var fn = $"voice_{DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss")}.ogg";
            var fullPath = Path.Combine(voiceDir, fn);
            var outputWav = "audio.wav";
            var outputWavFulPath = Path.Combine(voiceDir, outputWav);

            using (var fs = System.IO.File.OpenWrite(fullPath))
            {
                await botClient.DownloadFileAsync(fileInfo.FilePath, fs);
            }
            using var fileopener = new System.Diagnostics.Process();

            fileopener.StartInfo.FileName = "cmd";
            fileopener.StartInfo.Arguments = $"/C ffmpeg -i voice\\{fn} voice\\{outputWav} -y";
            fileopener.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            fileopener.StartInfo.WorkingDirectory = currentDir;
            fileopener.Start();
            fileopener.WaitForExit();
            Transmitter.Transmit(botClient, message.Chat, voice.Duration, outputWavFulPath);
        }

        private async Task HandleQyt(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            try
            {
                var commands = callbackQuery.Data.Replace(TelegramCommands.Qyt, "");
                foreach (var cmd in commands)
                {
                    Transmitter.ComPort.WriteLine(cmd.ToString());
                    await Task.Delay(TimeSpan.FromMilliseconds(1000)); // let's wait a bit
                }
                await Task.Delay(TimeSpan.FromMilliseconds(500)); // let's wait a bit
                await SendRadioScreen(botClient,
                    callbackQuery.Message.Chat,
                    "Radio screen",
                    new InlineKeyboardMarkup(Telegram.QytKeyboard())
                    );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Cannot handle QYT command");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat, "Oops...command error =( ");
            }
        }

        private async Task HandleFreq(ITelegramBotClient botClient, CallbackQuery query)
        {
            var message = query.Message;
            var from = query.From;
            await botClient.SendChatActionAsync(message.Chat, ChatAction.Typing);
            var activity = "";
            try
            {
                activity = MainForm.Mask.Resize().RecognizeImage();
            }
            catch { }
            activity = activity.Replace("\r", "").Replace("\n", "");
            //var (msgText, ) = await RecognizeImage(screen);
            //var lines = msgText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Where(x => x.Length > 3);
            //msgText = string.Join("\r\n", lines);
            if (!string.IsNullOrEmpty(activity))
            {
                activity = $"[{activity}] {DateTime.Now - MainForm.LastActivity:hh\\:mm\\:ss} ago";
                activity = $"Last activity / Последняя активность {activity}\r\nPTT clicks detected / Отшлепов обнаружено: {MainForm.PttClickCounter}";
            }
            await SendRadioScreen(botClient, message.Chat, activity, new InlineKeyboardMarkup(Telegram.InlineKeyboard(from)));
        }

        private async Task SendRadioScreen(ITelegramBotClient botClient, Chat chat, string caption, InlineKeyboardMarkup replyMarkup)
        {
            var screen = MainForm.CurrentFrame.Resize();

            using (var s = new MemoryStream())
            {
                BitmapConverter.ToBitmap(screen).Save(s, System.Drawing.Imaging.ImageFormat.Png);
                s.Position = 0;
                await botClient.SendPhotoAsync(
              chat,
              new InputOnlineFile(s),
               caption,
               disableNotification: true,
              replyMarkup: replyMarkup
              );
                return;
            }
        }
    }
}
