using CatifyApi.Captcha.SpeechRecognition.Interface;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CatifyApi.Captcha.SpeechRecognition.Microsoft
{
    public class CognitiveServicesClient : ISpeechRecognitionClient
    {
        private readonly string _subscriptionKey;

        private const string CognitiveEndpoint = "https://northeurope.stt.speech.microsoft.com/speech/recognition/conversation/cognitiveservices/v1?format=simple&language=en-US";
        private const string TokenEndpoint = "https://northeurope.api.cognitive.microsoft.com/sts/v1.0/issueToken";

        public CognitiveServicesClient()
        {
            _subscriptionKey = Environment.GetEnvironmentVariable("Cognitive_Key", EnvironmentVariableTarget.Machine);
        }

        public async Task<string> Recognize(byte[] file)
        {
            var token = await FetchTokenAsync();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var audioFile = new ByteArrayContent(file);
                audioFile.Headers.ContentType = new MediaTypeHeaderValue("audio/wav");

                var response = await httpClient.PostAsync(CognitiveEndpoint, audioFile);
                var jsonstring = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<CognitiveSimpleResponse>(jsonstring);
                return result?.DisplayText;
            }
        }

        private async Task<string> FetchTokenAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
                var uriBuilder = new UriBuilder(TokenEndpoint);
                var result = await client.PostAsync(uriBuilder.Uri.AbsoluteUri, null);

                return await result.Content.ReadAsStringAsync();
            }
        }
    }
}
