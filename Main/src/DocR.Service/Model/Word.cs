﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace DocR.Service.Model
{
    public class Word
    {
        [JsonProperty("boundingBox")]
        public List<int> BoundingBox { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}