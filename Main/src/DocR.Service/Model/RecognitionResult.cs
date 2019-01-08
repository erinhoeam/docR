using System.Collections.Generic;
using Newtonsoft.Json;

namespace DocR.Service.Model
{
    public class RecognitionResult
    {
        [JsonProperty("lines")]
        public List<Line> Lines { get; set; }
    }
}