namespace CatifyApi.Captcha.SpeechRecognition.Microsoft
{
    public class CognitiveSimpleResponse
    {
        public string RecognitionStatus { get; set; }

        public string DisplayText { get; set; }

        public long Offset { get; set; }

        public long Duration { get; set; }
    }
}
