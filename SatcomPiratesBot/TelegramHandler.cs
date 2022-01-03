using Serilog;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Extensions.Polling;
using System.Threading;
using System.IO;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using System.Linq;
using Telegram.Bot.Exceptions;

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
                    var isAdmin = await from.IsAdmin(botClient);
                    await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                    await botClient.SendChatActionAsync(message.Chat, ChatAction.Typing);
                    //try
                    //{
                    //    await botClient.DeleteMessageAsync(message.Chat, message.MessageId); // try to delete previous message                        
                    //}
                    //catch { }
                    Log.Information("Callback {Data} from {User} ({FirstName},{LastName})", callbackQuery.Data, from, from.FirstName, from.LastName);
                    if (callbackQuery.Data == TelegramCommands.Freq)
                    {
                        if (await from.IsPirate(botClient))
                        {
                            await HandleFreq(botClient, callbackQuery.Message, callbackQuery.From);
                        }
                        else
                        {
                            await Sorry(botClient, message.Chat);
                        }
                    }
                    else if (callbackQuery.Data.StartsWith(TelegramCommands.GM360) && isAdmin)
                    {
                        await HandleGM360(botClient, callbackQuery);
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
                    
                }
                else if (update.Message is Message message)
                {
                    if (await message.From.IsPirate(botClient))
                    {
                        Log.Information("Message from  pirate {User} ({FirstName},{LastName})", message.From, message.From.FirstName, message.From.LastName);
                        await HandleFreq(botClient, message, message.From);
                    }
                    else
                    {
                        Log.Information("Message from unknown {User} ({FirstName},{LastName})", message.From, message.From.FirstName, message.From.LastName);
                        await botClient.SendInlineKeyboard(message.Chat, message.From);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Cannot handle update");
            }
        }

        private async Task Sorry(ITelegramBotClient botClient, ChatId chat)
        {
            await botClient.SendTextMessageAsync(chat, "Sorry. You are not member of Satcom Pirates. /  Сожалеем, но вы не являетесь членом Satcom Pirates.");
        }
        private async Task HandleGM360(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            try
            {
                var command = callbackQuery.Data.Replace(TelegramCommands.GM360, "");
                Log.Information($"SEND:{command}");
                Transmitter.ComPort.Write(command);
                await Task.Delay(TimeSpan.FromMilliseconds(2000)); // let's wait a bit
                await SendRadioScreen(botClient,
                    callbackQuery.Message,
                    new InlineKeyboardMarkup(Telegram.RadioKeyboard())
                    );
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Cannot handle command");
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat, "Oops...command error =( ");
            }
        }

        private async Task HandleFreq(ITelegramBotClient botClient, Message message, User from)
        {
            await botClient.SendChatActionAsync(message.Chat, ChatAction.Typing);
            var isAdmin = await from.IsAdmin(botClient);
            await SendRadioScreen(botClient, message.Chat, new InlineKeyboardMarkup(Telegram.InlineKeyboard(from, isAdmin)));
        }

        private async Task SendRadioScreen(ITelegramBotClient botClient, Chat chat, InlineKeyboardMarkup replyMarkup)
        {
            await botClient.SendTextMessageAsync(
          chat,
           string.Join("\r\n", MainForm.Sniffer.ScanState),
           disableNotification: true,
          replyMarkup: replyMarkup
          );
        }

        private async Task SendRadioScreen(ITelegramBotClient botClient, Message msg, InlineKeyboardMarkup replyMarkup)
        {
            await botClient.EditMessageTextAsync(
                msg.Chat,
                msg.MessageId,
                MainForm.Sniffer.ScanState.FirstOrDefault()??"____",
                replyMarkup: replyMarkup
                );
        }
    }
}
