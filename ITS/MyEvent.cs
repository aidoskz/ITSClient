﻿using System.Collections.Generic;
using WebSocket4Net;
using Newtonsoft.Json;
using System;
using SocketIOClient;

namespace ITSClient
{
    internal class MyEvent : EventArgs
    {
         
       // [JsonProperty(PropertyName = "on")]
        public string on { get; set; }

        //[JsonProperty(PropertyName = "data")]
       // public Dictionary<string, string> data { get; set; }
        public string data { get; set; }



    }
}