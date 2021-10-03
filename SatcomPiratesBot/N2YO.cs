using Flurl.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatcomPiratesBot
{
    class N2YO
    {
        const string BaseUrl = "https://api.n2yo.com/rest/v1/satellite";
        private readonly ConfigModel config;
        private static (string name, string id)[] GetSatellites()
        {
            try
            {
                return System.IO.File.ReadAllLines("satellites.txt").Select(x =>
                {
                    var items = x.Split(',');
                    return (items[0].Trim(), items[1].Trim());
                }).ToArray();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Cannot load satellites.txt");
                return Array.Empty<(string name, string id)>();
            }
        }

        public N2YO(ConfigModel config)
        {
            this.config = config;
        }

        private async Task<(string satname, string tle)> GetTle(string satid)
        {
            string TLE = $"{BaseUrl}/tle/{satid}&apiKey={config.N2YOApiKey}";
            var response = await TLE.GetAsync().ReceiveJson();
            return (response?.info?.satname, response?.tle);
        }


        public async Task<string> PrepareTLE()
        {
            List<string> lines = new();

            foreach (var (sat, id) in GetSatellites())
            {
                var satName = sat;
                var filename = $"tle\\{DateTime.Today.ToString("yyyyMMdd")}.{id}.tle";
                var tle = "";
                if (System.IO.File.Exists(filename))
                {
                    tle = System.IO.File.ReadAllText(filename);
                }
                else
                {
                    (satName, tle) = await GetTle(id);
                    try
                    {
                        System.IO.File.WriteAllText(filename, tle);
                    }
                    catch { }
                }
                lines.Add($"{satName}:");
                lines.Add(tle);
                lines.Add("");
            }

            return string.Join("\r\n", lines);
        }

        public static string Link
            => $"https://www.n2yo.com///?s={string.Join("|", GetSatellites().Select(x => x.id))}";
    }
}
