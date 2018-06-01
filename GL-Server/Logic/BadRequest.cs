using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

﻿namespace GL.Servers.CoC.Logic
{
    public class BadRequest
    {
        [JsonProperty("reason")]
        public string Reason { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
