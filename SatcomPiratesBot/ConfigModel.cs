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
    }
}
