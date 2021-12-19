namespace SatcomPiratesBot
{
    public class ConfigModel
    {
        public string TelegramToken { get; set; }
        public string N2YOApiKey { get; set; }
        public string SSTVPath { get; set; } = @"C:\RX-SSTV\History";
        public string SSTVChannel { get; set; } = "@SATCOM_BOARD";
        public long PrimaryGroup { get; set; }
        public string DTMFCode { get; set; }
        public string WitAiToken { get; internal set; }
    }
}
