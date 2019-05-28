using CatifyApi.Captcha.SpeechRecognition.Interface;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CatifyApi.Captcha.SpeechRecognition.Wit
{
    public class WitClient : ISpeechRecognitionClient
    {
        private readonly string _token;

        private const string Endpoint = "https://api.wit.ai/speech?v=20170307";

        public WitClient()
        {
            _token = Environment.GetEnvironmentVariable("Wit_Token", EnvironmentVariableTarget.Machine);
        }

        public async Task<string> Recognize(byte[] file)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var audioFile = new ByteArrayContent(file);
                audioFile.Headers.ContentType = new MediaTypeHeaderValue("audio/wav");

                var response = await httpClient.PostAsync(Endpoint, audioFile);
                var jsonResult = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<WitResponse>(jsonResult);
                return result?._text;
            }
        }
    }
}
