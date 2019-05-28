using System.Threading.Tasks;

namespace CatifyApi.Captcha.SpeechRecognition.Interface
{
    public interface ISpeechRecognitionClient
    {
        Task<string> Recognize(byte[] file);
    }
}
