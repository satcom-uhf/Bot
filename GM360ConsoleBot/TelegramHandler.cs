// See https://aka.ms/new-console-template for more information

using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.Enums;

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
}