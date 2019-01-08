using Newtonsoft.Json;

namespace DocR.Service.Model
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TextOperationResult
    {
        [JsonProperty("recognitionResult")]
        public RecognitionResult RecognitionResult { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}