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
            if (update.Type == UpdateType.CallbackQuery)
            {
                var callbackQuery = update.CallbackQuery;
                var message = callbackQuery.Message;
                Log.Information(callbackQuery.Data);
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
            }
            else if (update.Message is Message message)
            {
                Log.Information("Message from {User} ({FirstName},{LastName})", message.From, message.From.FirstName, message.From.LastName);
                await botClient.SendInlineKeyboard(message.Chat, message.From);
            }
        }

        private async Task HandleQyt(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            await SendRadioScreen(botClient,
                callbackQuery.Message.Chat,
                "Radio screen",
                new InlineKeyboardMarkup(Telegram.QytKeyboard())
                );
        }

        private async Task HandleFreq(ITelegramBotClient botClient, CallbackQuery query)
        {
            var message = query.Message;
            var from = query.From;
            await botClient.SendChatActionAsync(message.Chat, ChatAction.Typing);
            var activity = "";
            try
            {
                activity = await MainForm.Mask.Resize().RecognizeImage();
            }
            catch { }
            activity = activity.Replace("\r", "").Replace("\n", "");
            //var (msgText, ) = await RecognizeImage(screen);
            //var lines = msgText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Where(x => x.Length > 3);
            //msgText = string.Join("\r\n", lines);
            if (!string.IsNullOrEmpty(activity))
            {
                activity = $"[{activity}] {(DateTime.Now - MainForm.LastActivity).ToString(@"hh\:mm\:ss")} ago";
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
