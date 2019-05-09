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
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class JsonSteamRep
    {
        [JsonProperty("steamrep")]
        public Steamrep Steamrep { get; set; }
    }

    public partial class Steamrep
    {
        [JsonProperty("flags")]
        public Flags Flags { get; set; }

        [JsonProperty("steamID32")]
        public string SteamId32 { get; set; }

        [JsonProperty("steamID64")]
        public string SteamId64 { get; set; }

        [JsonProperty("steamrepurl")]
        public Uri Steamrepurl { get; set; }

        [JsonProperty("displayname")]
        public string Displayname { get; set; }

        [JsonProperty("rawdisplayname")]
        public string Rawdisplayname { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }

        [JsonProperty("membersince")]
 
        public long Membersince { get; set; }

        [JsonProperty("customurl")]
        public string Customurl { get; set; }

        [JsonProperty("tradeban")]
 
        public long Tradeban { get; set; }

        [JsonProperty("vacban")]
 
        public long Vacban { get; set; }

        [JsonProperty("lastsynctime")]
 
        public long Lastsynctime { get; set; }

        [JsonProperty("reputation")]
        public Reputation Reputation { get; set; }

        [JsonProperty("stats")]
        public Stats Stats { get; set; }
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

    public partial class Stats
    {
        [JsonProperty("bannedfriends")]
 
        public long Bannedfriends { get; set; }

        [JsonProperty("unconfirmedreports")]
        public Unconfirmedreports Unconfirmedreports { get; set; }
    }

    public partial class Unconfirmedreports
    {
        [JsonProperty("reportcount")]
 
        public long Reportcount { get; set; }

        [JsonProperty("reportlink")]
        public Uri Reportlink { get; set; }
    }

    public partial class JsonSteamRep
    {
        public static JsonSteamRep FromJson(string json) => JsonConvert.DeserializeObject<JsonSteamRep>(json, SteamRepAPI.Converter.Settings);
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
