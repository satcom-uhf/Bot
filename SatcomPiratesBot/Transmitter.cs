using NAudio.Wave;
using System;
using System.IO.Ports;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SatcomPiratesBot
{
    public static class Transmitter
    {
        public static SerialPort ComPort { get; set; }
        private const string PTT = "t";
        private const string STOP_PTT = "s";
        public static bool ChannelBusy { get; set; }
        public static bool TransmitterBusy { get; set; }
        public static void Transmit(this ITelegramBotClient botClient, Chat chat, int duration, string wavFile) => Task.Run(async () =>
          {
              try
              {
                  var now = DateTime.Now;
                  var waitFor = TimeSpan.FromSeconds(30);
                  while (ChannelBusy && (DateTime.Now - now < waitFor))
                  {                      
                      await Task.Delay(TimeSpan.FromMilliseconds(100));
                  }
                  if (ChannelBusy)
                  {
                      await botClient.SendTextMessageAsync(chat, "Я ждал, но канал так и не освободился. Попробуйте еще раз");
                      return;
                  }
                  await botClient.SendTextMessageAsync(chat, "Сообщение передается...");
                  ComPort.WriteLine(PTT);
                  using (var audioFile = new WaveFileReader(wavFile))
                  using (var outputDevice = new WasapiOut())
                  {
                      outputDevice.Init(audioFile);
                      outputDevice.Play();
                      while (outputDevice.PlaybackState == PlaybackState.Playing)
                      {
                          await Task.Delay(100);
                      }
                  }
                  //await Task.Delay(TimeSpan.FromSeconds(duration + 1));
                  await botClient.SendTextMessageAsync(chat, "Сообщение передано");
              }
              finally
              {
                  ComPort.WriteLine(STOP_PTT);
                  TransmitterBusy = false;
              }
          });
    }
}
