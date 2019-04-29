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
using Mercury.Logger;
using MercuryBOT.FriendsList;
using MercuryBOT.SteamTrade;
using MercuryBOT.UserSettings;
using Newtonsoft.Json;
using SteamKit2;
using SteamKit2.Internal;
using SteamProfilePrivacy;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace MercuryBOT
{
    public class AccountLogin
    {
        private static Logger m_logger;

        public static string UserPersonaName, UserCountry, CurrentUsername;

        public static ulong CurrentAdmin;
        public static ulong CurrentSteamID = 0;
        public static string webAPIUserNonce;

        public static string myWebAPIUser;
        public static string myUserNonce;
        public static string myUniqueId;
        public static string APIKey;

        private System.Timers.Timer TrolhaCheckCommunity = new System.Timers.Timer();


        /// <summary>
        /// List with Friends/Groups
        /// </summary>
        public static List<SteamID> Friends { get; private set; }
        //private readonly List<SteamID> Groups = new List<SteamID>();
        public static Dictionary<ulong, string> ClanDictionary = new Dictionary<ulong, string>();

        /// <summary>
        /// Steam related
        /// </summary>
        public static SteamClient steamClient;
        public static SteamUser steamUser;
        public static SteamFriends steamFriends;
        public static CallbackManager Mercury_manager;
        public static SteamWeb steamWeb;


        public static SteamGameCoordinator steamGameCoordinator;
        public static SteamUnifiedMessages steamUnifiedMessages;

        /// <summary>
        /// Auto Msg Settings
        /// </summary>
        /// 
        public static bool ChatLogger = false;

        public static bool isAwayState = false;

        public static bool AwayMsg = false;

        public static string MessageString;

        public static bool AwayCustomMessageList = false;

        public static bool FriendsLoaded = false;

        /// <summary>
        /// Avatar Links
        /// </summary>
        public static string AvatarPrefix = "http://cdn.akamai.steamstatic.com/steamcommunity/public/images/avatars/";
        public static string AvatarSuffix = "_full.jpg";

        /// <summary>
        /// State Settings
        /// </summary>
        public static string ChangeState;

        public static bool IsWebLoggedIn { get; private set; }
        public static bool IsLoggedIn { get; private set; }
        public static bool isRunning = false;

        //Disconnected ints
        private static int DisconnectedCounter;
        private static int MaxDisconnects = 4;
        //

        private static string NewloginKey = null;

        private static bool cookiesAreInvalid = true;

        public static string user, pass;

        public static int maxfriendCount;

        public static string authCode, twoFactorAuth;
        public static string steamID;



        public static void UserSettingsGather(string username, string password)
        {
            try
            {
                var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                user = username;
                CurrentUsername = username;
                
                foreach (var a in list.Accounts)
                {
                    if (a.username == username)
                    {
                        pass = a.password;
                    }
                }
                Login();

            }
            catch (Exception e)
            {
               Console.WriteLine("[" + Program.BOTNAME + " usersettingsgather] - " + e);
            }
            

        }
        public static void Login()
        {
            Console.WriteLine("[" + Program.BOTNAME + "] - Starting Login...");

            isRunning = true;

            steamClient = new SteamClient();

            Mercury_manager = new CallbackManager(steamClient);
            m_logger = new Logger(user);
            steamWeb = new SteamWeb();

            //DebugLog.AddListener(new MyListener());
            //DebugLog.Enabled = true;


            #region Callbacks
            steamUser = steamClient.GetHandler<SteamUser>();
            steamFriends = steamClient.GetHandler<SteamFriends>();

            Mercury_manager.Subscribe<SteamClient.ConnectedCallback>(OnConnected);
            Mercury_manager.Subscribe<SteamClient.DisconnectedCallback>(OnDisconnected);

            Mercury_manager.Subscribe<SteamUser.LoggedOnCallback>(OnLoggedOn);
            Mercury_manager.Subscribe<SteamUser.LoggedOffCallback>(OnLoggedOff);
            Mercury_manager.Subscribe<SteamUser.AccountInfoCallback>(OnAccountInfo);
            Mercury_manager.Subscribe<SteamUser.UpdateMachineAuthCallback>(OnMachineAuth);
            Mercury_manager.Subscribe<SteamUser.LoginKeyCallback>(OnLoginKey);
            Mercury_manager.Subscribe<SteamUser.WebAPIUserNonceCallback>(WebAPIUser);

            Mercury_manager.Subscribe<SteamFriends.FriendsListCallback>(OnFriendsList);
            // Mercury_manager.Subscribe<SteamFriends.FriendAddedCallback>(OnFriendAdded);

            Mercury_manager.Subscribe<SteamFriends.PersonaStateCallback>(OnPersonaState);
            Mercury_manager.Subscribe<SteamFriends.ProfileInfoCallback>(OnProfileInfo);
            Mercury_manager.Subscribe<SteamFriends.FriendMsgCallback>(OnFriendMsg);
            Mercury_manager.Subscribe<SteamFriends.FriendMsgEchoCallback>(OnFriendEchoMsg);
            Mercury_manager.Subscribe<SteamFriends.PersonaChangeCallback>(OnSteamNameChange);

            #endregion

            Console.WriteLine("[" + Program.BOTNAME + "] - Connecting to Steam...");


            steamClient.Connect();

            while (isRunning)
            {
                Mercury_manager.RunWaitCallbacks(TimeSpan.FromMilliseconds(500));
            }
        }

        static void OnConnected(SteamClient.ConnectedCallback callback)
        {

            if (callback.ToString() != "SteamKit2.SteamClient+ConnectedCallback")
            {
                Console.WriteLine("[" + Program.BOTNAME + "] - Unable to connect to Steam: {0}", callback);

                isRunning = false;
                return;
            }
            //Sucess
            Console.WriteLine("[" + Program.BOTNAME + "] - Connected to Steam! Logging in '{0}'...", user);
           // ExtraInfo.GetUsername = user;

            Main.M_NotifyIcon.ShowBalloonTip(450, "INFO","Connected to Steam!\nLogging in "+ user + "..." , ToolTipIcon.Info);


            byte[] sentryHash = null;
            if (File.Exists(Program.SentryFolder + user + ".bin"))
            {
                byte[] sentryFile = File.ReadAllBytes(Program.SentryFolder + user + ".bin");
                sentryHash = CryptoHelper.SHAHash(sentryFile);
            }

            //Set LoginKey for user
            var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
            foreach (var a in list.Accounts)
            {
                if (a.username == user)
                {
                    // if (a.LoginKey.ToString() == "undefined") // deu?
                    if (string.IsNullOrEmpty(a.LoginKey) || a.LoginKey.ToString() == "undefined")
                    {

                        // NewloginKey = null;
                        a.LoginKey = "0";
                        File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(list, Formatting.Indented)); // update login key

                    }
                    else
                    {
                        NewloginKey = a.LoginKey;
                        myUniqueId = a.LoginKey;
                        File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(list, Formatting.Indented)); // update login key
                    }
                }
            }


            steamUser.LogOn(new SteamUser.LogOnDetails
            {
                Username = user,
                Password = pass,
                //NewloginKey == null ? pass : null,
                AuthCode = authCode,
                TwoFactorCode = twoFactorAuth,
                SentryFileHash = sentryHash,
                //
                LoginID = 1337,
                ShouldRememberPassword = true,
                LoginKey = NewloginKey
            });
        }

        static void OnLoggedOn(SteamUser.LoggedOnCallback callback)
        {
            bool isSteamGuard = callback.Result == EResult.AccountLogonDenied;
            bool is2FA = callback.Result == EResult.AccountLoginDeniedNeedTwoFactor;
            bool isLoginKey = callback.Result == EResult.InvalidPassword && NewloginKey != null;

            if (isSteamGuard || is2FA || isLoginKey)
            {

                if (!isLoginKey)
                {
                    Console.WriteLine("[" + Program.BOTNAME + "] - This account is SteamGuard protected!");
                }

                if (is2FA)
                {
                    SteamGuard SteamGuard = new SteamGuard("Phone", user);
                    SteamGuard.ShowDialog();

                    bool UserInputCode = true;
                    while (UserInputCode)
                    {
                        if (SteamGuard.AuthCode.Length == 5) // Wait for user input
                        {
                            UserInputCode = false;
                        }
                    }

                    twoFactorAuth = SteamGuard.AuthCode;

                }
                else if (isLoginKey)
                {
                    var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                    foreach (var a in list.Accounts)
                    {
                        if (a.username == user)
                        {
                            a.LoginKey = "";
                            Console.WriteLine("[" + Program.BOTNAME + "] - Removed old loginkey!");
                        }
                    }
                    string output = JsonConvert.SerializeObject(list, Formatting.Indented);
                    File.WriteAllText(Program.AccountsJsonFile, output);

                    NewloginKey = "";

                    if (pass != null)
                    {
                        Console.WriteLine("[" + Program.BOTNAME + "] - Login key expired. Connecting with user password.");
                        //InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Login key expired/Wrong Password. Connecting with user password. Wait 3secs...");
                        Main.M_NotifyIcon.ShowBalloonTip(500, "INFO", "Login key expired/Wrong Password.\nConnecting with user password. \nWait 3secs...", ToolTipIcon.Info);


                    }
                    else
                    {
                        Console.WriteLine("[" + Program.BOTNAME + "] - Login key expired.");
                        //InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Login key expired! Wait 3secs...");
                        Main.M_NotifyIcon.ShowBalloonTip(500, "INFO", "Login key expired!\nWait 3secs...", ToolTipIcon.Info);

                    }
                }
                else
                {
                    Console.Write("[" + Program.BOTNAME + "] - Please enter the auth code sent to the email at {0}: ", callback.EmailDomain);

                    SteamGuard SteamGuard = new SteamGuard(callback.EmailDomain, user);
                    SteamGuard.ShowDialog();

                    bool UserInputCode = true;
                    while (UserInputCode)
                    {
                        if (SteamGuard.AuthCode.Length == 5) // Wait for user input
                        {
                            UserInputCode = false;
                        }
                    }
                    authCode = SteamGuard.AuthCode;
                }
                return;

            }
            else if (callback.Result != EResult.OK)
            {
                Console.WriteLine("[" + Program.BOTNAME + "] - Unable to logon to Steam: {0} / {1}", callback.Result, callback.ExtendedResult);
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Unable to logon to Steam: " + callback.Result);
                isRunning = false;
                return;
            }
            Console.WriteLine("[" + Program.BOTNAME + "] - Successfully logged on!");
            Main.M_NotifyIcon.ShowBalloonTip(200, "INFO", "Successfully logged on!", ToolTipIcon.Info);

            steamID = steamClient.SteamID.ConvertToUInt64().ToString();
            CurrentSteamID = steamClient.SteamID.ConvertToUInt64();
            myUserNonce = callback.WebAPIUserNonce;

            UserWebLogOn(); // connect to community

            IsLoggedIn = true;
            UserClanIDS();
            steamFriends.SetPersonaState(EPersonaState.Online);
            //gatherWebApiKey();

            foreach (var a in JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile)).Accounts)
            {
                if (a.username == user && a.ChatLogger == true)
                {
                    ChatLogger = true;
                }
            }
        }

        static void OnDisconnected(SteamClient.DisconnectedCallback callback)
        {
            DisconnectedCounter++;

            if (isRunning)
            {
                if (DisconnectedCounter >= MaxDisconnects)
                {
                    Console.WriteLine("[" + Program.BOTNAME + "] - Too many disconnects occured in a short period of time. Wait 3 minutes brother...");
                    InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Too many disconnects occured in a short period of time. Wait 3 minutes brother...");
                    //Thread.Sleep(TimeSpan.FromMinutes(3));
                    DisconnectedCounter = 0;
                }
            }
            Console.WriteLine("[" + Program.BOTNAME + "] - Reconnecting in 3s ...");
            Thread.Sleep(3000);

            steamClient.Connect();
        }


        static void WebAPIUser(SteamUser.WebAPIUserNonceCallback webCallback)
        {
            Console.WriteLine("[" + Program.BOTNAME + "] - Received new WebAPIUserNonce.");

            if (webCallback.Result == EResult.OK)
            {
                myUserNonce = webCallback.Nonce;

                UserWebLogOn();
            }
            else
            {
                Console.WriteLine("[" + Program.BOTNAME + "] - WebAPIUserNonce Error: " + webCallback.Result);
            }
        }

        static void OnPersonaState(SteamFriends.PersonaStateCallback callback)
        {
            EPersonaState state = callback.State;
            SteamID friendId = callback.FriendID;

            ListFriends.UpdateName(friendId.ConvertToUInt64(), callback.Name);//soon
            ListFriends.UpdateStatus(friendId.ConvertToUInt64(), state.ToString());//soon

            if (callback.FriendID == steamClient.SteamID && steamFriends.GetPersonaState() != EPersonaState.Online) //detect when user goes afk
            {
                isAwayState = true;
            }
            else
            {
                isAwayState = false;
            }
        }


        static void OnProfileInfo(SteamFriends.ProfileInfoCallback callback)
        {
            // Console.WriteLine(callback.SteamID + ""+callback.TimeCreated);

        }

        static void OnLoginKey(SteamUser.LoginKeyCallback callback)
        {
            myUniqueId = callback.UniqueID.ToString();

            steamUser.AcceptNewLoginKey(callback);

            //  UserWebLogOn();

            var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
            foreach (var a in list.Accounts)
            {
                if (a.username == user)
                {
                    a.LoginKey = callback.LoginKey; // check this
                    NewloginKey = callback.LoginKey;// check this

                    if (a.SteamID.ToString() == "0")//add null or empty?
                    {
                        a.SteamID = steamClient.SteamID.ConvertToUInt64();
                    }
                    Console.WriteLine("[" + Program.BOTNAME + "] - Got Login-Key, setting in config!");
                }
            }
            File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(list, Formatting.Indented));
        }

        static void UserWebLogOn()
        {
            do
            {
                IsWebLoggedIn = steamWeb.Authenticate(myUniqueId, steamClient, myUserNonce);

                if (!IsWebLoggedIn)
                {
                    Console.WriteLine("[" + Program.BOTNAME + "] - Authentication failed, retrying in 2s...");
                    Thread.Sleep(2000);
                }
            } while (!IsWebLoggedIn); //test

            cookiesAreInvalid = false;
        }

        //    try
        //    {
        //        if (steamWeb.Authenticate(myUniqueId, steamClient, myUserNonce))
        //        {
        //            IsWebLoggedIn = true;
        //            Console.WriteLine("[" + Program.BOTNAME + "] - User Authenticated! ");
        //            cookiesAreInvalid = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("[" + Program.BOTNAME + "] - Error on UserWebLogon: " + ex.Message);
        //    }
        //}


        public static bool CheckCookies()
        {
            // We still haven't re-authenticated
            if (cookiesAreInvalid)
            {
                return false;
            }


            try
            {
                if (!steamWeb.VerifyCookies())
                {
                    // Cookies are no longer valid
                    Console.WriteLine("[" + Program.BOTNAME + "] - Cookies are invalid. Need to re-authenticate.");

                    cookiesAreInvalid = true;
                    steamUser.RequestWebAPIUserNonce();

                    return false;
                }
            }
            catch
            {
                Console.WriteLine("[" + Program.BOTNAME + "] - Cookie check failed. http://steamcommunity.com is possibly down.");
            }
            IsWebLoggedIn = true;
            return true;
            
        }


        static void OnLoggedOff(SteamUser.LoggedOffCallback callback)
        {
            //switch (callback.Result)
            //{
            //    case EResult.LoggedInElsewhere:
            //        //PlayingWasBlocked = true;

            //        break;
            //    case EResult.LogonSessionReplaced:
            //        // LastLogonSessionReplaced = now;

            //        break;
            //}

            IsLoggedIn = false;
            Console.WriteLine("[" + Program.BOTNAME + "] - Logged off of Steam: {0}", callback.Result);
            InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Logged off of Steam:" + callback.Result);
        }

        static void OnMachineAuth(SteamUser.UpdateMachineAuthCallback callback)
        {
            Console.WriteLine("[" + Program.BOTNAME + "] - Updating sentryfile...");

            byte[] sentryHash = CryptoHelper.SHAHash(callback.Data);
            File.WriteAllBytes(Program.SentryFolder + user + ".bin", callback.Data);

            steamUser.SendMachineAuthResponse(new SteamUser.MachineAuthDetails { JobID = callback.JobID, FileName = callback.FileName, BytesWritten = callback.BytesToWrite, FileSize = callback.Data.Length, Offset = callback.Offset, Result = EResult.OK, LastError = 0, OneTimePassword = callback.OneTimePassword, SentryFileHash = sentryHash, });
            Console.WriteLine("[" + Program.BOTNAME + "] - Sentry updated!");
        }


        static void OnAccountInfo(SteamUser.AccountInfoCallback callback)
        {
            UserPersonaName = callback.PersonaName;
            UserCountry = callback.Country;

        }

        public static void LoadFriends() //https://github.com/waylaidwanderer/Mist/tree/master/SteamBot
        {
            ListFriends.Clear();
            List<SteamID> steamIdList = new List<SteamID>();
            Console.WriteLine("[" + Program.BOTNAME + "] - Loading all friends...");
            for (int index = 0; index < steamFriends.GetFriendCount(); ++index)
            {
                steamIdList.Add(steamFriends.GetFriendByIndex(index));
                Thread.Sleep(25);
            }
            for (int index = 0; index < steamIdList.Count; ++index)
            {
                SteamID steamId = steamIdList[index];
                if (steamFriends.GetFriendRelationship(steamId) == EFriendRelationship.Friend)
                {
                    string friendPersonaName = steamFriends.GetFriendPersonaName(steamId);
                    string game = steamFriends.GetFriendRelationship(steamId).ToString(); // check
                    string status = steamFriends.GetFriendPersonaState(steamId).ToString();
                    ListFriends.Add(friendPersonaName, (ulong)steamId, game, status);
                }
            }
            foreach (ListFriends listFriends in ListFriends.Get(false))
            {
                if (ListFriendRequests.Find(listFriends.SID))
                {
                    Console.WriteLine("[" + Program.BOTNAME + "] - Found friend {0} in list of friend requests, so let's remove the user.", (object)listFriends.Name);
                    ListFriendRequests.Remove(listFriends.SID);
                }
            }
            foreach (ListFriendRequests listFriendRequests in ListFriendRequests.Get())
            {
                if (listFriendRequests.Name == "[unknown]")
                {
                    string friendPersonaName = steamFriends.GetFriendPersonaName((SteamID)listFriendRequests.SteamID);
                    ListFriendRequests.Remove(listFriendRequests.SteamID);
                    ListFriendRequests.Add(friendPersonaName, listFriendRequests.SteamID, "Offline");
                }
                if (listFriendRequests.Name == "")
                {
                    string friendPersonaName = steamFriends.GetFriendPersonaName((SteamID)listFriendRequests.SteamID);
                    ListFriendRequests.Remove(listFriendRequests.SteamID);
                    ListFriendRequests.Add(friendPersonaName, listFriendRequests.SteamID, "Offline");
                }
            }
            Console.WriteLine("[" + Program.BOTNAME + "] - Done! {0} friends.", (object)ListFriends.Get(false).Count);
            FriendsLoaded = true;
        }


        //public static async void GenerateSteamGuard()
        //{
        //    var authenticatorSharedSecret = "defenir key";

        //    var steamGuardCode = await Authenticator.GenerateSteamGuardCode(authenticatorSharedSecret);

        //    Console.Write("[" + Program.BOTNAME + "] - AuthCode: " + steamGuardCode);
        //}


        public static string GetAvatarLink(ulong steamid)
        {
            try
            {
                string SHA1 = BitConverter.ToString(steamFriends.GetFriendAvatar(steamid)).Replace("-", "").ToLower();
                string PreURL = SHA1.Substring(1, 2);
                return AvatarPrefix + PreURL + "/" + SHA1 + AvatarSuffix;
            }
            catch (Exception tete)
            {
                return "errorrr: "+tete;
            }

        }
        #region Friends Methods
        public static int GetMaxFriends()
        {
            if (maxfriendCount == 0)
            {
                try
                {
                    var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                    foreach (var a in list.Accounts)
                    {
                        if (a.username == CurrentUsername)
                        {
                            APIKey = a.APIWebKey;
                        }
                    }

                    //  if (APIKey == "undefined" || APIKey.Length == 0)
                    if (string.IsNullOrEmpty(APIKey))
                    {
                        InfoForm.InfoHelper.CustomMessageBox.Show("Alert", "Register your api key and set it on Accounts.json");
                        Process.Start("https://steamcommunity.com/dev/apikey");
                        throw new System.ArgumentException("Register your api key and set it on Accounts.json - https://steamcommunity.com/dev/apikey", "devapi missing");
                    }

                    using (var webClient = new WebClient())
                    {
                        var json = webClient.DownloadString("http://api.steampowered.com/IPlayerService/GetSteamLevel/v1/?key=" + APIKey + "&steamid=" + steamClient.SteamID.ConvertToUInt64());
                        var welcome = SteamPlayerLevel.PlayerLevel.FromJson(json);
                        maxfriendCount = 250 + (5 * Convert.ToInt32(welcome.Response.PlayerLevel));
                        Console.WriteLine("[" + Program.BOTNAME + "] - Level: " + welcome.Response.PlayerLevel + " !");
                    }
                }
                catch (Exception)
                {
                    InfoForm.InfoHelper.CustomMessageBox.Show("Alert", "Error while reading the steam level of own profile. \n Is steam profile even configured ?");
                    maxfriendCount = 250;
                }
            }

            return maxfriendCount;
        }

        static void OnFriendsList(SteamFriends.FriendsListCallback obj)
        {
            LoadFriends();

            List<SteamID> newFriends = new List<SteamID>();

            foreach (SteamFriends.FriendsListCallback.Friend friend in obj.FriendList)
            {
                switch (friend.SteamID.AccountType)
                {
                    // case EAccountType.Clan:
                    //     if (friend.Relationship == EFriendRelationship.RequestRecipient)
                    //DeclineGroupInvite(friend.SteamID);
                    //    break;

                    default:
                        CreateFriendsListIfNecessary();

                        if (friend.Relationship == EFriendRelationship.None)
                        {
                            steamFriends.RemoveFriend(friend.SteamID);
                        }
                        else if (friend.Relationship == EFriendRelationship.RequestRecipient)
                        {
                            if (!Friends.Contains(friend.SteamID))
                            {
                                Friends.Add(friend.SteamID);
                                newFriends.Add(friend.SteamID);
                            }
                        }
                        else if (friend.Relationship == EFriendRelationship.RequestInitiator)
                        {
                            if (!Friends.Contains(friend.SteamID))
                            {
                                Friends.Add(friend.SteamID);
                                newFriends.Add(friend.SteamID);
                            }
                        }
                        break;
                }
            }

            //Console.WriteLine("Recorded steam friends : {0} / {1}", steamFriends.GetFriendCount(), GetMaxFriends());
            Console.WriteLine("[" + Program.BOTNAME + "] Recorded steam friends : {0}", steamFriends.GetFriendCount());

            //Console.WriteLine("Max;" + GetMaxFriends());
            // if (steamFriends.GetFriendCount() == GetMaxFriends())
            // {
            // Console.WriteLine("Too much friends. Removing one.");
            // check this shit
            //steamFriends.SendChatMessage(steamID, EChatEntryType.ChatMsg, "Sorry, I had to remove you because my friend list is too small ! Feel free to add me back anytime !");
            //steamFriends.RemoveFriend(steamID);
            // }
        }

        public static void CreateFriendsListIfNecessary()
        {
            if (Friends != null)
                return;

            Friends = new List<SteamID>();
            for (int i = 0; i < steamFriends.GetFriendCount(); i++)
            {

                SteamID allfriends = steamFriends.GetFriendByIndex(i);
                if (allfriends.ConvertToUInt64() != 0)
                {
                    Friends.Add(allfriends.ConvertToUInt64());

                    //Friends.Add(steamFriends.GetFriendByIndex(i));
                }
            }
        }


        static void OnFriendMsg(SteamFriends.FriendMsgCallback callback) // Auto MSG
        {
            if (ChatLogger == true && callback.EntryType == EChatEntryType.ChatMsg)
            {

                ulong FriendID = callback.Sender;
                string Message = callback.Message; Message = Regex.Replace(Message, @"\t|\n|\r", ""); //741iq

                string Separator = "───────────────────";

                string pathLog = Program.ChatLogsFolder + @"\" + steamClient.SteamID.ConvertToUInt64() + @"\[" + FriendID + "] - " + steamFriends.GetFriendPersonaName(FriendID) + ".txt";
                string FinalMsg = "[" + DateTime.Now + "] " + steamFriends.GetFriendPersonaName(FriendID) + ": " + Message;

                if (File.Exists(pathLog))
                {
                    string[] LastDate = File.ReadLines(pathLog).Last().Split(' '); LastDate[0] = LastDate[0].Substring(1);

                    using (var tw = new StreamWriter(pathLog, true))
                        if (LastDate[0] != DateTime.Now.Date.ToShortDateString())
                        {
                            tw.WriteLine(Separator + "\n" + FinalMsg);
                        }
                        else
                        {
                            tw.WriteLine(FinalMsg);
                        }
                }
                else
                {
                    FileInfo file = new FileInfo(pathLog);
                    file.Directory.Create();
                    File.WriteAllText(pathLog, FinalMsg + "\n");
                }
            }


            if (callback.EntryType == EChatEntryType.ChatMsg)
            {
                var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                foreach (var a in list.Accounts)
                {
                    if (a.username == CurrentUsername)
                    {
                        CurrentAdmin = a.AdminID;
                    }
                }

                if (callback.Sender == CurrentAdmin)
                {
                    string command = callback.Message.Split(' ')[0];
                    string message = callback.Message.Replace(command, "");

                    switch (command)
                    {
                        case ".pcoff":
                            var shutdown = new ProcessStartInfo("shutdown", "/s /t 0") { CreateNoWindow = true, UseShellExecute = false };
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Going down... :c" + "\r\n\r\n" + Program.BOTNAME);
                            Thread.Sleep(100);
                            Process.Start(shutdown);
                            break;
                        case ".close":
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Closing... :c" + "\r\n\r\n" + Program.BOTNAME);
                            Thread.Sleep(100);
                            Environment.Exit(1);
                            break;
                        case ".logoff":
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Logged off... :c" + "\r\n\r\n" + Program.BOTNAME);
                            Thread.Sleep(100);
                            Logout();
                            break;
                        case ".play":
                            string clearGames = callback.Message.Replace(".play", "");
                            List<uint> gameuints = new List<uint>();
                            foreach (var a in JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile)).Accounts)
                            {
                                if (a.username == AccountLogin.CurrentUsername)
                                {
                                    if (a.Games.Count == 0)
                                    {
                                        steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Error: no appids in accounts.json. Use Gather AppIDS" + "\r\n\r\n" + Program.BOTNAME);
                                    }
                                    else
                                    {
                                        if (a.Games.Count >= 32)
                                        {
                                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Error: Max games allowed 32." + "\r\n\r\n" + Program.BOTNAME);
                                            return;
                                        }
                                        for (int i = 0; i < a.Games.Count; i++)
                                        {
                                            gameuints.Add(a.Games[i].app_id);
                                        }

                                        if (clearGames.Length != 0)
                                        {
                                            PlayGames(gameuints, clearGames + " | MercuryBOT");
                                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Playing..." + "\r\n\r\n" + Program.BOTNAME);

                                        }
                                        else
                                        {
                                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Error: Please enter a custom name after '.play custom name game'" + "\r\n\r\n" + Program.BOTNAME);
                                        }
                                    }
                                }
                            }
                            break;
                        case ".non":
                            string clearNoN = callback.Message.Replace(".non ", "");
                            if (clearNoN.Length < 50)
                            {
                                PlayNonSteamGame(clearNoN + " | MercuryBOT");
                                steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Playing: " + clearNoN + "\r\n\r\n" + Program.BOTNAME);
                            }
                            else
                            {
                                steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Error: Only 50 chars allowed." + "\r\n\r\n" + Program.BOTNAME);
                            }
                            break;
                        case ".stopgames":
                            StopGames();
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Stopping games." + "\r\n\r\n" + Program.BOTNAME);

                            break;
                        case "trolha":
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "https://steamcommunity.com/profiles/76561198041931474" + "\r\n\r\n" + Program.BOTNAME);
                            break;
                    }
                }

                if (AwayMsg == true || isAwayState == true)
                {
                    steamFriends.SendChatMessage(callback.Sender, EChatEntryType.ChatMsg, MessageString + "\r\n\r\n" + Program.BOTNAME);
                }

                if (AwayCustomMessageList == true)
                {
                    Random random = new Random();
                    int r = 0;
                    List<string> CMessages = new List<string>();// secalhar nem criar lista, secalhar usar  a lista do json e pegar na random direta ai
                    CMessages.Add("Im using MercuryBOT! - https://github.com/sp0ok3r/Mercury"); // *

                    foreach (var a in JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile)).Accounts)
                    {
                        if (a.username == CurrentUsername) // get user name com steamclient 
                        {
                            for (int i = 0; i < a.AFKMessages.Count; i++)
                            {
                                CMessages.Add(a.AFKMessages[i].Message);
                            }
                            r = random.Next(0, a.AFKMessages.Count); // +1 ? por causa da de cima?
                        }
                    }
                    steamFriends.SendChatMessage(callback.Sender, EChatEntryType.ChatMsg, CMessages[r] + "\r\n\r\n" + Program.BOTNAME);
                }
            }
        }


        static void OnFriendEchoMsg(SteamFriends.FriendMsgEchoCallback callback)
        {
            if (ChatLogger == true && callback.EntryType == EChatEntryType.ChatMsg)
            {
                ulong FriendID = callback.Recipient;
                string Message = callback.Message; Message = Regex.Replace(Message, @"\t|\n|\r", "");

                string Separator = "───────────────────";

                string pathLog = Program.ChatLogsFolder + @"\" + steamClient.SteamID.ConvertToUInt64() + @"\[" + FriendID + "] - " + steamFriends.GetFriendPersonaName(FriendID) + ".txt";
                string FinalMsg = "[" + DateTime.Now + "] " + steamFriends.GetPersonaName() + ": " + Message;

                if (File.Exists(pathLog))
                {
                    string[] LastDate = File.ReadLines(pathLog).Last().Split(' '); LastDate[0] = LastDate[0].Substring(1);
                    using (var tw = new StreamWriter(pathLog, true))
                    {

                        if (LastDate[0] != DateTime.Now.Date.ToShortDateString())
                        {
                            tw.WriteLine(Separator + "\n" + FinalMsg);
                        }
                        else
                        {
                            tw.WriteLine(FinalMsg);
                        }
                    }
                }
                else
                {
                    FileInfo file = new FileInfo(pathLog);
                    file.Directory.Create();
                    File.WriteAllText(pathLog, FinalMsg + "\n");
                }
            }
        }

        public static void SendMsg2AllFriends(string message)
        {
            for (int i = 0; i <= steamFriends.GetFriendCount(); i++)
            {
                SteamID allfriends = steamFriends.GetFriendByIndex(i);
                if (allfriends.ConvertToUInt64() != 0)
                {
                    steamFriends.SendChatMessage(allfriends.ConvertToUInt64(), EChatEntryType.ChatMsg, message + "\r\n\r\n" + Program.BOTNAME);
                    Thread.Sleep(2500);
                }
            }
            InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Sent message to: " + steamFriends.GetFriendCount() + " Friends.");
        }

        public static void RemoveFriend(ulong goodbye)
        {
            steamFriends.SendChatMessage(goodbye, EChatEntryType.ChatMsg, "Have an amazing day!" + "\r\n\r\n" + Program.BOTNAME);
            Thread.Sleep(500);
            steamFriends.RemoveFriend(goodbye);
        }

        #endregion

        static void OnSteamNameChange(SteamFriends.PersonaChangeCallback callback)
        {
            UserPersonaName = callback.Name;
            Console.WriteLine("[" + Program.BOTNAME + "] - Name changed to: " + callback.Name);
        }
        public static string GetPersonaName(ulong steamid)
        {
            return steamFriends.GetFriendPersonaName((SteamID)steamid);
        }

        public static void DeleteSelectedComment(string CID, string ProfileOrClan)
        {
            var DeleteCommentData = new NameValueCollection
            {
                { "gidcomment", CID},
                { "start", "0" },
                { "count", "1" },
                { "sessionid", steamWeb.SessionId },
                { "feature2", "-1" }
            };

            string resp = steamWeb.Fetch("https://steamcommunity.com/comment/" + ProfileOrClan + "/delete/" + steamClient.SteamID.ConvertToUInt64() + "/-1/", "POST", DeleteCommentData);
            if (resp != String.Empty)
            {
                Console.WriteLine(resp);//debug
            }
        }
        public static void gatherWebApiKey()//teste
        {
            string resp = steamWeb.Fetch("https://steamcommunity.com/dev/apikey?l=english", "GET");

            if (resp != String.Empty)
            {

                var parser = new HtmlParser();
                var document = parser.ParseDocument(resp);

                if (document.QuerySelector("div[id='mainContents'] > h2").TextContent == "Access Denied")
                {
                    InfoForm.InfoHelper.CustomMessageBox.Show("Error", document.QuerySelector("div[id='bodyContents_lo'] > p").TextContent);
                    return;
                }


                var webkeySet = document.QuerySelector("div[id='bodyContents_ex'] > p"); //650ip
                if (webkeySet.TextContent.Contains("Key:"))
                {
                    var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                    foreach (var a in list.Accounts)
                    {
                        if (a.username == AccountLogin.CurrentUsername)
                        {
                            a.APIWebKey = webkeySet.TextContent.Replace("Key: ", ""); //861ip
                        }
                    }
                    string output = JsonConvert.SerializeObject(list, Formatting.Indented);
                    File.WriteAllText(Program.AccountsJsonFile, output);
                }
                else
                {

                    var SetWebApi = new NameValueCollection
                    {
                        { "domain", "localhost" },
                        { "agreeToTerms", "agreed"},
                        { "sessionid", steamWeb.SessionId },
                        { "Submit", "Register"}
                    };

                    string ObtainKey = steamWeb.Fetch("https://steamcommunity.com/dev/registerkey?l=english", "POST", SetWebApi);
                    if (ObtainKey != String.Empty)
                    {
                        gatherWebApiKey();
                    }
                }
            }
        }

        #region Change State Name Persona Flag
        public static void ChangeCurrentState(EPersonaState state)
        {
            steamFriends.SetPersonaState(state);
            Console.WriteLine("[" + Program.BOTNAME + "] - State Changed to: " + state);
        }

        public static void ChangeCurrentName(string Name)
        {
            steamFriends.SetPersonaName(Name);
            Console.WriteLine("[" + Program.BOTNAME + "] - Name Changed to: " + Name);
        }



        public static void ChangePersonaFlags(uint uimode)
        {
            ClientMsgProtobuf<CMsgClientChangeStatus> request2 = new ClientMsgProtobuf<CMsgClientChangeStatus>(EMsg.ClientChangeStatus)
            {
                Body = { persona_state_flags = uimode }
            };
            steamClient.Send(request2);
        }
        #endregion

        private void ChatMode()
        {
            //https://github.com/SteamRE/SteamKit/issues/561
            ClientMsgProtobuf<CMsgClientUIMode> request = new ClientMsgProtobuf<CMsgClientUIMode>(EMsg.ClientCurrentUIMode) { Body = { chat_mode = 2 } };
            steamClient.Send(request);
        }
        #region PlayGames
        public static void PlayNonSteamGame(string customgame)
        {
            var playGame = new ClientMsgProtobuf<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayed);

            playGame.Body.games_played.Add(new CMsgClientGamesPlayed.GamePlayed { game_id = 12350489788975939584, game_extra_info = customgame });
            steamClient.Send(playGame);

            Console.WriteLine("[" + Program.BOTNAME + "] - Now playing: " + customgame);
        }

        public static void PlayGames(List<uint> gameIDs, string NonSteam)
        {
            ClientMsgProtobuf<CMsgClientGamesPlayed> request = new ClientMsgProtobuf<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayed);

            if (NonSteam.ToString() != "disable")
            {
                request.Body.games_played.Add(new CMsgClientGamesPlayed.GamePlayed
                {
                    // game_id = 12350489788975939584,
                    game_id = new GameID
                    {
                        AppType = GameID.GameType.P2P,
                        ModID = uint.MaxValue
                    },
                    //game_extra_infoSpecified
                    game_extra_info = NonSteam
                });
            }

            foreach (uint gameID in gameIDs.Where(gameID => gameID != 0))
            {
                request.Body.games_played.Add(new CMsgClientGamesPlayed.GamePlayed { game_id = new GameID(gameID) });
            }
            steamClient.Send(request);
        }



        public static void StopGames()
        {
            ClientMsgProtobuf<CMsgClientGamesPlayed> request = new ClientMsgProtobuf<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayed);

            request.Body.games_played.Add(new CMsgClientGamesPlayed.GamePlayed { game_id = new GameID(0) });

            steamClient.Send(request);
        }
        #endregion

        public static void RedeemKey(string _key)
        {
            ClientMsgProtobuf<CMsgClientRegisterKey> registerKey = new ClientMsgProtobuf<CMsgClientRegisterKey>(EMsg.ClientRegisterKey)
            {
                Body = { key = _key }
            };
            steamClient.Send(registerKey);
        }

        public static void UserClanIDS()
        {
            List<SteamID> AllGroups = new List<SteamID>();
            for (var i = 0; i < steamFriends.GetClanCount(); i++)
            {
                SteamID steamIDClan = steamFriends.GetClanByIndex(i);
                if (!ClanDictionary.ContainsKey(steamIDClan))
                {
                    ClanDictionary.Add(steamIDClan, steamFriends.GetClanName(steamIDClan));
                }
            }
        }

        public static void JoinGroup(string groupID)
        {
            var JoinGroup = new NameValueCollection
            {
                { "action","join"},
                { "sessionID", steamWeb.SessionId}
            };

            string resp = steamWeb.Fetch("https://steamcommunity.com/gid/" + groupID, "POST", JoinGroup);

            if (resp != String.Empty)
            {
            }
        }

        public static void LeaveGroup(string groupID, string groupName)
        {
            var LeaveGroup = new NameValueCollection
            {
                { "sessionID", steamWeb.SessionId},
                { "action","leaveGroup"},
                { "groupId", groupID}
            };

            string resp = steamWeb.Fetch("https://steamcommunity.com/profiles/" + steamClient.SteamID.ConvertToUInt64() + "/home_process", "POST", LeaveGroup);

            if (resp != String.Empty)
            {

                //  InfoForm.InfoHelper.CustomMessageBox.Show("Left successfully " + groupName + " !");

            }
        }

        public static void ClearAliases()
        {
            var ClearAliases = new NameValueCollection
            {
                { "sessionid", steamWeb.SessionId}
            };

            string resp = steamWeb.Fetch("https://steamcommunity.com/profiles/" + steamClient.SteamID.ConvertToUInt64() + "/ajaxclearaliashistory", "POST", ClearAliases);

            if (resp != String.Empty)
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Aliases Clear!");
            }
        }
        public void ClearUnreadMessages()
        {
            steamFriends.RequestOfflineMessages();
        }
        public static IDictionary<string, int> GetProfileSettings()
        {

            if (AccountLogin.IsLoggedIn == true)
            {
                try
                {
                    string resp = steamWeb.Fetch("https://steamcommunity.com/profiles/" + steamClient.SteamID.ConvertToUInt64() + "/edit/settings", "GET");
                    //File.WriteAllText(Program.ExecutablePath + @"\ey.html", resp);


                    var parser = new HtmlParser();
                    var document = parser.ParseDocument(resp);
                    var ReadPrivacyDiv = document.QuerySelector("div.ProfileReactRoot").GetAttribute("data-privacysettings");

                    var renderPrivacySettings = RenderProfilePrivacy.FromJson(ReadPrivacyDiv);

                    var dictionary = new Dictionary<string, int>();
                    dictionary.Add("PrivacyProfile", renderPrivacySettings.PrivacySettings.PrivacyProfile);
                    dictionary.Add("PrivacyFriendsList", renderPrivacySettings.PrivacySettings.PrivacyFriendsList);
                    dictionary.Add("PrivacyPlaytime", renderPrivacySettings.PrivacySettings.PrivacyPlaytime);
                    dictionary.Add("PrivacyOwnedGames", renderPrivacySettings.PrivacySettings.PrivacyOwnedGames);
                    dictionary.Add("PrivacyInventoryGifts", renderPrivacySettings.PrivacySettings.PrivacyInventoryGifts);
                    dictionary.Add("PrivacyInventory", renderPrivacySettings.PrivacySettings.PrivacyInventory);
                    dictionary.Add("ECommentPermission", renderPrivacySettings.ECommentPermission);


                    Console.WriteLine(steamWeb.SessionId);

                    return dictionary;

                }
                catch (Exception te)
                {
                    InfoForm.InfoHelper.CustomMessageBox.Show("Error", "SessionID not valid, login again.");
                    Console.WriteLine(te);
                    return null;
                }
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "not logged");
                return null;
            }

        }


        public static void ProfileSettings(int Profile, int Inventory, int Gifts, int OwnedGames, int Playtime, int FriendsList, int Comment)
        {
            var ProfileSettings = new NameValueCollection
            {
                { "sessionid", steamWeb.SessionId },// Unknown,Private, FriendsOnly,Public
                { "Privacy","{\"PrivacyProfile\":"+Profile+",\"PrivacyInventory\":"+Inventory+",\"PrivacyInventoryGifts\":"+Gifts+",\"PrivacyOwnedGames\":"+OwnedGames+",\"PrivacyPlaytime\":"+Playtime+",\"PrivacyFriendsList\":"+FriendsList+"}"},
                              {"eCommentPermission" ,""+Comment}//FriendsOnly,Public,Private
            };

            string resp = steamWeb.Fetch("https://steamcommunity.com/profiles/" + steamClient.SteamID.ConvertToUInt64() + "/ajaxsetprivacy/", "POST", ProfileSettings);

            if (resp != String.Empty && resp.Contains("success\":1"))
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Profile settings set!");
            }
            else
            {
                Console.WriteLine("erro:\n" + resp);
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", resp);
            }
        }


        public static void UIMode(uint x)
        {
            ClientMsgProtobuf<CMsgClientUIMode> uiMode = new ClientMsgProtobuf<CMsgClientUIMode>(EMsg.ClientCurrentUIMode)
            {
                Body = { uimode = x }
            };
            steamClient.Send(uiMode);
        }



        public static void Logout()
        {
            isRunning = false;
            IsLoggedIn = false;
            IsWebLoggedIn = false;

            steamUser.LogOff();
            DisconnectedCounter = 0;
            CurrentUsername = null;
        }

    }
}

// remove at end project
class MyListener : IDebugListener
{
    public void WriteLine(string category, string msg)
    {
        Console.WriteLine("[IDebug] 💔 " + category + ": " + msg);
    }
}
