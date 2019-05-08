/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/

namespace SteamRepAPI
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class SteamRepApi
    {
        [JsonProperty("steamrep")]
        public SteamRep Steamrep { get; set; }
    }

    public partial class SteamRep
    {
        [JsonProperty("flags")]
        public Flags Flags { get; set; }

        [JsonProperty("steamID32")]
        public string SteamId32 { get; set; }

        [JsonProperty("steamID64")]
        public string SteamId64 { get; set; }

        [JsonProperty("steamrepurl")]
        public Uri Steamrepurl { get; set; }

        [JsonProperty("reputation")]
        public Reputation Reputation { get; set; }
    }

    public partial class Flags
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class Reputation
    {
        [JsonProperty("full")]
        public string Full { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }
    }

    public partial class SteamRepApi
    {
        public static SteamRepApi FromJson(string json) => JsonConvert.DeserializeObject<SteamRepApi>(json, SteamRepAPI.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this SteamRepApi self) => JsonConvert.SerializeObject(self, SteamRepAPI.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}