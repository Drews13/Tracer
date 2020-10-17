﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    class JsonSerializer : ISerializer
    {
        private JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Include,
            Formatting = Formatting.Indented,
        };
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, JsonSettings);
        }
    }
}
