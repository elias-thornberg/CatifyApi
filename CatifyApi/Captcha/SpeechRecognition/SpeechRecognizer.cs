using CatifyApi.Captcha.SpeechRecognition.Google;
using CatifyApi.Captcha.SpeechRecognition.Interface;
using CatifyApi.Captcha.SpeechRecognition.Microsoft;
using CatifyApi.Captcha.SpeechRecognition.Wit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatifyApi.CatifyApi.SpeechRecognition
{
    public class SpeechRecognizer
    {
        private readonly List<ISpeechRecognitionClient> _clients = new List<ISpeechRecognitionClient>
        {
            new GoogleSpeechClient(),
            new CognitiveServicesClient(),
            new WitClient()
        };

        public async Task<string> Recognize(byte[] file)
        {
            foreach(var client in _clients)
            {
                var result = await client.Recognize(file);
                if(!string.IsNullOrEmpty(result))
                {
                    return result;
                }
            }

            return string.Empty;
        }
    }
}
