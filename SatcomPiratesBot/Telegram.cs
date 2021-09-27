﻿using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace SatcomPiratesBot
{
    using static InlineKeyboardButton;
    using static TelegramCommands;
    using static QytCommands;
    static class Telegram
    {
        private static TelegramBotClient Bot;
        public static ChatMember[] Admins = new ChatMember[] { };
        private static FileSystemWatcher SstvSpy = new FileSystemWatcher();
        
        public static async Task Start(ConfigModel config, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Starting bot");
                Bot = new TelegramBotClient(config.TelegramToken);
                Bot.StartReceiving<TelegramHandler>(cancellationToken);
                await Bot.GetMeAsync();
                Log.Information("Getting admins");
                Admins = await Bot.GetChatAdministratorsAsync(config.MainDiscussuionGroup);
                Log.Information("Starting SSTV Spy");
                StartSstvSpy(config);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Cannot start bot");
            }
        }

        private static void StartSstvSpy(ConfigModel config)
        {
            SstvSpy.Path = config.SSTVPath;
            SstvSpy.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;
            SstvSpy.Created += async (s, e) =>
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(2));
                    await SendPhotoToChannel(new ChatId(config.SSTVChannel), e.FullPath);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "SSTV error");
                }
            };
            SstvSpy.Filter = "*.jpg";
            SstvSpy.Error += (s, e) => Log.Error(e?.GetException(), $"Watcher error");
            SstvSpy.EnableRaisingEvents = true;
        }
        private static async Task SendPhotoToChannel(ChatId channel, string path)
        {
            using (var fs = System.IO.File.OpenRead(path))
            {
                await Bot.SendPhotoAsync(channel, new InputOnlineFile(fs));
            }
        }

        public static IEnumerable<IEnumerable<InlineKeyboardButton>> InlineKeyboard(User user)
        {
            // first row
            yield return new[]            {
                         WithCallbackData("📶 Active frequencies / Активные частоты", Freq)
                    };
            yield return new[]
            {
                        WithCallbackData("🎤 Record your voice / Записать свой голос", SoundRecord)
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
            if (user.IsAdmin())
            {
                yield return new[] {
                    WithCallbackData("Управление станцией", Qyt),
                    WithCallbackData("Подудеть", TransmitVoice)
                };
            }
            //new []
            //{
            //    WithUrl("Donate / Поддержать", "https://sobe.ru/na/satcom_uhf"),
            //}

        }

        public static bool IsAdmin(this User user) => Admins.Any(x => x.User.Id == user.Id);

        public static IEnumerable<IEnumerable<InlineKeyboardButton>> QytKeyboard()
        {
            var stepsToDisableTmr = Menu + Menu + Up + Menu + Exit;
            var stepsToEnableTmr = Menu + Menu + Down + Menu + Exit;
            // first row
            yield return new[]
                    {
                        WithCallbackData("(!)Отключить TMR", $"{Qyt}{stepsToDisableTmr}"),
                    };
            yield return new[]
                    {
                        WithCallbackData("(!)Включить TMR",$"{Qyt}{stepsToEnableTmr}"),
                    };
            yield return new[]            {
                        WithCallbackData("🔼   Up", $"{Qyt}{Up}"),
                        WithCallbackData("✅ Menu", $"{Qyt}{Menu}")
                    };
            yield return new[]
                    {
                        WithCallbackData("🔽 Down", $"{Qyt}{Down}"),
                        WithCallbackData("🔠 Exit", $"{Qyt}{Exit}")
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
