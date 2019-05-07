/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MercuryBOT.UserSettings
{
    public partial class UserAccounts
    {
        [JsonProperty("LastLoginTime")]
        public DateTime LastLoginTime { get; set; }

        [JsonProperty("AdminID")]
        public ulong AdminID { get; set; }

        [JsonProperty("LoginState")]
        public int LoginState { get; set; }

        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("password")]
        public string password { get; set; }

        [JsonProperty("LoginKey")]
        public string LoginKey { get; set; }

        [JsonProperty("APIWebKey")]
        public string APIWebKey { get; set; }

        [JsonProperty("SteamID")]
        public ulong SteamID { get; set; }

        [JsonProperty("ChatLogger")]
        public bool ChatLogger { get; set; }

        [JsonProperty("Games")]
        public List<Game> Games { get; set; }

        [JsonProperty("AFKMessages")]
        public List<CustomMessages> AFKMessages { get; set; }

        [JsonProperty("MsgRecipients")]
        public List<string> MsgRecipients { get; set; }
       
    }
    public class Game
    {
        [JsonProperty("app_id")]
        public uint app_id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }
    }

    public class CustomMessages
    {
        [JsonProperty("Message")]
        public string Message { get; set; }
    }

    public class RootObject
    {
        public List<UserAccounts> Accounts { get; set; }
    }

    public partial class UserAccounts
    {
        public static UserAccounts FromJson(string json) => JsonConvert.DeserializeObject<UserAccounts>(json, UserSettings.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this UserAccounts self) => JsonConvert.SerializeObject(self, UserSettings.Converter.Settings);
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
