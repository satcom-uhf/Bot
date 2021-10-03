using DtmfDetection;
using DtmfDetection.NAudio;
using NAudio.Wave;
using System;

namespace SatcomPiratesBot
{
    class DTMF
    {
        private static BackgroundAnalyzer analyzer;
        private static DateTime lastDtmf;
        private static string dtmfBuffer = "";
        public static event EventHandler Detected;
        public static event EventHandler<string> Changed;
        public static TimeSpan NotOftenThan { get; set; } = TimeSpan.FromSeconds(10);
        public static void StartDetection(ConfigModel config)
        {
            var audioSource = new WaveInEvent
            {
                WaveFormat = new WaveFormat(Config.Default.SampleRate, bits: 32, channels: 1)
            };

            analyzer = new BackgroundAnalyzer(
                audioSource, config: Config.Default.WithThreshold(10));

            analyzer.OnDtmfDetected += dtmf =>
            {
                if (string.IsNullOrEmpty(config.DTMFCode)) return;
                if (dtmf.IsStop)
                {
                    var key = dtmf.Key.ToSymbol();
                    dtmfBuffer += key;
                    dtmfBuffer = dtmfBuffer.Substring(Math.Max(0, dtmfBuffer.Length - config.DTMFCode.Length));
                    if (dtmfBuffer == config.DTMFCode)
                    {
                        var diff = DateTime.Now - lastDtmf;
                        if (diff > NotOftenThan)
                        {
                            lastDtmf = DateTime.Now;
                            Serilog.Log.Information("DTMF Detected");
                            Detected?.Invoke(null, EventArgs.Empty);
                        }
                        else
                        {
                            Serilog.Log.Warning("DTMF DDOS detected");
                        }
                        dtmfBuffer = "";
                    }
                    else if (dtmfBuffer.Length == config.DTMFCode.Length)
                    {
                        dtmfBuffer = "";
                    }
                    Changed?.Invoke(null, dtmfBuffer);
                }
            };
        }

    }
}
