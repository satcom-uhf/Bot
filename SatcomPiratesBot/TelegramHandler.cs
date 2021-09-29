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
                    else if (callbackQuery.Data == TelegramCommands.Tle)
                    {
                        var n2yo = new N2YO(MainForm.Config);
                        var tle = await n2yo.PrepareTLE();
                        if (!string.IsNullOrEmpty(tle))
                        {
                            using var stream = new MemoryStream();
                            var writer = new StreamWriter(stream);
                            writer.Write(tle);
                            writer.Flush();
                            stream.Position = 0;
                            await botClient.SendDocumentAsync(message.Chat,
                                new InputOnlineFile(stream, $"{DateTime.Now.ToString("yyyy-MM-dd")}.txt"),
                                disableNotification: true
                                );
                        }
                    }
                    else if (callbackQuery.Data == TelegramCommands.EnableVox)
                    {
                        var TOT = TimeSpan.FromMinutes(5);
                        Transmitter.Vox.Start(TOT, cancellationToken);
                        var msg = $"VOX активирован и будет автоматически выключен через {(Transmitter.Vox.StopTime - DateTime.Now).ToString(@"mm\:ss")}";
                        await SendRadioScreen(botClient,
                    callbackQuery.Message.Chat,
                    msg,
                    new InlineKeyboardMarkup(Telegram.QytKeyboard())
                    );
                    }
                    else if (callbackQuery.Data == TelegramCommands.DisableVox)
                    {
                        Transmitter.Vox.Stop();
                        var msg = $"VOX деактивирован";
                        await SendRadioScreen(botClient,
                    callbackQuery.Message.Chat,
                    msg,
                    new InlineKeyboardMarkup(Telegram.QytKeyboard())
                    );
                    }
                }
                else if (update.Message is Message message)
                {
                    Log.Information("Message from {User} ({FirstName},{LastName})", message.From, message.From.FirstName, message.From.LastName);
                    await botClient.SendInlineKeyboard(message.Chat, message.From);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Cannot handle update");
            }
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
