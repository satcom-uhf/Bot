namespace SatcomPiratesBot
{
    internal class FreeOcrResponse
    {
        public class ParsedResult
        {
            public string ParsedText { get; set; }
        }
        public ParsedResult[] ParsedResults { get; set; }
    }
}