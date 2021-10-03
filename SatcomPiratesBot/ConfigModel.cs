namespace SatcomPiratesBot
{
    class HSV
    {
        public int H { get; set; }
        public int S { get; set; }
        public int V { get; set; }
    }
    class ConfigModel
    {
        public HSV StartHSV { get; set; } = new HSV { H = 40, S = 40, V = 40 };
        public HSV EndHSV { get; set; } = new HSV { H = 70, S = 255, V = 255 };
        public string TelegramToken { get; set; }
        public string N2YOApiKey { get; set; }
        public string SSTVPath { get; set; } = @"C:\RX-SSTV\History";
        public string SSTVChannel { get; set; } = "@SATCOM_BOARD";
        public string MainDiscussuionGroup { get; set; } = "@SATCOM_UHF";
        public string DTMFCode { get; set; }
    }
}
