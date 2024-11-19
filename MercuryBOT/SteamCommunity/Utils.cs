/*
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/
using AngleSharp.Html.Parser;
using MercuryBOT.UserSettings;
using Newtonsoft.Json;
using SteamKit2;
using SteamKit2.Internal;
using SteamProfilePrivacy;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.IO;
using Mercury;
using System.Threading.Tasks;
using Mercury.Helpers;
using Steam.Models.DOTA2;
using SteamPlayerLevel;

namespace MercuryBOT.SteamCommunity
{
    class Utils
    {
        #region ProfileRelated
        public static IDictionary<string, int> GetProfileSettings()
        {

            //  try
            //  {
            string resp = SteamWeb.GETRequest("https://steamcommunity.com/profiles/" + HandleLogin.CurrentSteamID + "/edit/settings", SessionData.GetCookies()).Result;



            // File.WriteAllText(Program.ExecutablePath + @"\testo.html", resp);


            var document = new HtmlParser().ParseDocument(resp);
            // var ReadPrivacyDiv = document.QuerySelector("div.ProfileReactRoot").GetAttribute("data-privacysettings");
            var ReadPrivacyDiv = document.QuerySelector("div.ProfileReactRoot").GetAttribute("data-privacysettings");

            var renderPrivacySettings = RenderProfilePrivacy.FromJson(ReadPrivacyDiv);

            var dictionary = new Dictionary<string, int>{
                        { "PrivacyProfile",         renderPrivacySettings.PrivacySettings.PrivacyProfile},
                        { "PrivacyFriendsList",     renderPrivacySettings.PrivacySettings.PrivacyFriendsList},
                        { "PrivacyPlaytime",        renderPrivacySettings.PrivacySettings.PrivacyPlaytime},
                        { "PrivacyOwnedGames",      renderPrivacySettings.PrivacySettings.PrivacyOwnedGames},
                        { "PrivacyInventoryGifts",  renderPrivacySettings.PrivacySettings.PrivacyInventoryGifts},
                        { "PrivacyInventory",       renderPrivacySettings.PrivacySettings.PrivacyInventory},
                        { "ECommentPermission",     renderPrivacySettings.ECommentPermission}
                    };

            return dictionary;

            //   }
            //   catch (Exception te)
            //   {
            //      InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Try login again.");
            //     Console.WriteLine(te);
            //     return null;
            // }
        }
        public static async Task ProfileSettingsAsync(int Profile, int Inventory, int Gifts, int OwnedGames, int Playtime, int FriendsList, int Comment)
        {
            var ProfileSettings = new NameValueCollection
            {
                { "sessionid", SessionData.SessionID },// Unknown,Private, FriendsOnly,Public
                { "Privacy","{\"PrivacyProfile\":"+Profile+
                            ",\"PrivacyInventory\":" +Inventory+
                            ",\"PrivacyInventoryGifts\":"+Gifts+
                            ",\"PrivacyOwnedGames\":"+OwnedGames+
                            ",\"PrivacyPlaytime\":"+Playtime+
                            ",\"PrivacyFriendsList\":"+FriendsList+"}"},
                { "eCommentPermission" ,Comment.ToString()}//FriendsOnly,Public,Private
            };


            // Privacy: { "PrivacyProfile":1,"PrivacyInventory":1,"PrivacyInventoryGifts":1,"PrivacyOwnedGames":2,"PrivacyPlaytime":1,"PrivacyFriendsList":2}

            string resp = await SteamWeb.POSTRequest("https://steamcommunity.com/profiles/" + HandleLogin.CurrentSteamID + "/ajaxsetprivacy/", SessionData.GetCookies(), ProfileSettings);
            //string response = SteamWeb.GETRequest(url, this.Session.GetCookies()).Result;


            Console.WriteLine(resp);
            if (resp != String.Empty && resp.Contains("success\":1"))
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Profile settings set!");
            }
            else
            {
                //Console.WriteLine("erro:\n" + resp);
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Try login again.");
            }
        }

        public static async Task ClearAliasesAsync()
        {

            SessionData.GetCookies();

            var ClearAliases = new NameValueCollection { { "sessionid", SessionData.SessionID } };

          //  var postBody = new NameValueCollection();
           // postBody.Add("sessionid", SessionData.SessionID);

            try
            {
                //string response = await SteamWeb.POSTRequest("https://api.steampowered.com/ITwoFactorService/RemoveAuthenticator/v1?access_token=" + this.Session!.AccessToken, null!, postBody);


                string resp = await SteamWeb.POSTRequest("https://steamcommunity.com/profiles/" + HandleLogin.CurrentSteamID + "/ajaxclearaliashistory", null, ClearAliases);

                //var removeResponse = JsonConvert.DeserializeObject(resp);

                Console.WriteLine(resp);

                if (resp != String.Empty)
                {
                    InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Aliases Clear!");
                }
            }catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ClearUnreadMessages()
        {
            //HandleLogin.steamFriends.RequestOfflineMessages();
        }
        #endregion

        #region Group Utils
        public static void MakeGroupAnnouncement(string groupName, string HeadLine, string body)
        {
            var data = new NameValueCollection {
                {"sessionID", SessionData.SessionID},
                {"action", "post"},
                {"headline", HeadLine},
                {"body", body},
                {"languages[0][headline]", HeadLine},
                {"languages[0][body]", body},
            };

            string url = "https://steamcommunity.com/gid/" + groupName + "/announcements";

            //string resp = SteamWeb.POSTRequest(url, data);
            //if (resp != String.Empty && resp.Contains("Sorry!"))
            //{
            //    InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Try login again");
            //}
            //else
            //{
            //    InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Announcement sent!");
            //}
        }

        public static void setGroupPlayerOfTheWeek(string gid, string steamID)
        {
            var data = new NameValueCollection{
                { "xml", "1"},
                { "action", "potw" },
                { "memberId", new SteamID(steamID).Render(true) },// [U:1:46143802] steamid3
                { "sessionid", SessionData.SessionID}
            };

            string url = "https://steamcommunity.com/gid/" + gid + "/potwEdit/";

            // string resp = SteamWeb.POSTRequest(url, data); // works

            //if (resp != String.Empty && resp.Contains("Sorry!"))
            //{
            //    InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Try login again");
            //}
            //else
            //{
            //    InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Set Player of the week: " + steamID + " in: " + gid);
            //}
        }
        public static void kickGroupMember(string gid, string steamID)
        {
            var data = new NameValueCollection {
                {"sessionID", SessionData.SessionID },
                {"action", "kick" },
                {"memberId", steamID},//64
                {"queryString", "" },
            };

            string url = "https://steamcommunity.com/" + gid + "/membersManage";

            //string resp = SteamWeb.POSTRequest(url, data);

            //if (resp != String.Empty && resp.Contains("Sorry!"))
            //{
            //    InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Try login again");
            //}
            //else
            //{
            //    InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Kicked " + steamID + " from: " + gid);
            //}

        }
        public static void JoinGroup(string groupID)
        {
            var JoinGroup = new NameValueCollection{
                { "action","join"},
                { "sessionID", SessionData.SessionID}
            };

            //string resp = SteamWeb.POSTRequest("https://steamcommunity.com/gid/" + groupID, JoinGroup);

            //if (resp != String.Empty)
            //{
            //    Notification.NotifHelper.MessageBox.Show("Info", "Joined successfully " + groupID + " !");
            //}
        }

        public static void LeaveGroup(string groupID, string groupName)
        {
            var LeaveGroup = new NameValueCollection{
                { "sessionID", SessionData.SessionID },
                { "action","leaveGroup"},
                { "groupId", groupID}
            };

            //string resp = SteamWeb.POSTRequest("https://steamcommunity.com/profiles/" + HandleLogin.CurrentSteamID + "/home_process", LeaveGroup);

            //if (resp != String.Empty)
            //{
            //    Notification.NotifHelper.MessageBox.Show("Info", "Left successfully " + groupName + " !");
            //}
        }
        public static void GroupInvite(ulong groupID, ulong userID)
        {
            var mass_JoinGroup = new NameValueCollection{
                {"group", groupID.ToString()},
                {"invitee", userID.ToString()},
                {"json", "1"},
                {"sessionID", SessionData.SessionID},
                {"type", "groupInvite"},
            };

            //string resp = SteamWeb.POSTRequest("https://steamcommunity.com/actions/GroupInvite", mass_JoinGroup);

            //if (resp != String.Empty && resp.Contains("success\":1"))
            //{
            //    //InfoForm.InfoHelper.CustomMessageBox.Show("Info", "User Invited!");
            //    Console.WriteLine("User Invited!");
            //}
            //else
            //{
            //    Console.WriteLine("Try login again/Or User already invited");

            //    // InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Try login again.");
            //}
        }
        #endregion

        #region PlayGames

        public static void PlayNormal1App(uint customgame)
        {
            HandleLogin.gamesHandler.SetGamePlayingNormal(customgame);
            Console.WriteLine("[" + Program.TOOLNAME + "] - Now playing: " + customgame);
        }

        public static void PlayNonSteamGame(string customgame)
        {
            HandleLogin.gamesHandler.SetGamePlayingNONSteam(customgame);
            Console.WriteLine("[" + Program.TOOLNAME + "] - Now playing: " + customgame);
        }

        public static void StopGames()
        {
            HandleLogin.gamesHandler.StopPlayingGames();
        }
        #endregion

        #region Comments
        public static void DeleteSelectedComment(string CID, string ProfileOrClan)
        {
            var DeleteCommentData = new NameValueCollection
            {
                { "gidcomment", CID},
                { "start", "0" },
                { "count", "1" },
                { "sessionid", SessionData.SessionID },
                { "feature2", "-1" }
            };

            //string resp = SteamWeb.POSTRequest("https://steamcommunity.com/comment/" + ProfileOrClan + "/delete/" + HandleLogin.CurrentSteamID + "/-1/", DeleteCommentData);
            //if (resp != String.Empty)
            //{
            //    Console.WriteLine(resp);//debug
            //}
        }
        #endregion

        #region WebAPI
        public static void gatherWebApiKey()
        {
            //string resp = SteamWeb.GETRequest("https://steamcommunity.com/dev/apikey?l=english");

            //if (resp != String.Empty)
            //{
            //    var parser = new HtmlParser();
            //    var document = parser.ParseDocument(resp);

            //    if (document.QuerySelector("div[id='mainContents'] > h2").TextContent == "Access Denied")
            //    {
            //        InfoForm.InfoHelper.CustomMessageBox.Show("Error", document.QuerySelector("div[id='bodyContents_lo'] > p").TextContent);
            //        return;
            //    }


            //    var webkeySet = document.QuerySelector("div[id='bodyContents_ex'] > p"); //650ip
            //    if (document.QuerySelector("div[id='bodyContents_ex'] > p").TextContent.Contains("Key:"))
            //    {
            //        var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
            //        foreach (var a in list.Accounts)
            //        {
            //            if (a.username == HandleLogin.CurrentUsername)
            //            {
            //                a.APIWebKey = webkeySet.TextContent.Replace("Key: ", ""); //861ip
            //            }
            //        }
            //        File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(list, Formatting.Indented));
            //    }
            //    else
            //    {
            //        var SetWebApi = new NameValueCollection
            //        {
            //            { "domain", "localhost" },
            //            { "agreeToTerms", "agreed"},
            //            { "sessionid", SessionData.SessionID },
            //            { "Submit", "Register"}
            //        };

            //        string ObtainKey = SteamWeb.POSTRequest("https://steamcommunity.com/dev/registerkey?l=english", SetWebApi);
            //        if (ObtainKey != String.Empty)
            //        {
            //            gatherWebApiKey();
            //        }
            //    }
            //}
        }
        #endregion
    }
}