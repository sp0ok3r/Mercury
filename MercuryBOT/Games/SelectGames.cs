/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/
using MercuryBOT.Helpers;
using MercuryBOT.Properties;
using MercuryBOT.UserSettings;
using Newtonsoft.Json;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using Win32Interop.Methods;

namespace MercuryBOT.GamesGather
{
    public partial class SelectGames : MetroFramework.Forms.MetroForm
    {

        public static string apikey;

        public SelectGames()
        {
            InitializeComponent();
            this.components.SetStyle(this);
            this.Activate();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(Gdi32.CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
            foreach (var button in this.Controls.OfType<MetroFramework.Controls.MetroButton>())
            {
                IntPtr ptr = Gdi32.CreateRoundRectRgn(1, 1, button.Width, button.Height, 5, 5);
                button.Region = Region.FromHrgn(ptr);
                Gdi32.DeleteObject(ptr);
            }
        }

        private void SelectGames_Load(object sender, EventArgs e)
        {
            btn_selectAll.Enabled = false;
            var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
            foreach (var a in list.Accounts)
            {
                if (a.username == AccountLogin.CurrentUsername)
                {
                    apikey = a.APIWebKey;
                }
            }
            //if (apikey == "undefined" || apikey.Length==0)
            if (string.IsNullOrEmpty(apikey) || apikey == "0")
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Register your api key and set it on Accounts.json");
                Process.Start("https://steamcommunity.com/dev/apikey");
                Close();
                return;
            }
            else
            {
                GET_ALL_GAMES();
            }
        }

        List<uint> GAMESLIST = new List<uint>();
        Dictionary<uint, string> gamesDictionary = new Dictionary<uint, string>();

        public async void GET_ALL_GAMES()
        {
            var webInterfaceFactory = new SteamWebInterfaceFactory(apikey);
            var InterfacePlayerService = webInterfaceFactory.CreateSteamWebInterface<PlayerService>(new HttpClient());
            
            WebClient wc = new WebClient();

            var OwnedGames = await InterfacePlayerService.GetOwnedGamesAsync(AccountLogin.CurrentSteamID, true, false); // check if correct

            progreeBar_GatherGames.Maximum = Int32.Parse(OwnedGames.Data.GameCount.ToString());
            lbl_selgames_count.Text = OwnedGames.Data.GameCount.ToString() + " games loaded.";

            const string pictureUrl = "http://media.steampowered.com/steamcommunity/public/images/apps/{0}/{1}.jpg";

            string Tempath = Path.GetTempPath() + @"\MercuryTemp\GamesImg\" + AccountLogin.CurrentUsername + @"\";
            if (!Directory.Exists(Tempath))
            {
                Directory.CreateDirectory(Tempath);
            }

            int i = 0;
            foreach (Steam.Models.SteamCommunity.OwnedGameModel game in OwnedGames.Data.OwnedGames)
            {
                list_main_game.Items.Add(game.Name + " | " + Math.Round(game.PlaytimeForever.TotalHours, 0) + "h", i);

                gamesDictionary.Add(game.AppId, game.Name);
                GAMESLIST.Add(game.AppId);

                imageList1.ImageSize = new Size(180, 67);

                var FormatedImgUrl = string.Format(pictureUrl, game.AppId, game.ImgLogoUrl);

                if (File.Exists(Tempath + @"\" + game.AppId + ".jpg"))
                {
                    imageList1.Images.Add(Image.FromFile(Tempath + @"\" + game.AppId + ".jpg"));
                }
                else
                {
                    try
                    {
                        wc.DownloadFile(FormatedImgUrl, Tempath + @"\" + game.AppId + ".jpg");

                        imageList1.Images.Add(Image.FromFile(Tempath + @"\" + game.AppId + ".jpg"));


                        Graphics theGraphics = Graphics.FromHwnd(this.Handle);
                        imageList1.Draw(theGraphics, new Point(1, 1), i);
                        Application.DoEvents();
                    }
                    catch (Exception)
                    {
                        Bitmap bmp = new Bitmap(Resources.mercuryIMGNotFound);
                        imageList1.Images.Add(bmp);
                    }
                }
                progreeBar_GatherGames.Value += 1;
                i++;
            }
            string TempathFolder = Path.GetTempPath() + @"\MercuryTemp\GamesImg\";
            lbl_totalfoldersize.Text = "Folder Total Size: " + Extensions.SizeSuffix(Directory.GetFiles(TempathFolder, "*", SearchOption.AllDirectories).Sum(t => (new FileInfo(t).Length)));
            btn_selectAll.Enabled = true;

            this.Refresh();
        }



        int iSELECTED = 0;
        private void list_main_game_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_main_game.SelectedItems.Count > 0)
            {
                iSELECTED = list_main_game.SelectedItems[0].Index;
            }
        }

        private void AddSelectedGame()
        {
            var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
            foreach (var a in list.Accounts)
            {
                if (a.username == AccountLogin.CurrentUsername)
                {
                    for (int i = 0; i < a.Games.Count; i++)
                    {
                        if (a.Games[i].app_id == GAMESLIST[iSELECTED])
                        {
                            InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Duplicate game!");
                            return;
                        }
                    }
                    foreach (KeyValuePair<uint, string> entry in gamesDictionary)
                    {
                        if (GAMESLIST[iSELECTED] == entry.Key)
                        {
                            Game NewGame = new Game { app_id = entry.Key, name = entry.Value };
                            a.Games.Add(NewGame);
                        }
                    }
                }
            }
            File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(list, Formatting.Indented));
            InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Game added!");
        }

        private void btn_AddSelected_Click(object sender, EventArgs e)
        {
            AddSelectedGame();
        }
        
        private void Btn_selectAll_Click(object sender, EventArgs e)
        {
            var AccountsList = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
            
            foreach (var Acc in AccountsList.Accounts)
            {
                if (Acc.username == AccountLogin.CurrentUsername)
                {
                    if (Acc.Games.Count == 0)
                    {
                        foreach (KeyValuePair<uint, string> entry in gamesDictionary)
                        {
                            Game NewGame = new Game { app_id = entry.Key, name = entry.Value };
                            Acc.Games.Add(NewGame);
                        }
                    }
                    else
                    {
                        foreach (KeyValuePair<uint, string> entry in gamesDictionary)
                        {
                            if (!Acc.Games.Any(s => s.app_id.Equals(entry.Key)))
                            {
                                Game NewGame = new Game { app_id = entry.Key, name = entry.Value };
                                Acc.Games.Add(NewGame);
                            }
                        }
                    }
                }
                File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(AccountsList, Formatting.Indented)); 
            }
            InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Added/Updated all games!");
        }

        private void list_main_game_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AddSelectedGame();
        }

        private void txtBox_Game2Find_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode) 
            {
                if (txtBox_Game2Find.Text == "")
                {
                    InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Game not found!");
                }
                else
                {
                    ListViewItem foundItem = list_main_game.FindItemWithText(txtBox_Game2Find.Text, false, 0, true);

                    if (foundItem != null)
                    {
                        list_main_game.TopItem = foundItem;
                        foundItem.Selected = true;
                    }
                    else
                    {
                        InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Game not found!");
                    }
                }
            }
        }

        private void metroLink_GamesIMGPath_Click(object sender, EventArgs e)
        {
            Process.Start(Path.GetTempPath() + @"\MercuryTemp\GamesImg\"+ AccountLogin.CurrentUsername);
        }

        private void SelectGames_FormClosed(object sender, FormClosedEventArgs e)
        {
            list_main_game.Dispose();
            list_main_game.Items.Clear();
            list_main_game.Refresh();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (chck_clearimagescache.Checked)
            {
                var path = Path.GetTempPath() + @"\MercuryTemp\GamesImg\" + AccountLogin.CurrentUsername;
                Directory.GetFiles(path).ToList().ForEach(File.Delete);
                Directory.Delete(path);
            }
        }
    }
}
// 256 iq