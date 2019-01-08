﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace DocR.Service.Model
{
    public class Line
    {
        [JsonProperty("boundingBox")]
        public List<int> BoundingBox { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("words")]
        public List<Word> Words { get; set; }
    }
}