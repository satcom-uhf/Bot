using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace SatcomPiratesBot
{
    using static InlineKeyboardButton;
    using static TelegramCommands;
    using static GM360Commands;
    static class Telegram
    {
        private static TelegramBotClient Bot;
        public static ChatId PrimaryGroup { get; set; }
        private static FileSystemWatcher SstvSpy = new FileSystemWatcher();

        public static async Task Start(ConfigModel config, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Starting bot");
                Bot = new TelegramBotClient(config.TelegramToken);
                PrimaryGroup = new ChatId(config.PrimaryGroup);
                Bot.StartReceiving<TelegramHandler>(cancellationToken);
                await Bot.GetMeAsync();
                Log.Information("Starting SSTV Spy");
                StartSstvSpy(config);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Cannot start bot");
            }
        }

        public static async Task SendVoiceMessageToChannel(ConfigModel config, string frequency)
        {
            var text = await Sound.RecognizeMessage(config);
            await SendVoiceMessageTo(new ChatId(config.SSTVChannel), $"[{frequency}]  {text}");
        }

        public static async Task SendVoiceMessageTo(ChatId chat, string title = null)
        {
            using (var fs = System.IO.File.OpenRead(Sound.RecordMp3))
            {
                await Bot.SendVoiceAsync(chat, new InputOnlineFile(fs), caption: title);
            }
        }
        public static async Task<bool> IsPirate(this User from, ITelegramBotClient botClient)
        {
            var valid = false;
            try
            {
                var member = await botClient.GetChatMemberAsync(PrimaryGroup, from.Id);
                Log.Information("Status is {Status}", member.Status);
                valid = member.Status == ChatMemberStatus.Administrator
                    || member.Status == ChatMemberStatus.Creator
                    || member.Status == ChatMemberStatus.Member;                
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Can't check membership");
            }
            return valid;
        }
        public static async Task<bool> IsAdmin(this User from, ITelegramBotClient botClient)
        {
            var valid = false;
            try
            {
                var member = await botClient.GetChatMemberAsync(PrimaryGroup, from.Id);
                valid = member.Status == ChatMemberStatus.Creator 
                    || (member.Status == ChatMemberStatus.Administrator && member.CanRestrictMembers == true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Can't check membership");
            }
            return valid;
        }
        public static async Task SendInlineKeyboard(this ITelegramBotClient bot, ChatId chat, User user)
        {
            var isAdmin = await user.IsAdmin(bot);
            await bot.SendTextMessageAsync(
                chatId: chat,
                text: "Select an action / Выберите действие",
                replyMarkup: new InlineKeyboardMarkup(InlineKeyboard(user, isAdmin)),
                disableNotification: true
            );

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

        public static IEnumerable<IEnumerable<InlineKeyboardButton>> InlineKeyboard(User user, bool isAdmin)
        {
            // first row
            yield return new[]            {
                         WithCallbackData("📶 Active frequencies / Активные частоты", Freq)
                    };
            //yield return new[]
            //{
            //            WithCallbackData("🎤 Record your voice / Записать свой голос", SoundRecord)
            //        };

            yield return new[]
                    {
                        WithCallbackData("🔭 TLE", Tle), WithUrl("🌐 n2yo map", N2YO.Link)
                    };

            yield return new[]
                    {
                        WithUrl("☠️ Satcom Pirates", "https://t.me/SATCOM_UHF")
                    };
            yield return new[]
                    {
                        WithUrl("📻 WebSDR от @Nano_VHF", "http://sdr.rlspb.ru:3000/")
                    };
            if (isAdmin)
            {
                yield return new[] {
                    WithCallbackData("Управление станцией", GM360)
                };
            }
            //new []
            //{
            //    WithUrl("Donate / Поддержать", "https://sobe.ru/na/satcom_uhf"),
            //}

        }
        
        public static IEnumerable<IEnumerable<InlineKeyboardButton>> RadioKeyboard()
        {
            // first row
            //var voxActive = Transmitter.Vox.Running;
            //yield return new[]
            //{
            //    WithCallbackData(voxActive?"Выключить VOX":"Включить VOX", voxActive?DisableVox:EnableVox)
            //};
            yield return new[]            {
                        WithCallbackData("P1", $"{GM360}{P1}"),
                        WithCallbackData("⎋", $"{GM360}{Exit}"),
                        WithCallbackData("🔼", $"{GM360}{Up}"),
                        WithCallbackData("✅", $"{GM360}{Ok}"),
                        WithCallbackData("P3", $"{GM360}{P3}"),

                    };
            yield return new[]
                    {
                        WithCallbackData("P2", $"{GM360}{P1}"),
                        WithCallbackData("◀️", $"{GM360}{Left}"),
                        WithCallbackData("🔽", $"{GM360}{Down}"),
                        WithCallbackData("▶️ ", $"{GM360}{Right}"),
                        WithCallbackData("P4", $"{GM360}{P4}")
                    };
            yield return new[]
                   {
                        WithCallbackData("Закрыть управление", Freq),
                    };


        }
    }
}
