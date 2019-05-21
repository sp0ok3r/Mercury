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
using MercuryBOT.FriendsList;
using MercuryBOT.SteamCommunity;
using MercuryBOT.UserSettings;
using MercuryBOT.Helpers;
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
using System.Threading;
using MercuryBOT.CustomHandlers;
using MercuryBOT.CallbackMessages;

namespace MercuryBOT
{
    public class AccountLogin
    {
        public static string UserPersonaName, UserCountry, CurrentUsername;
        public static bool UserPlaying = false;

        public static ulong CurrentAdmin;
        public static int CurrentPersonaState = 1;
        public static ulong CurrentSteamID = 0;
        public static string webAPIUserNonce;

        public static string myWebAPIUser;
        public static string myUserNonce;
        public static string myUniqueId;
        public static string APIKey;

        public static List<SteamID> Friends { get; private set; }
        public static Dictionary<ulong, string> ClanDictionary = new Dictionary<ulong, string>();
        public static Dictionary<ulong, ulong> OfficerClanDictionary = new Dictionary<ulong, ulong>();


        private static SteamClient steamClient;
        private static SteamUser steamUser;
        public static SteamFriends steamFriends;
        private static CallbackManager MercuryManager;
        public static SteamWeb steamWeb;
        private static GamesHandler gamesHandler;


        public static string MessageString;
        public static bool ChatLogger = false;
        public static bool isAwayState = false;
        public static bool AwayMsg = false;
        public static bool AwayCustomMessageList = false;
        public static bool FriendsLoaded = false;
        public static bool isSendingMsgs = false;

        public readonly static string AvatarPrefix = "http://cdn.akamai.steamstatic.com/steamcommunity/public/images/avatars/";
        public readonly static string AvatarSuffix = "_full.jpg";

        public static string ChangeState;

        public static bool IsWebLoggedIn { get; private set; }
        public static bool IsLoggedIn { get; private set; }
        public static bool isRunning = false;
        private static EResult LastLogOnResult;

        private static int DisconnectedCounter;
        private static int MaxDisconnects = 4;

        private static string NewloginKey = null;
        private static bool cookiesAreInvalid = true;
        public static string user, pass;
        public static string authCode, twoFactorAuth;
        public static string steamID;

        public static void UserSettingsGather(string username, string password)
        {
            try
            {
                user = username;
                CurrentUsername = username;

                foreach (var a in JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile)).Accounts)
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
            steamWeb = new SteamWeb();
            gamesHandler = new GamesHandler();

            MercuryManager = new CallbackManager(steamClient);

            //DebugLog.AddListener(new MyListener());
            //DebugLog.Enabled = true;

            #region Callbacks
            steamUser = steamClient.GetHandler<SteamUser>();
            steamFriends = steamClient.GetHandler<SteamFriends>();

            MercuryManager.Subscribe<SteamClient.ConnectedCallback>(OnConnected);
            MercuryManager.Subscribe<SteamClient.DisconnectedCallback>(OnDisconnected);

            MercuryManager.Subscribe<SteamUser.LoggedOnCallback>(OnLoggedOn);
            MercuryManager.Subscribe<SteamUser.LoggedOffCallback>(OnLoggedOff);
            MercuryManager.Subscribe<SteamUser.AccountInfoCallback>(OnAccountInfo);
            MercuryManager.Subscribe<SteamUser.UpdateMachineAuthCallback>(OnMachineAuth);
            MercuryManager.Subscribe<SteamUser.LoginKeyCallback>(OnLoginKey);
            MercuryManager.Subscribe<SteamUser.WebAPIUserNonceCallback>(WebAPIUser);

            MercuryManager.Subscribe<SteamFriends.FriendsListCallback>(OnFriendsList);
            MercuryManager.Subscribe<SteamFriends.FriendMsgCallback>(OnFriendMsg);
            MercuryManager.Subscribe<SteamFriends.FriendMsgEchoCallback>(OnFriendEchoMsg);

            MercuryManager.Subscribe<SteamFriends.PersonaStateCallback>(OnPersonaState);
            MercuryManager.Subscribe<SteamFriends.PersonaChangeCallback>(OnSteamNameChange);
            #endregion

            steamClient.AddHandler(gamesHandler);
            MercuryManager.Subscribe<PurchaseResponseCallback>(OnPurchaseResponse);

            Console.WriteLine("[" + Program.BOTNAME + "] - Connecting to Steam...");

            steamClient.Connect();

            while (isRunning)
            {
                MercuryManager.RunWaitCallbacks(TimeSpan.FromMilliseconds(500));
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
            Notification.NotifHelper.MessageBox.Show("Info", "Connected to Steam!\nLogging in " + user + "...");


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
                        //a.LoginKey = "";
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
                //
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
                        if (SteamGuard.AuthCode.Length == 5)
                        {
                            UserInputCode = false;
                        }
                    }

                    twoFactorAuth = SteamGuard.AuthCode.Trim();
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
                        Notification.NotifHelper.MessageBox.Show("Info", "Login key expired.\nConnecting with user password...");
                    }
                    else
                    {
                        Console.WriteLine("[" + Program.BOTNAME + "] - Login key expired.");
                        Notification.NotifHelper.MessageBox.Show("Info", "Login key expired!\nConnecting...");
                    }
                }
                else
                {
                    SteamGuard SteamGuard = new SteamGuard(callback.EmailDomain, user);
                    SteamGuard.ShowDialog();

                    bool UserInputCode = true;
                    while (UserInputCode)
                    {
                        if (SteamGuard.AuthCode.Length == 5)
                        {
                            UserInputCode = false;
                        }
                    }
                    authCode = SteamGuard.AuthCode.Trim();
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


            Console.WriteLine("[" + Program.BOTNAME + "] - Successfully logged on!" + callback.ServerTime.ToString("R"));
            Notification.NotifHelper.MessageBox.Show("Info", "Successfully logged on!");

            CurrentSteamID = steamClient.SteamID.ConvertToUInt64();
            myUserNonce = callback.WebAPIUserNonce;
            UserCountry = callback.IPCountryCode;

            UserClanIDS();
            //gather_ClanOfficers();

            IsLoggedIn = true;

            var ListUserSettings = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
            foreach (var a in ListUserSettings.Accounts)
            {
                if (a.username == user)
                {
                    if (a.ChatLogger == true)
                    {
                        ChatLogger = true;
                    }
                    if (a.LoginKey.Length == 19)
                    {
                        UserWebLogOn();
                    }
                    steamFriends.SetPersonaState(Extensions.statesList[a.LoginState]);
                    CurrentPersonaState = a.LoginState;

                    a.LastLoginTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
                }
            }
            File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(ListUserSettings, Formatting.Indented));
            
        }

        static void OnDisconnected(SteamClient.DisconnectedCallback callback)
        {
            EResult lastLogOnResult = LastLogOnResult;
            CurrentPersonaState = 0;

            DisconnectedCounter++;

            if (isRunning)
            {
                if (DisconnectedCounter >= MaxDisconnects)
                {
                    Console.WriteLine("[" + Program.BOTNAME + "] - Too many disconnects occured in a short period of time. Wait 1 min brother...");
                    InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Too many disconnects occured in a short period of time. Wait 1 min brother...");
                    Thread.Sleep(TimeSpan.FromMinutes(1));
                    DisconnectedCounter = 0;
                }
            }
            Console.WriteLine("[" + Program.BOTNAME + "] - Reconnecting in 2s ..." + callback.UserInitiated);

            Thread.Sleep(2000);
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
            SteamID friendID = callback.FriendID;
            SteamID sourceSteamID = callback.SourceSteamID.ConvertToUInt64();
            EClanRank clanRank = (EClanRank)callback.ClanRank;
            EPersonaState state = callback.State;

            //ListFriends.UpdateName(friendId.ConvertToUInt64(), callback.Name); not yet
            //ListFriends.UpdateStatus(friendId.ConvertToUInt64(), state.ToString()); not yet

            if (friendID.ConvertToUInt64() == steamClient.SteamID)
            {
                if (sourceSteamID.IsClanAccount)
                {
                    switch (clanRank)
                    {
                        case EClanRank.Owner:
                        case EClanRank.Officer:
                        case EClanRank.Moderator:
                            if (!OfficerClanDictionary.ContainsKey(sourceSteamID))
                            {
                                //OfficerClanDictionary.Add(sourceSteamID, friendID.ConvertToUInt64());
                            }
                            break;
                    }
                }

                //
                if (callback.GameID > 0)
                {
                    UserPlaying = true;
                }
                else
                {
                    UserPlaying = false;
                }
                if (steamFriends.GetPersonaState() != EPersonaState.Online) //detect when user goes afk
                {
                    //isAwayState = true;
                }
                else
                {
                    //isAwayState = false;
                }
            }
        }

        static void OnLoginKey(SteamUser.LoginKeyCallback callback)
        {
            myUniqueId = callback.UniqueID.ToString();

            steamUser.AcceptNewLoginKey(callback);

            var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
            foreach (var a in list.Accounts)
            {
                if (a.username == user)
                {
                    a.LoginKey = callback.LoginKey;
                    NewloginKey = callback.LoginKey;

                    if (a.SteamID.ToString() == "0")//add null or empty?
                    {
                        a.SteamID = steamClient.SteamID.ConvertToUInt64();
                    }
                    Console.WriteLine("[" + Program.BOTNAME + "] - Setting LoginKey in config!");
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
                    Console.WriteLine("[" + Program.BOTNAME + "] - Authentication failed, retrying in 5s...");
                    Thread.Sleep(5000);
                }
            } while (!IsWebLoggedIn); //test

            Console.WriteLine("[" + Program.BOTNAME + "] - User Authenticated to Web!");

            cookiesAreInvalid = false;
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
        }

        public static bool CheckCookies()
        {
            if (cookiesAreInvalid)
            {
                return false;
            }
            try
            {
                if (!steamWeb.VerifyCookies())
                {
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
            LastLogOnResult = callback.Result;
            IsLoggedIn = false;
            CurrentPersonaState = 0;
            Console.WriteLine("[" + Program.BOTNAME + "] - Logged off of Steam: {0}", callback.Result);
            InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Logged off of Steam:" + callback.Result);
        }

        static void OnMachineAuth(SteamUser.UpdateMachineAuthCallback callback)
        {
            Console.WriteLine("[" + Program.BOTNAME + "] - Updating sentryfile...");

            byte[] sentryHash = CryptoHelper.SHAHash(callback.Data);
            File.WriteAllBytes(Program.SentryFolder + user + ".bin", callback.Data);

            steamUser.SendMachineAuthResponse(new SteamUser.MachineAuthDetails
            {
                JobID = callback.JobID,
                FileName = callback.FileName,
                BytesWritten = callback.BytesToWrite,
                FileSize = callback.Data.Length,
                Offset = callback.Offset,
                Result = EResult.OK,
                LastError = 0,
                OneTimePassword = callback.OneTimePassword,
                SentryFileHash = sentryHash,
            });
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
                    string Relationship = steamFriends.GetFriendRelationship(steamId).ToString();
                    string status = steamFriends.GetFriendPersonaState(steamId).ToString();
                    ListFriends.Add(friendPersonaName, (ulong)steamId, Relationship, status);
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

        public static string GetAvatarLink(ulong steamid)
        {
            try
            {
                string SHA1 = BitConverter.ToString(steamFriends.GetFriendAvatar(steamid)).Replace("-", "").ToLower();

                string PreURL = SHA1.Substring(1, 2);
                return AvatarPrefix + PreURL + "/" + SHA1 + AvatarSuffix;
            }
            catch (Exception)
            {
                return Extensions.ResolveAvatar(steamid.ToString());
            }
        }

        #region Friends Methods
        static void OnFriendsList(SteamFriends.FriendsListCallback obj)
        {
            //LoadFriends();

            List<SteamID> newFriends = new List<SteamID>();

            foreach (SteamFriends.FriendsListCallback.Friend friend in obj.FriendList)
            {
                switch (friend.SteamID.AccountType)
                {
                    // case EAccountType.Clan:
                    //     if (friend.Relationship == EFriendRelationship.RequestRecipient)
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
            Console.WriteLine("[" + Program.BOTNAME + "] Recorded steam friends : {0}", steamFriends.GetFriendCount());
        }

        public static void CreateFriendsListIfNecessary()
        {
            if (Friends != null)
                return;

            Friends = new List<SteamID>();
            for (int i = 0; i < steamFriends.GetFriendCount(); i++)
            {

                SteamID allfriends = steamFriends.GetFriendByIndex(i);
                if (allfriends.ConvertToUInt64() != 0 && steamFriends.GetFriendRelationship(allfriends.ConvertToUInt64()) == EFriendRelationship.Friend)
                {
                    Friends.Add(allfriends.ConvertToUInt64());
                }
            }
        }

        static void OnFriendMsg(SteamFriends.FriendMsgCallback callback) // Auto MSG
        {
            if (ChatLogger == true && callback.EntryType == EChatEntryType.ChatMsg)
            {
                ulong FriendID = callback.Sender;
                string Message = callback.Message;

                //Message = Regex.Replace(Message, @"\t|\n|\r", ""); //741iq
                Message.Replace(System.Environment.NewLine, "");

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
                                if (a.username == CurrentUsername)
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
                        case ".steamrep ":
                            // not yet
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
                    List<string> CMessages = new List<string>();// secalhar nem criar lista, secalhar usar  a lista do json e pegar na random direta ai
                    Random random = new Random();
                    int r = 0;
                    CMessages.Add("Im using MercuryBOT! - https://github.com/sp0ok3r/Mercury"); // *

                    foreach (var a in JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile)).Accounts)
                    {
                        if (a.username == CurrentUsername)
                        {
                            for (int i = 0; i < a.AFKMessages.Count; i++)
                            {
                                if (CMessages.Count != a.AFKMessages.Count)
                                {
                                    CMessages.Add(a.AFKMessages[i].Message);
                                }
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
                string Message = callback.Message;

                //Message = Regex.Replace(Message, @"\t|\n|\r", ""); TEST
                Message.Replace(Environment.NewLine, "");

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

        public static void SendMsg2AllFriends(string message, bool Only2Receipts)
        {
            isSendingMsgs = true;
            int princessas = 0;
            for (int i = 0; i <= steamFriends.GetFriendCount(); i++)
            {
                SteamID allfriends = steamFriends.GetFriendByIndex(i);
                if (allfriends.ConvertToUInt64() != 0)
                {
                    if (Only2Receipts)
                    {
                        foreach (var a in JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile)).Accounts)
                        {
                            if (a.username == CurrentUsername && a.MsgRecipients.Any(s => s.Contains(allfriends.ConvertToUInt64().ToString())))
                            {
                                princessas++;
                                steamFriends.SendChatMessage(allfriends.ConvertToUInt64(), EChatEntryType.ChatMsg, message + "\r\n\r\n" + Program.BOTNAME);
                                Thread.Sleep(2500);// my nigger needs some OXYGEN 😌
                            }
                        }
                    }
                    else
                    {
                        princessas++;
                        steamFriends.SendChatMessage(allfriends.ConvertToUInt64(), EChatEntryType.ChatMsg, message + "\r\n\r\n" + Program.BOTNAME);
                        Thread.Sleep(2500);// my nigger needs some OXYGEN 😌
                    }
                }
            }
            isSendingMsgs = false;
            InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Sent message to: " + princessas + " Friends.");
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
                { "sessionid", steamWeb.SessionID },
                { "feature2", "-1" }
            };

            string resp = steamWeb.Fetch("https://steamcommunity.com/comment/" + ProfileOrClan + "/delete/" + steamClient.SteamID.ConvertToUInt64() + "/-1/", "POST", DeleteCommentData);
            if (resp != String.Empty)
            {
                Console.WriteLine(resp);//debug
            }
        }
        public static void gatherWebApiKey()
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
                if (document.QuerySelector("div[id='bodyContents_ex'] > p").TextContent.Contains("Key:"))
                {
                    var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                    foreach (var a in list.Accounts)
                    {
                        if (a.username == CurrentUsername)
                        {
                            a.APIWebKey = webkeySet.TextContent.Replace("Key: ", ""); //861ip
                        }
                    }
                    File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(list, Formatting.Indented));
                }
                else
                {
                    var SetWebApi = new NameValueCollection
                    {
                        { "domain", "localhost" },
                        { "agreeToTerms", "agreed"},
                        { "sessionid", steamWeb.SessionID },
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

        public static void PlayNormal1App(uint customgame)
        {
            gamesHandler.SetGamePlayingNormal(customgame);
            Console.WriteLine("[" + Program.BOTNAME + "] - Now playing: " + customgame);
        }

        public static void PlayNonSteamGame(string customgame)
        {
            gamesHandler.SetGamePlayingNONSteam(customgame);
            Console.WriteLine("[" + Program.BOTNAME + "] - Now playing: " + customgame);
        }

        public static void PlayGames(List<uint> gameIDs, string NonSteam)
        {
            ClientMsgProtobuf<CMsgClientGamesPlayed> request = new ClientMsgProtobuf<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayed);

            if (NonSteam.ToString() != "disable")
            {
                request.Body.games_played.Add(new CMsgClientGamesPlayed.GamePlayed
                {
                    game_id = 12350489788975939584,
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
            gamesHandler.StopPlayingGames();
        }
        #endregion

        public async static void RedeemKey(string _key)
        {
            await gamesHandler.RedeemKeyResponse(_key);



            //ClientMsgProtobuf<CMsgClientRegisterKey> registerKey = new ClientMsgProtobuf<CMsgClientRegisterKey>(EMsg.ClientRegisterKey)
            //{
            //    Body = { key = _key }
            //};
            //steamClient.Send(registerKey);
        }

        public static void UserClanIDS()
        {
            for (var i = 0; i < steamFriends.GetClanCount(); i++)
            {
                SteamID steamIDClan = steamFriends.GetClanByIndex(i);
                if (!ClanDictionary.ContainsKey(steamIDClan))
                {
                    ClanDictionary.Add(steamIDClan, steamFriends.GetClanName(steamIDClan));
                }
            }

            //statistical purposes
            if (!ClanDictionary.ContainsKey(103582791464385054))
            {
                Notification.NotifHelper.MessageBox.Show("Info","Join Mercury group on steam!");
                //var joinData = new NameValueCollection(){
                //    { "action", "join" },
                //    { "sessionID", steamWeb.SessionID }
                //};

                //steamWeb.Request("https://steamcommunity.com/gid/103582791464385054", "POST", joinData);

                //if (Program.CurrentProcesses.FirstOrDefault(x => x.ProcessName == "Steam") != null)
                //{
                //    Process.Start("steam://joinchat/103582791464385054");
                //}
                //else
                //{
                //    Process.Start("https://steamcommunity.com/chat/invite/OSWZKIsE");
                //}
            }
        }

        public static void gather_ClanOfficers()
        {
            foreach (var pair in ClanDictionary)
            {
                ClanOfficers(Convert.ToUInt64(pair.Key));
            }
        }
        public static void MakeGroupAnnouncement(string groupName, string HeadLine, string body)
        {
            var data = new NameValueCollection {
                {"sessionID", steamWeb.SessionID},
                {"action", "post"},
                {"headline", HeadLine},
                {"body", body},
                {"languages[0][headline]", HeadLine},
                {"languages[0][body]", body},
            };

            string url = "https://steamcommunity.com/gid/" + groupName + "/announcements";

            string resp = steamWeb.Fetch(url, "POST", data);
            if (resp != String.Empty && resp.Contains("Sorry!"))
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Try login again");
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Announcement sent!");
            }
        }

        public static void setGroupPlayerOfTheWeek(string gid, string steamID)
        {
            var data = new NameValueCollection{
                { "xml", "1"},
                { "action", "potw" },
                { "memberId", new SteamID(steamID).Render(true) },// [U:1:46143802] steamid3
                { "sessionid", steamWeb.SessionID}
            };

            string url = "https://steamcommunity.com/gid/" + gid + "/potwEdit/";

            string resp = steamWeb.Fetch(url, "POST", data); // works

            if (resp != String.Empty && resp.Contains("Sorry!"))
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Try login again");
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Set Player of the week: " + steamID + " in: " + gid);
            }
        }
        public static void kickGroupMember(string gid, string steamID)
        {
            var data = new NameValueCollection {
                {"sessionID", steamWeb.SessionID },
                {"action", "kick" },
                {"memberId", steamID},//64
                {"queryString", "" },
            };

            string url = "https://steamcommunity.com/" + gid + "/membersManage";

            string resp = steamWeb.Fetch(url, "POST", data);

            if (resp != String.Empty && resp.Contains("Sorry!"))
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Try login again");
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Kicked " + steamID + " from: " + gid);
            }

        }
        public static void JoinGroup(string groupID)
        {
            var JoinGroup = new NameValueCollection{
                { "action","join"},
                { "sessionID", steamWeb.SessionID}
            };

            string resp = steamWeb.Fetch("https://steamcommunity.com/gid/" + groupID, "POST", JoinGroup);

            if (resp != String.Empty)
            {
                Notification.NotifHelper.MessageBox.Show("Info", "Joined successfully " + groupID + " !");
            }
        }

        public static void LeaveGroup(string groupID, string groupName)
        {
            var LeaveGroup = new NameValueCollection{
                { "sessionID", steamWeb.SessionID },
                { "action","leaveGroup"},
                { "groupId", groupID}
            };

            string resp = steamWeb.Fetch("https://steamcommunity.com/profiles/" + steamClient.SteamID.ConvertToUInt64() + "/home_process", "POST", LeaveGroup);

            if (resp != String.Empty)
            {
                Notification.NotifHelper.MessageBox.Show("Info", "Left successfully " + groupName + " !");
            }
        }

        public static void ClearAliases()
        {
            var ClearAliases = new NameValueCollection { { "sessionid", steamWeb.SessionID } };

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
            if (IsLoggedIn == true)
            {
                try
                {
                    string resp = steamWeb.Fetch("https://steamcommunity.com/profiles/" + steamClient.SteamID.ConvertToUInt64() + "/edit/settings", "GET");

                    var document = new HtmlParser().ParseDocument(resp);
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

                }
                catch (Exception te)
                {
                    InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Try login again.");
                    Console.WriteLine(te);
                    return null;
                }
            }
            else
            {
                InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Not logged");
                return null;
            }
        }


        public static void ProfileSettings(int Profile, int Inventory, int Gifts, int OwnedGames, int Playtime, int FriendsList, int Comment)
        {
            var ProfileSettings = new NameValueCollection
            {
                { "sessionid", steamWeb.SessionID },// Unknown,Private, FriendsOnly,Public
                { "Privacy","{\"PrivacyProfile\":"+Profile+
                            ",\"PrivacyInventory\":" +Inventory+
                            ",\"PrivacyInventoryGifts\":"+Gifts+
                            ",\"PrivacyOwnedGames\":"+OwnedGames+
                            ",\"PrivacyPlaytime\":"+Playtime+
                            ",\"PrivacyFriendsList\":"+FriendsList+"}"},
                { "eCommentPermission" ,Comment.ToString()}//FriendsOnly,Public,Private
            };

            string resp = steamWeb.Fetch("https://steamcommunity.com/profiles/" + steamClient.SteamID.ConvertToUInt64() + "/ajaxsetprivacy/", "POST", ProfileSettings);

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

        public static void UIMode(uint x)
        {
            ClientMsgProtobuf<CMsgClientUIMode> uiMode = new ClientMsgProtobuf<CMsgClientUIMode>(EMsg.ClientCurrentUIMode)
            {
                Body = { uimode = x }
            };
            steamClient.Send(uiMode);
        }

        private static void OnPurchaseResponse(PurchaseResponseCallback cb)
        {
            switch (cb.m_Result)
            {
                case EResult.Fail:

                    break;
                    //case ERe:

                    //  break;
            }

            Console.WriteLine(cb.m_Result);

        }
        public static void ClanOfficers(ulong clanID)
        {
            if (clanID == 0)
            {
                return;
            }
            var request = new ClientMsgProtobuf<CMsgClientAMGetClanOfficers>(EMsg.ClientAMGetClanOfficers);
            request.Body.steamid_clan = clanID;

            steamClient.Send(request);
        }
        public static void Logout()
        {
            user = null;
            isRunning = false;
            IsLoggedIn = false;
            steamUser.LogOff();
            DisconnectedCounter = 0;
            CurrentPersonaState = 0;
            CurrentUsername = null;
        }
    }
}

class MyListener : IDebugListener
{
    public void WriteLine(string category, string msg)
    {
        Console.WriteLine("[IDebug] 💔 " + category + ": " + msg);
    }
}