using CatifyApi.Captcha.SpeechRecognition.Interface;
using Google.Cloud.Speech.V1;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CatifyApi.Captcha.SpeechRecognition.Google
{
    public class GoogleSpeechClient : ISpeechRecognitionClient
    {
        private readonly string _environmentPath;

        public GoogleSpeechClient()
        {
            _environmentPath = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", EnvironmentVariableTarget.Machine);
        }

        public async Task<string> Recognize(byte[] file)
        {
            var speech = SpeechClient.Create();
            var response = await speech.RecognizeAsync(new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                LanguageCode = "en",
            }, RecognitionAudio.FromBytes(file));

            return response.Results?.FirstOrDefault()?.Alternatives?.FirstOrDefault()?.Transcript;
        }
    }
}
