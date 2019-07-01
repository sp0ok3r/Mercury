/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/
using Gameloop.Vdf;
using Gameloop.Vdf.Linq;
using MercuryBOT.Helpers;
using MercuryBOT.User2Json;
using MercuryBOT.UserSettings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Win32Interop.Methods;
using Timer = System.Windows.Forms.Timer;

namespace MercuryBOT.Splash
{
    public partial class SplashScreen : MetroFramework.Forms.MetroForm
    {

        private static List<SteamLoginUsers> _users;

        public SplashScreen()
        {
            InitializeComponent();
            this.components.SetStyle(this);
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(Gdi32.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
        }

        private void CheckForAccountsFile()
        {
            if (!File.Exists(Program.AccountsJsonFile))
            {
                File.WriteAllText(Program.AccountsJsonFile, "{Accounts: []}");
            }

            if (!File.Exists(Program.SettingsJsonFile))
            {
                File.WriteAllText(Program.SettingsJsonFile, "{}");
            }
            if (!File.Exists(Program.SentryFolder))
            {
                Directory.CreateDirectory(Program.SentryFolder);
            }

            if (!File.Exists(Program.ChatLogsFolder))
            {
                Directory.CreateDirectory(Program.ChatLogsFolder);
            }
            lbl_info2.Text = "LOADING";
        }

        public static void LoginusersVDF_ToFile()
        {
            _users = GetLoginUsers().ToList();

            if (_users.Count == 0)
            {
                throw new IOException(@"Cannot find saved users!");
            }
            foreach (var user in _users)
            {
                var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));

                IList<string> savedAccs = list.Accounts.Select(u => u.username).ToList();

                if (!savedAccs.Contains(user.AccountName))
                {
                    var EmptyGameList = new List<Game>();
                    var EmptyCustomMessagesList = new List<UserSettings.CustomMessages>();

                    list.Accounts.Add(new UserAccounts
                    {
                        LastLoginTime = user.LastLoginTime.ToString(),
                        AdminID = 0,
                        LoginState = 1, //default: online
                        username = user.AccountName,
                        password = "",
                        SteamID = user.SteamId64,
                        LoginKey = "",
                        APIWebKey = "",
                        Games = EmptyGameList,
                        AFKMessages = EmptyCustomMessagesList,
                        MsgRecipients = new List<string>()
                    });
                    File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(list, new JsonSerializerSettings { Formatting = Formatting.Indented }));
                }
            }
        }
        
        public static IEnumerable<SteamLoginUsers> GetLoginUsers()
        {
            if (Helpers.Extensions.SteamLocation == null)
            {
                Helpers.Extensions.Init();
            }

            dynamic volvo = VdfConvert.Deserialize(File.ReadAllText(Helpers.Extensions.SteamLocation + @"\config\loginusers.vdf"));
            VToken v2 = volvo.Value;
            return v2.Children().Select(child => new SteamLoginUsers(child)).OrderByDescending(user => user.LastLoginTime).ToList();
            //.Where(user => user.RememberPassword)
        }

        [Obsolete]
        private void SplashScreen_Load(object sender, EventArgs e)
        {
            this.Activate();
            lbl_version.Text = Program.Version.Replace("-", "");
        }
        Timer tmr;

        [Obsolete]
        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            CheckForAccountsFile();

            try
            {
                LoginusersVDF_ToFile();
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("["+Program.BOTNAME+"] - Steam Directory not found, but starting anyways...");
            }

            tmr = new Timer();
            tmr.Interval = 1000;
            tmr.Start();
            tmr.Tick += tmr_Tick;
        }

        [Obsolete]
        void tmr_Tick(object sender, EventArgs e)
        {
            tmr.Stop();
            Form mf = new Main();
            mf.Show();
            this.Hide();
        }
    }
}