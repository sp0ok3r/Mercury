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
using Timer = System.Windows.Forms.Timer;

namespace MercuryBOT.Splash
{
    public partial class SplashScreen : MetroFramework.Forms.MetroForm
    {

        private static List<SteamLoginUsers> _users;

        public SplashScreen()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(Helpers.Extensions.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
        }

        private void CheckForAccountsFile()
        {
            if (!File.Exists(Program.AccountsJsonFile))
            {
                var DefaultJson = "{Accounts: []}";
                File.WriteAllText(Program.AccountsJsonFile, DefaultJson);
                lbl_info2.Text = "Creating Accounts file...";
            }

            if (!File.Exists(Program.SettingsJsonFile))
            {
                var DefaultJson = "{}";
                File.WriteAllText(Program.SettingsJsonFile, DefaultJson);
                lbl_info2.Text = "Creating Settings file...";
            }

            if (!File.Exists(Program.SentryFolder))
            {
                Directory.CreateDirectory(Program.SentryFolder);
                lbl_info2.Text = "Creating Sentry folder...";
            }

            if (!File.Exists(Program.ChatLogsFolder))
            {
                Directory.CreateDirectory(Program.ChatLogsFolder);
                lbl_info2.Text = "Creating Chat Logs folder...";
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
                var list = JsonConvert.DeserializeObject<UserSettings.RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                foreach (var a in list.Accounts)
                {
                    if (a.username == user.AccountName)
                    {
                        return;
                    }
                }
                var EmptyGameList = new List<Game>();
                var EmptyCustomMessagesList = new List<UserSettings.CustomMessages>();
                list.Accounts.Add(new UserAccounts
                {
                    AdminID = 0,
                    username = user.AccountName,
                    password = "",
                    SteamID = user.SteamId64,
                    LoginKey = "0",
                    APIWebKey = "0",
                    Games = EmptyGameList,
                    AFKMessages = EmptyCustomMessagesList
                });
                var convertedJson = JsonConvert.SerializeObject(list, new JsonSerializerSettings { Formatting = Formatting.Indented });
                File.WriteAllText(Program.AccountsJsonFile, convertedJson);
            }
        }


        public static IEnumerable<User2Json.SteamLoginUsers> GetLoginUsers()
        {
            if (SteamPath.SteamLocation == null)
            {
                SteamPath.Init();
            }

            dynamic volvo = VdfConvert.Deserialize(File.ReadAllText(SteamPath.SteamLocation + @"\config\loginusers.vdf"));
            VToken v2 = volvo.Value;
            return v2.Children().Select(child => new User2Json.SteamLoginUsers(child)).Where(user => user.RememberPassword).OrderByDescending(user => user.LastLoginTime).ToList();
        }

        [Obsolete]
        private void SplashScreen_Load(object sender, EventArgs e)
        {
            this.Activate(); // bring form to fronte eyes
            lbl_version.Text = Program.Version;

        }
        Timer tmr;

        [Obsolete]
        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            CheckForAccountsFile();

            try // Saved some gamers (900-10)iq
            {
                LoginusersVDF_ToFile();
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Directory not found, but starting anyway...");
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


