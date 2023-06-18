﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace faxnocapBPbot.ConfigStructs
{
    public static class ConfigDeserializator
    {
        public static T ReturnDeserializedJson<T>(string path)
        {
            var json = string.Empty;
            using (var fs = File.OpenRead(path))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false))) json = sr.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
