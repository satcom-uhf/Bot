using Flurl.Http;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Serilog;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;


namespace SatcomPiratesBot
{
    static class ImageTools
    {
        public static Mat Resize(this Mat img)
        {
            var r_mask = new Mat();
            Cv2.Resize(img, r_mask, new OpenCvSharp.Size(320, 240), 0, 0);
            return r_mask;
        }
        public static async Task<string> RecognizeImage(this Mat img)
        {
            return await RecognizeBitmap(BitmapConverter.ToBitmap(img));
        }
        public static async Task<string> RecognizeBitmap(Image bitmap)
        {
            try
            {
                System.IO.MemoryStream ms = new MemoryStream();
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] byteImage = ms.ToArray();
                var base64 = $"data:image/jpeg;base64,{Convert.ToBase64String(byteImage)}";
                var response = await "https://api.ocr.space/parse/image"
                    .WithHeader("apiKey", MainForm.Config.OcrSpaceKey)
                    .PostMultipartAsync(x => x
                    .AddString("base64Image", base64)
                    .AddString("isOverlayRequired", "false"))
                    .ReceiveJson<FreeOcrResponse>().ConfigureAwait(false);
                return response.ParsedResults[0].ParsedText;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Cannot recognize image");
                return "Cannot detect last activity =(";
            }
        }
    }
}
