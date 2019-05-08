/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/
using Gameloop.Vdf.Linq;
using System;

namespace MercuryBOT.User2Json
{
    public class SteamLoginUsers
    {
        public SteamLoginUsers(VProperty volvo)
        {
            SteamId64 = ulong.Parse(volvo.Key);
            AccountName = volvo.Value[@"AccountName"].ToString();
            PersonaName = volvo.Value[@"PersonaName"].ToString();
            RememberPassword = int.Parse(volvo.Value[@"RememberPassword"].ToString()) == 1;
            MostRecent = int.Parse(volvo.Value[@"mostrecent"].ToString()) == 1;
            LastLoginTime = volvo.Value[@"Timestamp"].ToString();
            //Helpers.Extensions.GetTime(volvo.Value[@"Timestamp"].ToString());
        }

        public ulong SteamId64 { get; }

        public string AccountName { get; }

        public string PersonaName { get; }

        public bool RememberPassword { get; }

        public bool MostRecent { get; }

        public string LastLoginTime { get; }
    }
}