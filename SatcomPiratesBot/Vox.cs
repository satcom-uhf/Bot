using NAudio.CoreAudioApi;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatcomPiratesBot
{
    public class Vox
    {
        private readonly MMDevice device;
        public DateTime StopTime { get; private set; }
        public bool IsOpened { get; private set; }
        public bool Running { get; private set; }
        public event EventHandler StateChanged;
        public static int Sensitivity { get; set; } = 2;
        public Vox()
        {
            var enumerator = new MMDeviceEnumerator();
            device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
        }
        private bool SoundDetected => Convert.ToInt32(device.AudioMeterInformation.MasterPeakValue * Sensitivity) > 0;
        public void Start(TimeSpan maxTimeLimit, CancellationToken cancellationToken)
        {
            if (!Running)
            {
                StopTime = DateTime.Now + maxTimeLimit;
                Running = true;

                Task.Run(async () =>
                {
                    while (!cancellationToken.IsCancellationRequested && Running && (DateTime.Now < StopTime))
                    {
                        await UpdateState(SoundDetected);
                    }
                    IsOpened = false;
                    StateChanged?.Invoke(this, EventArgs.Empty);
                    Running = false;
                    Serilog.Log.Information("Vox stopped");

                });
            }
        }

        private async Task UpdateState(bool newState)
        {
            try
            {
                if (IsOpened != newState)
                {
                    if (IsOpened)
                    {
                        var waitUntil = DateTime.Now + TimeSpan.FromSeconds(1);
                        while (DateTime.Now < waitUntil)
                        {
                            if (SoundDetected) return;
                        }
                    }
                    IsOpened = newState;
                    StateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
            catch
            {
                // ignored
            }
        }

        public void Stop() => Running = false;
    }
}
