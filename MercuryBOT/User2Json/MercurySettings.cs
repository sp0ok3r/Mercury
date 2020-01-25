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

namespace MercuryBOT.User2Json
{
    public partial class MercurySettings
    {
        public int startupColor { get; set; }
        public bool playsound { get; set; }

        public bool hideInTaskBar { get; set; }
        public bool startup { get; set; }

        public int startupTab { get; set; }

        public ulong startupAcc { get; set; }
        public bool startMinimized { get; set; }
        //public string notificationEffect { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string LastTimeCheckedUpdate { get; set; }
    }
}
