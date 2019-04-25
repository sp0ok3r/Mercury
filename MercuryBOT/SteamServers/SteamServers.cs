/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/

namespace SteamServers
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Welcome
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("online")]
        public double Online { get; set; }

        [JsonProperty("services")]
        public Services Services { get; set; }

        [JsonProperty("psa")]
        public string Psa { get; set; }
    }

    public partial class Services
    {
        [JsonProperty("cms")]
        public data Cms { get; set; }

        [JsonProperty("cms-ws")]
        public data CmsWs { get; set; }

        [JsonProperty("community")]
        public data Community { get; set; }

        [JsonProperty("csgo_australia")]
        public data CsgoAustralia { get; set; }

        [JsonProperty("csgo_brazil")]
        public data CsgoBrazil { get; set; }

        [JsonProperty("csgo_chile")]
        public data CsgoChile { get; set; }

        [JsonProperty("csgo_china_guangzhou")]
        public data CsgoChinaGuangzhou { get; set; }

        [JsonProperty("csgo_china_shanghai")]
        public data CsgoChinaShanghai { get; set; }

        [JsonProperty("csgo_china_tianjin")]
        public data CsgoChinaTianjin { get; set; }

        [JsonProperty("csgo_community")]
        public data CsgoCommunity { get; set; }

        [JsonProperty("csgo_emirates")]
        public data CsgoEmirates { get; set; }

        [JsonProperty("csgo_eu_east")]
        public data CsgoEuEast { get; set; }

        [JsonProperty("csgo_eu_north")]
        public data CsgoEuNorth { get; set; }

        [JsonProperty("csgo_eu_west")]
        public data CsgoEuWest { get; set; }

        [JsonProperty("csgo_hong_kong")]
        public data CsgoHongKong { get; set; }

        [JsonProperty("csgo_india")]
        public data CsgoIndia { get; set; }

        [JsonProperty("csgo_india_east")]
        public data CsgoIndiaEast { get; set; }

        [JsonProperty("csgo_japan")]
        public data CsgoJapan { get; set; }

        [JsonProperty("csgo_mm_scheduler")]
        public data CsgoMmScheduler { get; set; }

        [JsonProperty("csgo_peru")]
        public data CsgoPeru { get; set; }

        [JsonProperty("csgo_poland")]
        public data CsgoPoland { get; set; }

        [JsonProperty("csgo_sessions")]
        public data CsgoSessions { get; set; }

        [JsonProperty("csgo_singapore")]
        public data CsgoSingapore { get; set; }

        [JsonProperty("csgo_south_africa")]
        public data CsgoSouthAfrica { get; set; }

        [JsonProperty("csgo_spain")]
        public data CsgoSpain { get; set; }

        [JsonProperty("csgo_us_northcentral")]
        public data CsgoUsNorthcentral { get; set; }

        [JsonProperty("csgo_us_northeast")]
        public data CsgoUsNortheast { get; set; }

        [JsonProperty("csgo_us_northwest")]
        public data CsgoUsNorthwest { get; set; }

        [JsonProperty("csgo_us_southeast")]
        public data CsgoUsSoutheast { get; set; }

        [JsonProperty("csgo_us_southwest")]
        public data CsgoUsSouthwest { get; set; }

        [JsonProperty("database")]
        public data Database { get; set; }

        [JsonProperty("dota2")]
        public data Dota2 { get; set; }

        [JsonProperty("graphs")]
        public data Graphs { get; set; }

        [JsonProperty("online")]
        public Online Online { get; set; }

        [JsonProperty("store")]
        public data Store { get; set; }

        [JsonProperty("tf2")]
        public data Tf2 { get; set; }

        [JsonProperty("webapi")]
        public data Webapi { get; set; }
    }

    public partial class data
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }
    }

    public partial class Online
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }

    public enum Status { Good, Major, Minor };

    public partial class Welcome
    {
        public static Welcome FromJson(string json) => JsonConvert.DeserializeObject<Welcome>(json, SteamServers.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Welcome self) => JsonConvert.SerializeObject(self, SteamServers.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                StatusConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class StatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "good":
                    return Status.Good;
                case "major":
                    return Status.Major;
                case "minor":
                    return Status.Minor;
            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Status)untypedValue;
            switch (value)
            {
                case Status.Good:
                    serializer.Serialize(writer, "good");
                    return;
                case Status.Major:
                    serializer.Serialize(writer, "major");
                    return;
                case Status.Minor:
                    serializer.Serialize(writer, "minor");
                    return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }
}
