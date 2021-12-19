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

        public event EventHandler<bool> SquelchUpdate;
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
                        DisplayChange?.Invoke(this, string.Join("\r\n", lines.Values));
                    }
                    var usefulChars = ExcludeSpecificChars(bytes);
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
