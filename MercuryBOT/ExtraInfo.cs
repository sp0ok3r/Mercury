/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/

using System.Collections.Generic;
using SteamKit2;

namespace MercuryBOT
{
    public class ExtraInfo
    {
        public static string username;


        public static string GetUsername
        {
            get{return username;} set{username = value;}
        }

        public static List<EPersonaState> statesList = new List<EPersonaState> {
                        EPersonaState.Offline,
                        EPersonaState.Online,
                        EPersonaState.Busy,
                        EPersonaState.Away,
                        EPersonaState.Snooze,
                        EPersonaState.LookingToTrade,
                        EPersonaState.LookingToPlay,
                        EPersonaState.Invisible };
    }
}
               

