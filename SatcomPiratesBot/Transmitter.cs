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
        static Vox vox;
        public static Vox Vox
        {
            get
            {
                if (vox == null)
                {
                    vox = new Vox();
                    vox.StateChanged += (s, e) =>
                    {
                        Serilog.Log.Warning(Vox.IsOpened ? nameof(PTT) : nameof(STOP_PTT));
                        ComPort.WriteLine(Vox.IsOpened ? PTT : STOP_PTT);
                    };
                }
                return vox;
            }
        }
    }
}
