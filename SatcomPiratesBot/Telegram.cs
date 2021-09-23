using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SatcomPiratesBot
{
    using static InlineKeyboardButton;
    static class Telegram
    {
        private static TelegramBotClient Bot;
        public static ChatMember[] Admins = new ChatMember[] { };

        public static async Task Start(string token, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Starting bot");
                Bot = new TelegramBotClient(token);
                Bot.StartReceiving<TelegramHandler>(cancellationToken);
                await Bot.GetMeAsync();
                Log.Information("Getting admins");
                Admins = await Bot.GetChatAdministratorsAsync("@SATCOM_UHF");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Cannot start bot");
            }
        }
        public static IEnumerable<IEnumerable<InlineKeyboardButton>> InlineKeyboard(User user)
        {
            // first row
            yield return new[]            {
                         WithCallbackData("📶 Active frequencies / Активные частоты", TelegramCommands.Freq)
                    };
            yield return new[]
            {
                        WithCallbackData("🎤 Record your voice / Записать свой голос", TelegramCommands.SoundRecord)
                    };
            //yield return new[]
            //        {
            //            WithCallbackData("🔭 TLE", "/tle"), WithUrl("🌐 n2yo map", N2YO.Link)
            //        };

            yield return new[]
                    {
                        WithUrl("☠️ Satcom Pirates", "https://t.me/SATCOM_UHF")
                    };
            yield return new[]
                    {
                        WithUrl("📻 WebSDR от Nano", "http://171.25.164.45:3000/")
                    };
            if (Admins.Any(x => x.User.Id == user.Id))
            {
                yield return new[] { WithCallbackData("Управление станцией", TelegramCommands.Qyt) };
            }
            //new []
            //{
            //    WithUrl("Donate / Поддержать", "https://sobe.ru/na/satcom_uhf"),
            //}

        }

        public static IEnumerable<IEnumerable<InlineKeyboardButton>> QytKeyboard()
        {
            // first row
            yield return new[]            {
                        WithCallbackData("🔼   Up", $"{TelegramCommands.Qyt}+"),
                        WithCallbackData("✅ Menu", $"{TelegramCommands.Qyt}_m")
                    };
            yield return new[]
                    {
                        WithCallbackData("🔽 Down", $"{TelegramCommands.Qyt}-"),
                        WithCallbackData("🔠 Exit", $"{TelegramCommands.Qyt}_e")
                    };
            yield return new[]
                    {
                        WithCallbackData("Закрыть управление", "/start"),
                    };
        }

        public static async Task SendInlineKeyboard(this ITelegramBotClient bot, ChatId chat, User user)
        {
            await bot.SendTextMessageAsync(
                chatId: chat,
                text: "Select an action / Выберите действие",
                replyMarkup: new InlineKeyboardMarkup(InlineKeyboard(user)),
                disableNotification: true
            );
        }
    }
}
