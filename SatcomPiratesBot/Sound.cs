using NAudio.Wave;
using System.Threading.Tasks;

namespace SatcomPiratesBot
{
    public static class Sound
    {
        public static async Task PlayOK()
        {
            using (var audioFile = new WaveFileReader("OK.wav"))
            using (var outputDevice = new WasapiOut())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    await Task.Delay(1000);
                }
            }
        }
    }
}
