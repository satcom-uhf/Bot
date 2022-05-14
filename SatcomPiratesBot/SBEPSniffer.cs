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
            .OrderByDescending(x => x.Value)
            .Take(4)
            .Select(x =>
            {
                var parts = x.Key.Split(' ');
                if (int.TryParse(parts[0], out var chNumber))
                {
                    var diff = DateTime.Now - x.Value;
                    var diffstr = diff.TotalSeconds > 5 ? $"{diff:hh\\:mm\\:ss} ago" : "       ";
                    return $"{x.Key} {diffstr}";
                }
                return x.Key;
            });

        public bool ScanEnabled { get; set; } = true;
        public event EventHandler<bool> SquelchUpdate;
        public event EventHandler ScanChanged;
        public event EventHandler<int> SMeter;
        public event EventHandler<string> HistoryChanged;
        public event EventHandler<string> RawUpdate;
        public void Subscribe(IEnumerable<byte[]> bytesSeq)
        {
            try
            {
                foreach (var bytes in bytesSeq)
                {
                    if (bytes.Length == 3 && bytes[0] == 0x16)
                    {
                        var lowByte = bytes[2];
                        var highByte = bytes[1];
                        var sMeter = (highByte << 8) + lowByte;
                        SMeter?.Invoke(this, sMeter);
                        continue;
                    }
                    if(Find(bytes, Confirmation, out var _))
                    {
                        continue;
                    }
                    else if (Find(bytes, OpenSquelch, out var pos))
                    {
                        if (scanState.Count > 0)
                        {
                            var lastActiveFreq = scanState.OrderByDescending(x => x.Value).FirstOrDefault();
                            scanState[lastActiveFreq.Key] = DateTime.Now;
                        }

                        SquelchUpdate?.Invoke(this, true);
                    }
                    else if (Find(bytes, CloseSquelch, out pos))
                    {
                        SquelchUpdate?.Invoke(this, false);
                    }
                    else if (Find(bytes, DisplayPrefix, out pos))
                    {
                        var x = bytes.Skip(pos + DisplayPrefix.Length);
                        var subArray = x.ToArray();
                        //var addr = x.Take(2).ToArray();
                        if (Find(subArray, DisplayPrefix, out var duplicatePos))
                        {
                            subArray = subArray.Skip(duplicatePos + DisplayPrefix.Length).ToArray();
                        }
                        //lines[Convert.ToHexString(addr)] = ExcludeSpecificChars(subArray);
                        var singleLine = ExcludeSpecificChars(subArray);
                        var parts = singleLine.Split(' ');
                        var key = string.Join(" ", parts.Take(2)).Trim();
                        scanState[key] = DateTime.Now;
                        HistoryChanged?.Invoke(this, key);
                    }
                    else if (Find(bytes, ScanOn, out pos))
                    {
                        ScanEnabled = true;
                        ScanChanged?.Invoke(this, EventArgs.Empty);
                    }
                    else if (Find(bytes, ScanOff, out pos))
                    {
                        ScanEnabled = false;
                        ScanChanged?.Invoke(this, EventArgs.Empty);
                    }
                    var usefulChars = ExcludeSpecificChars(bytes);
                    var bytesAsString = BitConverter.ToString(bytes);
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

        private static byte[] DisplayPrefix = new byte[] { 0xFF, 0x34, 0x00 };
        private static byte[] CloseSquelch = new byte[] { 0xF5, 0x35, 0x03, 0xFF };
        private static byte[] OpenSquelch = new byte[] { 0xF5, 0x35, 0x00, 0x3F };
        private static byte[] ScanOn = new byte[] { 0xF4, 0x24, 0x0A, 0x00, 0x00, 0xDD };
        private static byte[] ScanOff = new byte[] { 0xF4, 0x24, 0x0A, 0x01, 0x00, 0xDC };
        private static byte[] Confirmation = new byte[] { 0xF2, 0x36, 0x00, 0xD7 };
        private static bool Find(byte[] bytes, byte[] pattern, out int pos)
        {
            pos = SearchBytes(bytes, pattern);
            return pos >= 0;
        }
        static int SearchBytes(byte[] haystack, byte[] needle)
        {
            var len = needle.Length;
            var limit = haystack.Length - len;
            for (var i = 0; i <= limit; i++)
            {
                var k = 0;
                for (; k < len; k++)
                {
                    if (needle[k] != haystack[i + k]) break;
                }
                if (k == len) return i;
            }
            return -1;
        }

    }
}
