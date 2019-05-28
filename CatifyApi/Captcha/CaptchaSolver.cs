using CatifyApi.CatifyApi.SpeechRecognition;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace CatifyApi.Captcha
{
    public class CaptchaSolver
    {
        public async Task<string> Solve(string url)
        {
            var file = await DownloadFile(url);
            var wavFile = ConvertToWav(file);

            var speechRecognizer = new SpeechRecognizer();
            return await speechRecognizer.Recognize(wavFile);
        }

        private async Task<byte[]> DownloadFile(string url)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    var response = await httpClient.SendAsync(request);
                    return await response.Content.ReadAsByteArrayAsync();
                }
            }
        }

        private byte[] ConvertToWav(byte[] file)
        {
            var originalFileStream = new MemoryStream(file);
            var outputStream = new MemoryStream();
            using (var waveStream = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(originalFileStream)))
            {
                var sample = waveStream.ToSampleProvider();
                var mono = new StereoToMonoSampleProvider(sample);
                mono.LeftVolume = 0.5f;
                mono.RightVolume = 0.5f;

                var bitSample = new SampleToWaveProvider16(mono);
                WaveFileWriter.WriteWavFileToStream(outputStream, bitSample);
            }

            return outputStream.ToArray();
        }
    }
}
