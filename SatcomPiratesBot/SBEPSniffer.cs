using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatcomPiratesBot
{
    class SBEPSniffer
    {
        private Dictionary<string, string> lines = new Dictionary<string, string>();

        private Dictionary<string, DateTime> scanState = new Dictionary<string, DateTime>();

        public IEnumerable<string> ScanState => scanState
            .Where(x=>!x.Key.ToLower().Contains("скан"))
            .OrderByDescending(x => x.Value)
            .Take(6)            
            .Select(x => {
                var diff = DateTime.Now - x.Value;
                var diffstr = diff.TotalSeconds > 5 ? $"{diff:hh\\:mm\\:ss} ago" : "active";
                return $"{x.Key} {diffstr}";
            });


        public event EventHandler<bool> SquelchUpdate;
        public event EventHandler<int> SMeter;
        public event EventHandler<string> DisplayChange;
        public event EventHandler<string> RawUpdate;
        public void Subscribe(IEnumerable<byte[]> bytesSeq)
        {
            try
            {
                foreach (var bytes in bytesSeq)
                {
                    var bytesAsString = BitConverter.ToString(bytes);
                    if (OpenSquelch(bytesAsString))
                    {
                        if (scanState.Count > 0)
                        {
                            var lastActiveFreq = scanState.OrderByDescending(x => x.Value).FirstOrDefault();
                            scanState[lastActiveFreq.Key] = DateTime.Now;
                        }

                        SquelchUpdate?.Invoke(this, true);
                    }
                    else if (CloseSquelch(bytesAsString))
                    {
                        SquelchUpdate?.Invoke(this, false);
                    }
                    else if (DisplayUpdate(bytesAsString))
                    {
                        var startIndex = bytesAsString.IndexOf(DisplayPrefix);
                        var addr = bytesAsString.Substring(startIndex + DisplayPrefix.Length - 1, 5);
                        var subArray = bytes.Skip(6).Take(bytes.Length - 8).ToArray();
                        lines[addr] = ExcludeSpecificChars(subArray);
                        var key = string.Join("\r\n", lines.Values).Trim().TrimStart('4')
                            .Split('_')[0];// <-sometimes RSSI is mixed with display data
                        scanState[key] = DateTime.Now;
                        DisplayChange?.Invoke(this, key);
                    }
                    var usefulChars = ExcludeSpecificChars(bytes);
                    if (usefulChars.StartsWith("_"))
                    {
                        SMeter?.Invoke(this, int.Parse(usefulChars.TrimStart('_').TrimEnd('P')));
                        continue;
                    }
                    RawUpdate?.Invoke(this, $"{bytesAsString} [{usefulChars}]");
                }

            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Cannot handle bytes");
            }
        }

        private string ExcludeSpecificChars(byte[] bytes) =>

            new string(Encoding.GetEncoding(866).GetString(bytes)
                                  .Where(x => char.IsLetterOrDigit(x) || char.IsSeparator(x) || char.IsPunctuation(x)).ToArray());


        private const string DisplayPrefix = "FF-34-00-";
        private static bool DisplayUpdate(string bytes) => bytes.Contains(DisplayPrefix);

        private static bool CloseSquelch(string bytes) => bytes.StartsWith("F5-35-03-FF");

        private static bool OpenSquelch(string bytes) => bytes.StartsWith("F5-35-00-3F");

    }
}
