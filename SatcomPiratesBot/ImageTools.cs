using Flurl.Http;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Serilog;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Tesseract;

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
        public static string RecognizeImage(this Mat img) => RecognizeBitmap(BitmapConverter.ToBitmap(img));
        private static string RecognizeBitmap(Bitmap bitmap)
        {
            try
            {
                TesseractEngine engine = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
                var pix=Tesseract.PixConverter.ToPix(bitmap);
                Page page = engine.Process(pix, PageSegMode.Auto);
                return page.GetText()?.Replace(" ", "");

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Cannot recognize image");
                return "Cannot detect last activity =(";
            }
        }
    }
}
