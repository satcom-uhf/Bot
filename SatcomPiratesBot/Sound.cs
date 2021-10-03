using Flurl;
using Flurl.Http;
using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SatcomPiratesBot
{
    public static class Sound
    {
        public const string RecordMp3 = "record.mp3";
        public static async Task PlayOK()
        {
            using (var audioFile = new WaveFileReader("OK.wav"))
            using (var outputDevice = new WasapiOut())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    await Task.Delay(1000);
                }
            }
        }

        public static async Task<(bool, string)> RecordVoice()
        {
            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, "MyBot", out createdNew))
            {

                if (!createdNew)
                {
                    Serilog.Log.Warning("Recording is busy");
                    return (false, null);
                }
                if (System.IO.File.Exists(RecordMp3))
                {
                    System.IO.File.Delete(RecordMp3);
                }
                var waveInStream = new WaveInEvent();
                waveInStream.WaveFormat = new WaveFormat(41000, 16, 1);
                using (var writer = new LameMP3FileWriter(RecordMp3, waveInStream.WaveFormat, LAMEPreset.EXTREME_FAST))
                {
                    waveInStream.DataAvailable += (s, e) =>
                    {
                        writer.Write(e.Buffer, 0, e.BytesRecorded);
                    };
                    waveInStream.StartRecording();
                    await Task.Delay(TimeSpan.FromSeconds(5));
                    var frame = MainForm.Mask.Resize();
                    await Task.Delay(TimeSpan.FromSeconds(5));
                    waveInStream.StopRecording();
                    try
                    {
                        var freq = frame.RecognizeImage();
                        return (true, freq);
                    }
                    catch
                    {
                        return (true, "Not detected");
                    }
                }
            }
        }

        public static async Task<string> RecognizeMessage(ConfigModel config)
        {
            try
            {
                using (var fs = System.IO.File.OpenRead(RecordMp3))
                {
                    var response = await "https://api.wit.ai"
                    .AppendPathSegment("speech")
                   .WithOAuthBearerToken(config.WitAiToken)
                   .WithHeader("Content-Type", "audio/mpeg3")
                   .PostAsync(new StreamContent(fs))
                   .ReceiveJson();
                    string result = response.text;
                    return result;
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Cannot recognize voice");
                return null;
            }
        }
    }
}
