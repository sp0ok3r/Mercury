/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/

using System;
using System.Windows.Forms;
using System.IO;
using MercuryBOT.Splash;
using System.Net;
using System.Diagnostics;

namespace MercuryBOT
{
    static class Program
    {
        public static readonly WebClient Web              = new WebClient();
        //public static readonly Process MercuryProcess     = Process.GetCurrentProcess();
        public static readonly Process[] CurrentProcesses = Process.GetProcesses();

        public static readonly string BOTNAME          = "MercuryBOT";
        public static readonly string Version          = "4.0.0beta6.3f";//4.0.0

        public static readonly string spkDomain        = "http://sp0ok3r.tk/Mercury/";

        public static readonly string ExecutablePath   = Path.GetDirectoryName(Application.ExecutablePath);
        public static readonly string AccountsJsonFile = ExecutablePath + @"\Accounts.json";
        public static readonly string SettingsJsonFile = ExecutablePath + @"\Settings.json";
        public static readonly string SentryFolder     = ExecutablePath + @"\Sentry\";
        public static readonly string ChatLogsFolder   = ExecutablePath + @"\ChatLogs\";
        public static readonly string keysFolder       = ExecutablePath + @"\Keys\";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SplashScreen());
        }
    }
}