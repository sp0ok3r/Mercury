﻿using MercuryBOT.FriendsList;
using MercuryBOT.SteamCommunity;
using MercuryBOT.UserSettings;
using MercuryBOT.Helpers;
using Newtonsoft.Json;
using SteamKit2;
using SteamKit2.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using MercuryBOT.CustomHandlers;
using MercuryBOT.CallbackMessages;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SteamKit2.Authentication;
using MercuryBOT;
using Mercury.MetroMessageBox;
using Mercury.Helpers;
using System.Net;


/*
 * 
 *                  todo
 * 
 *          se o auto msg esta ligado, e o chatlogger tambem, o programa n esta a guardar a msg do auto msg
 * 
 * 
 * 
 * 
 * 
 * 
 */



namespace Mercury
{
    public class HandleLogin
    {
        // Instance-level properties
        public string WebApiNonce { get; private set; }
        public string getGuardData;
        public CookieContainer CookieContainer { get; private set; }
        private AuthSession _authSession;
        private TaskCompletionSource<bool> _connectionCompletionSource;
        private SessionData sessiondata = new SessionData();

        // Static User Information
        public static string UserPersonaName, UserCountry, CurrentUsername;
        public static ulong CurrentSteamID = 0;
        public static string avatar, LastErrorLogin;

        // Static User Authentication and Session Information
        public static string authCode, twoFactorAuth;
        public static string user, pass;
        public static string webAPIUserNonce, myWebAPIUser, myUserNonce, myUniqueId, APIKey;
        public static bool IsLoggedIn { get; private set; }
        public static bool IsWebLoggedIn { get; private set; }
        public static string LoginStatus = "Not connected...";
        public static bool cookiesAreInvalid = true;

        // Static Avatar Information
        public static string AvatarPrefix = "http://cdn.akamai.steamstatic.com/steamcommunity/public/images/avatars/";
        public static string AvatarSuffix = "_full.jpg";
        public static string AvatarHash, MessageString, ChangeState;

        // Static User State Information
        public static int CurrentPersonaState = 1;
        public static bool UserPlaying = false;
        public static bool isAwayState = false;
        public static bool AwayMsg = false;
        public static bool AwayCustomMessageList = false;
        public static bool isSendingMsgs = false;
        public static bool FriendsLoaded = false;

        // Static Login and Session State
        public static bool isRunning = false;
        public static EResult LastLogOnResult;
        public static string LastMessageReceived, LastMessageSent;
        public static string LastLogOnResultString => LastLogOnResult.ToString(); // Optional, if you need the result as a string

        // Static User Interaction Information
        public static bool ChatLogger = false;
        public static ulong CurrentAdmin;
        public static string steamID;

        // Static Friends and Privacy Information
        public static List<SteamID> Friends { get; private set; }
        public static IDictionary<string, int> PrivacySettings = new Dictionary<string, int>();
        public static Dictionary<ulong, string> ClanDictionary = new Dictionary<ulong, string>();
        public static Dictionary<ulong, ulong> OfficerClanDictionary = new Dictionary<ulong, ulong>();

        // Static Steam Client Information
        public static SteamClient steamClient;
        private static CallbackManager MercuryManager;
        public static SteamFriends steamFriends { get; set; }
        private SteamUser steamUser;
        public static SteamMatchmaking steamMM;
        public static SteamUnifiedMessages steamUnified;
        static SteamUnifiedMessages.UnifiedService<IPlayer> playerService;
        static JobID playerRequest = JobID.Invalid;
        public static GamesHandler gamesHandler;


        public HandleLogin()
        {
            isRunning = true;

            steamClient = new SteamClient();
            //steamWeb = new SteamWeb();
            gamesHandler = new GamesHandler();

            MercuryManager = new CallbackManager(steamClient);

            DebugLog.AddListener(new MyListener());
            DebugLog.Enabled = true;

            #region Callbacks

            steamUnified = steamClient.GetHandler<SteamUnifiedMessages>();

            MercuryManager.Subscribe<SteamClient.ConnectedCallback>(OnConnected);
            MercuryManager.Subscribe<SteamClient.DisconnectedCallback>(OnDisconnected);

            steamUser = steamClient.GetHandler<SteamKit2.SteamUser>();
            MercuryManager.Subscribe<SteamUser.LoggedOnCallback>(OnLoggedOn);
            MercuryManager.Subscribe<SteamUser.LoggedOffCallback>(OnLoggedOff);
            MercuryManager.Subscribe<SteamUser.AccountInfoCallback>(OnAccountInfo);

            // MercuryManager.Subscribe<SteamKit2.SteamUser.WebAPIUserNonceCallback>(WebAPIUser);
            steamFriends = steamClient.GetHandler<SteamFriends>();
            MercuryManager.Subscribe<SteamFriends.FriendsListCallback>(OnFriendsList);
            MercuryManager.Subscribe<SteamFriends.FriendMsgCallback>(OnFriendMsg);
            MercuryManager.Subscribe<SteamFriends.FriendMsgEchoCallback>(OnFriendEchoMsg);
            MercuryManager.Subscribe<SteamFriends.PersonaStateCallback>(OnPersonaState);
            MercuryManager.Subscribe<SteamFriends.PersonaChangeCallback>(OnSteamNameChange);

            // we also want to create our local service interface, which will help us build requests to the unified api
            playerService = steamUnified.CreateService<IPlayer>();

            #endregion

            steamClient.AddHandler(gamesHandler);
            MercuryManager.Subscribe<PurchaseResponseCallback>(OnPurchaseResponse);


            LoginStatus = "Starting...";

            // manager.Subscribe<SteamFriends.FriendMsgHistoryCallback>(OnFriendMsgHistory);
            // manager.Subscribe<SteamFriends.ChatMsgCallback>(OnChatMsg);

            // Start running the callback manager in a background task
            Task.Run(() => RunCallbackManager());
        }

        public async Task StartLogin(string user, string pw)
        {
            //Task.Run(() => RunCallbackManager());


            string username = user;
            string password = pw;
            CurrentUsername = username;

            try
            {
                if (_authSession == null)
                {
                    Console.WriteLine($"Attempting initial login for user: {username}");

                    int retryCount = 3;

                    while (retryCount > 0)
                    {
                        _connectionCompletionSource = new TaskCompletionSource<bool>();

                        // Connect to Steam
                        steamClient.Connect();

                        // Await connection completion asynchronously
                        if (await WaitForSteamConnectionAsync(TimeSpan.FromSeconds(10)))
                        {
                            Console.WriteLine("Connected to Steam successfully.");
                            break; // Exit retry loop on success
                        }

                        Console.WriteLine("Failed to connect to Steam: Connection timed out. Retrying...");


                        //Main.ActiveForm.BackColor = System.Drawing.Color.Yellow;
                        //MetroMessageBox.Show(Main.ActiveForm, "Failed to connect to Steam: Connection timed out. Retrying... (Maybe Steam Maintenance..)", "Error", MessageBoxButtons.OK, MessageBoxIcon.None, 250);

                        CustomMetroMessageBox.Show(Main.ActiveForm, "Failed to connect to Steam: Connection timed out. Retrying... (Maybe Steam Maintenance..)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                        retryCount--;
                    }

                    if (retryCount == 0)
                    {
                        Console.WriteLine("Failed to connect to Steam after multiple attempts.");
                        return;
                    }


                    bool tkn_datacheck = File.Exists(Program.SentryFolder + user + "_tkn.data");

                    if (File.Exists(Program.SentryFolder + user + "_tkn.data"))
                    {
                        // test this
                        var listUserSettings = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));

                        var account = listUserSettings.Accounts.Find(a => a.username == user);
                        if (account != null)
                        {
                            account.password = "";
                        }

                        File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(listUserSettings, Formatting.Indented));



                        //var ListUserSettings = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                        //foreach (var a in ListUserSettings.Accounts)
                        //{
                        //    if (a.username == user)
                        //    {
                        //        a.password = "";
                        //    }
                        //}
                        //File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(ListUserSettings, Formatting.Indented));

                        steamUser.LogOn(new SteamUser.LogOnDetails
                        {
                            Username = username,
                            AccessToken = File.ReadAllText(Program.SentryFolder + user + "_tkn.data"),

                            // condition ? consequent : alternative
                            ShouldRememberPassword = true,
                            LoginID = (uint)(new Random().Next(10000, 10000000)),
                        });
                    }
                    else
                    {
                        // Begin the authentication session after a successful connection
                        _authSession = await steamClient.Authentication.BeginAuthSessionViaCredentialsAsync(new AuthSessionDetails
                        {
                            Username = username,
                            Password = password,
                            Authenticator = new DeviceAuth(),
                            IsPersistentSession = true,
                            //GuardData = getGuardData
                        });

                        // Start polling for the authentication result
                        var pollResponse = await _authSession.PollingWaitForResultAsync();

                        if (pollResponse.NewGuardData != null)
                        {
                            // When using certain two factor methods (such as email 2fa), guard data may be provided by Steam
                            // for use in future authentication sessions to avoid triggering 2FA again (this works similarly to the old sentry file system).
                            // Do note that this guard data is also a JWT token and has an expiration date.
                            getGuardData = pollResponse.NewGuardData;
                        }

                        // Log on to Steam with the access token we received
                        steamUser.LogOn(new SteamUser.LogOnDetails
                        {
                            Username = pollResponse.AccountName,
                            AccessToken = pollResponse.RefreshToken,//a ? read : 

                            // condition ? consequent : alternative
                            ShouldRememberPassword = true,
                            LoginID = (uint)(new Random().Next(10000, 10000000)),
                        });

                        if (!tkn_datacheck)
                        {
                            File.WriteAllText(Program.SentryFolder + user + "_tkn.data", pollResponse.RefreshToken);
                        }

                        Console.WriteLine("Login successful!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login failed: {ex.Message}");
            }
        }


        //private void UserWebLogOn()
        //{
        //    do
        //    {
        //        //myUniqueId, steamClient, myUserNonce
        //        IsWebLoggedIn = steamWeb.Authenticate(myUniqueId, steamClient, myUserNonce);

        //        if (!IsWebLoggedIn)
        //        {
        //            Console.WriteLine("[" + Program.TOOLNAME + "] - Authentication failed, retrying in 10s...");
        //            Thread.Sleep(10000);
        //        }
        //    } while (!IsWebLoggedIn); //test

        //    Console.WriteLine("[" + Program.TOOLNAME + "] - User Authenticated to Web!");

        //    cookiesAreInvalid = false;
        //}


        private void OnConnected(SteamClient.ConnectedCallback callback)
        {
            Console.WriteLine("Connected to Steam!");
            _connectionCompletionSource?.SetResult(true);
        }

        private void OnDisconnected(SteamClient.DisconnectedCallback callback)
        {
            Console.WriteLine($"Disconnected from Steam. UserInitiated: {callback.UserInitiated}");

            if (!callback.UserInitiated)
            {
                // Retry connection if disconnected unexpectedly
                _connectionCompletionSource?.SetResult(false);
            }
        }

        //static void OnDisconnected(SteamClient.DisconnectedCallback callback)
        //{
        //    if (callback.UserInitiated)
        //    {
        //        return;
        //    }

        //    EResult lastLogOnResult = LastLogOnResult;
        //    LastLogOnResult = EResult.Invalid;
        //    CurrentPersonaState = 0;
        //    switch (lastLogOnResult)
        //    {
        //        case EResult.AccountDisabled:
        //            // Do not attempt to reconnect, those failures are permanent
        //            return;
        //        case EResult.InvalidPassword:
        //        case EResult.NoConnection:
        //        case EResult.ServiceUnavailable:
        //        case EResult.Timeout:
        //        case EResult.TryAnotherCM:
        //        case EResult.TwoFactorCodeMismatch:
        //            //retry
        //            TimeSpan.FromSeconds(5);
        //            steamClient.Connect();
        //            Console.WriteLine("Disconnected from steam, reconnecting in 5 sec... [" + lastLogOnResult + "]");
        //            break;
        //        case EResult.AccountLoginDeniedNeedTwoFactor:
        //            steamClient.Connect();
        //            break;

        //        case EResult.RateLimitExceeded:
        //            //retry
        //            Console.WriteLine("Disconnected from steam, please try again in 30min [" + lastLogOnResult + "]");

        //            break;
        //            // default: //test
        //            //   TimeSpan.FromSeconds(5);
        //            //   steamClient.Connect();
        //            //   Console.WriteLine("Disconnected from steam, reconnecting in 5 sec... [" + lastLogOnResult + "]");
        //            //    break;

        //    }
        //}


        public void loadcookies()
        {
            if (SessionData.IsRefreshTokenExpired())
            {
                MessageBox.Show("Your session has expired. Use the login again button under the selected account menu.", "Trade Confirmations", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check for a valid access token, refresh it if needed
            if (SessionData.IsAccessTokenExpired())
            {
                try
                {
                    SessionData.RefreshAccessToken();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Steam Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void OnLoggedOn(SteamUser.LoggedOnCallback callback)
        {
            if (callback.Result == EResult.OK)
            {
                if (File.Exists(Program.SentryFolder + CurrentUsername + "_tkn.data"))
                {
                    SessionData.SteamID = callback.ClientSteamID;
                    SessionData.AccessToken = File.ReadAllText(Program.SentryFolder + CurrentUsername + "_tkn.data");
                    //loadcookies();
                }


                LastLogOnResult = callback.Result;

                Console.WriteLine("[] - Successfully logged on!");
                LastErrorLogin = "ok";
                LoginStatus = "Successfully logged on!";


                CurrentSteamID = steamClient.SteamID.ConvertToUInt64();
                // UserWebLogOn();



                UserCountry = callback.IPCountryCode;

                UserClanIDS();
                gather_ClanOfficers();

                //ChatMode();

                IsLoggedIn = true;
                LastLogOnResult = EResult.OK;

                // myUserNonce = callback.WebAPIUserNonce;
               // steamFriends.SetPersonaState(CurrentPersonaState);

                var ListUserSettings = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                foreach (var a in ListUserSettings.Accounts)
                {
                    if (a.username == CurrentUsername)
                    {
                        if (a.ChatLogger == true)
                        {
                            ChatLogger = true;
                        }
                        steamFriends.SetPersonaState(Extensions.statesList[a.LoginState]);
                        CurrentPersonaState = a.LoginState;

                        a.LastLoginTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
                    }
                }
                File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(ListUserSettings, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("1Unable to logon to Steam: {0} / {1}", callback.Result, callback.ExtendedResult);

                LastErrorLogin = "Unable to logon to Steam: " + callback.Result + " / " + callback.ExtendedResult;

                MercuryBOT.InfoForm.InfoHelper.CustomMessageBox.Show("Error", callback.Result + " / " + callback.ExtendedResult);

                switch (callback.Result)
                {
                    case EResult.Expired:
                    case EResult.AccessDenied:
                        MercuryBOT.InfoForm.InfoHelper.CustomMessageBox.Show("Error", "Login Session Expired, please login with password again.");

                        if (File.Exists(Program.SentryFolder + CurrentUsername + "_tkn.data"))
                        {
                            File.Delete(Program.SentryFolder + CurrentUsername + "_tkn.data");
                        }
                        break;
                }
                _connectionCompletionSource?.SetResult(false);
            }
        }

        private void OnLoggedOff(SteamUser.LoggedOffCallback callback)
        {
            Console.WriteLine($"Logged off from Steam: {callback.Result}");
            _connectionCompletionSource?.SetResult(false);
        }

        // Utility method to wait for the connection
        private async Task<bool> WaitForSteamConnectionAsync(TimeSpan timeout)
        {
            var completedTask = await Task.WhenAny(_connectionCompletionSource.Task, Task.Delay(timeout));
            return completedTask == _connectionCompletionSource.Task && _connectionCompletionSource.Task.Result;
        }

        // Utility method to run the callback manager
        private void RunCallbackManager()
        {
            while (true)
            {
                MercuryManager.RunWaitCallbacks(TimeSpan.FromMilliseconds(100));
            }
        }

        private void OnAccountInfo(SteamUser.AccountInfoCallback callback)
        {
            UserPersonaName = callback.PersonaName;
            UserCountry = callback.Country;
        }


        public static void GetPrivacySettings()
        {
            CPlayer_GetPrivacySettings_Request req = new CPlayer_GetPrivacySettings_Request { };
            playerRequest = playerService.SendMessage(x => x.GetPrivacySettings(req));
        }

        private void OnPersonaState(SteamFriends.PersonaStateCallback callback)
        {
            SteamID friendID = callback.FriendID;
            SteamID sourceSteamID = callback.SourceSteamID.ConvertToUInt64();
            EClanRank clanRank = (EClanRank)callback.ClanRank;
            EPersonaState state = callback.State;


            //Console.WriteLine("Online Sessions:" + callback.OnlineSessionInstances);

            //ListFriends.UpdateName(friendId.ConvertToUInt64(), callback.Name); not yet
            //ListFriends.UpdateStatus(friendId.ConvertToUInt64(), state.ToString()); not yet

            if (friendID.ConvertToUInt64() == steamClient.SteamID)
            {
                if (sourceSteamID.IsClanAccount)
                {
                    switch (clanRank)
                    {
                        case EClanRank.Owner:
                            // case EClanRank.Officer:
                            //case EClanRank.Moderator:
                            if (!OfficerClanDictionary.ContainsKey(sourceSteamID))
                            {
                                OfficerClanDictionary.Add(sourceSteamID, friendID.ConvertToUInt64());
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
                if (state != EPersonaState.Online) //detect when user goes afk
                {
                    //isAwayState = true;
                }
                else
                {
                    //isAwayState = false;
                }

                string avatarHash = null;

                if ((callback.AvatarHash.Length > 0) && callback.AvatarHash.Any(singleByte => singleByte != 0))
                {
                    avatarHash = BitConverter.ToString(callback.AvatarHash).Replace("-", "").ToLowerInvariant();

                    if (string.IsNullOrEmpty(avatarHash) || avatarHash.All(singleChar => singleChar == '0'))
                    {
                        avatarHash = null;
                    }
                }

                AvatarHash = avatarHash;
            }
        }


        public void LoadFriends() //https://github.com/waylaidwanderer/Mist/tree/master/SteamBot
        {
            ListFriends.Clear();
            List<SteamID> steamIdList = new List<SteamID>();
            Console.WriteLine("[" + Program.TOOLNAME + "] - Loading all friends...");
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
                    Console.WriteLine("[" + Program.TOOLNAME + "] - Found friend {0} in list of friend requests, so let's remove the user.", (object)listFriends.Name);
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
            Console.WriteLine("[" + Program.TOOLNAME + "] - Done! {0} friends.", (object)ListFriends.Get(false).Count);
            FriendsLoaded = true;
        }

        public string GetAvatarLink(ulong steamid)
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
        private void OnFriendsList(SteamFriends.FriendsListCallback obj)
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
                            //steamFriends.RemoveFriend(friend.SteamID);
                        }
                        else if (friend.Relationship == EFriendRelationship.RequestRecipient)
                        {
                            if (!Friends.Contains(friend.SteamID))
                            {
                                // Friends.Add(friend.SteamID);
                                newFriends.Add(friend.SteamID);
                            }
                        }
                        else if (friend.Relationship == EFriendRelationship.RequestInitiator)
                        {
                            if (!Friends.Contains(friend.SteamID))
                            {
                                // Friends.Add(friend.SteamID);
                                newFriends.Add(friend.SteamID);
                            }
                        }
                        break;
                }
            }
            Console.WriteLine("[" + Program.TOOLNAME + "] Recorded steam friends : {0}", steamFriends.GetFriendCount());
        }

        public void CreateFriendsListIfNecessary()
        {
            if (Friends != null)
                return;

            Friends = new List<SteamID>();
            for (int i = 0; i < steamFriends.GetFriendCount(); i++)
            {

                SteamID allfriends = steamFriends.GetFriendByIndex(i);
                var id = allfriends.ConvertToUInt64().ToString();
                if (id.StartsWith("7") && allfriends.ConvertToUInt64() != 0 && steamFriends.GetFriendRelationship(allfriends.ConvertToUInt64()) == EFriendRelationship.Friend)
                {
                    Friends.Add(allfriends.ConvertToUInt64());
                }
            }
        }


        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

        private void OnFriendMsg(SteamFriends.FriendMsgCallback callback) // Auto MSG
        {
            if (ChatLogger && callback.EntryType == EChatEntryType.ChatMsg)
            {
                ulong FriendID = callback.Sender;
                string Message = callback.Message;

                string FriendName = steamFriends.GetFriendPersonaName(FriendID);
                string nameClean = Regex.Replace(FriendName, "[^A-Za-z0-9 _]", "");

                // Ensure the chat logs directory exists
                string userSteamID = steamClient.SteamID.ConvertToUInt64().ToString();
                string chatLogDirectory = Path.Combine(Program.ChatLogsFolder, userSteamID);
                if (!Directory.Exists(chatLogDirectory))
                {
                    Directory.CreateDirectory(chatLogDirectory);
                }

                // Log file path
                string FriendIDName = $"[{FriendID}] - {nameClean}.txt";
                string pathLog = Path.Combine(chatLogDirectory, FriendIDName);

                // Final message format
                string FinalMsg = $"[{DateTime.Now}] {FriendName}: {Message}";
                string Separator = "───────────────────";


                // Check if log file exists for the friend
                string[] files = Directory.GetFiles(chatLogDirectory, $"[{FriendID}]*.txt");

                if (files.Length > 0)//file exist
                {
                    string[] LastDate = File.ReadLines(files[0]).Last().Split(' '); 
                    LastDate[0] = LastDate[0].Substring(1);


                    using (var tw = new StreamWriter(files[0], true))
                    {


                        if (LastDate[0] != DateTime.Now.Date.ToShortDateString())
                        {
                            tw.WriteLine($"{Separator}\n{FinalMsg}");
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
                    File.WriteAllText(pathLog, $"{FinalMsg}\n");
                }
            }


            if (callback.EntryType == EChatEntryType.ChatMsg)
            {
                // Deserialize once to reuse data
                var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
                var currentAccount = list.Accounts.FirstOrDefault(a => a.username == CurrentUsername);
                if (currentAccount != null)
                {
                    CurrentAdmin = currentAccount.AdminID;
                }

                if (callback.Sender == CurrentAdmin)
                {
                    string command = callback.Message.Split(' ')[0];
                    //string message = callback.Message.Replace(command, "");
                    string message = callback.Message.Substring(command.Length).Trim();

                    switch (command)
                    {
                        case ".pcsleep":
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Sleeping..." + "\r\n\r\n" + Program.TOOLNAME);
                            Thread.Sleep(500);
                            SetSuspendState(false, true, true); //sleep
                            break;
                        case ".pchiber":
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Hibernating..." + "\r\n\r\n" + Program.TOOLNAME);
                            Thread.Sleep(500);
                            SetSuspendState(true, true, true); //hibernate
                            break;
                        case ".pcrr":
                            var reboot = new ProcessStartInfo("shutdown", "/r /t 0") { CreateNoWindow = true, UseShellExecute = false };
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Restarting..." + "\r\n\r\n" + Program.TOOLNAME);
                            Thread.Sleep(500);
                            Process.Start(reboot);
                            break;
                        case ".pcoff":
                            var shutdown = new ProcessStartInfo("shutdown", "/s /t 0") { CreateNoWindow = true, UseShellExecute = false };
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Shutdown..." + "\r\n\r\n" + Program.TOOLNAME);
                            Thread.Sleep(500);
                            Process.Start(shutdown);
                            break;
                        case ".close":
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Closing... :c" + "\r\n\r\n" + Program.TOOLNAME);
                            Thread.Sleep(500);
                            Environment.Exit(1);
                            break;
                        case ".logoff":
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Logged off... :c" + "\r\n\r\n" + Program.TOOLNAME);
                            Thread.Sleep(500);
                            Logout();
                            break;
                        case ".play":
                            //  string clearGames = callback.Message.Replace(".play", "");
                            string clearGames = callback.Message.Substring(".play ".Length).Trim(); // Remove ".play" from the start

                            List<uint> gameuints = new List<uint>();

                            var accounts = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile)).Accounts;

                            // Find the current account
                            var account = accounts.FirstOrDefault(a => a.username == CurrentUsername);

                            if (account != null)
                            {
                                if (account.Games.Count == 0)
                                {
                                    steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg,
                                        "Error: no appids in accounts.json. Use Gather AppIDS\r\n\r\n" + Program.TOOLNAME);
                                }
                                else if (account.Games.Count >= 32)
                                {
                                    steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg,
                                        "Error: Max games allowed 32.\r\n\r\n" + Program.TOOLNAME);
                                    return;
                                }
                                else
                                {
                                    // Add game app_ids to the list
                                    gameuints.AddRange(account.Games.Select(game => game.app_id));

                                    // If a custom name for the game is provided
                                    if (!string.IsNullOrEmpty(clearGames))
                                    {
                                        PlayGames(gameuints, clearGames + " | Mercury");
                                        steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg,
                                            "Playing...\r\n\r\n" + Program.TOOLNAME);
                                    }
                                    else
                                    {
                                        steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg,
                                            "Error: Please enter a custom name after '.play custom name game'\r\n\r\n" + Program.TOOLNAME);
                                    }
                                }
                            }
                            break;
                        case ".non":
                            string clearNoN = callback.Message.Replace(".non ", "");
                            if (clearNoN.Length < 50)
                            {
                                Utils.PlayNonSteamGame(clearNoN + " | Mercury");
                                steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Playing: " + clearNoN + "\r\n\r\n" + Program.TOOLNAME);
                            }
                            else
                            {
                                steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Error: Only 50 chars allowed." + "\r\n\r\n" + Program.TOOLNAME);
                            }
                            break;
                        case ".stopgames":
                            Utils.StopGames();
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Stopping games." + "\r\n\r\n" + Program.TOOLNAME);
                            break;
                        case ".steamrep ":
                            // not yet
                            break;
                        case ".trolha":
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "https://steamcommunity.com/profiles/76561198041931474" + "\r\n\r\n" + Program.TOOLNAME);
                            break;
                    }
                }

                if (AwayMsg || isAwayState)
                {
                    steamFriends.SendChatMessage(callback.Sender, EChatEntryType.ChatMsg, MessageString + "\r\n\r\n" + Program.TOOLNAME);
                }
                // Handle Away Custom Message List
                if (AwayCustomMessageList)
                {
                    List<string> CMessages = currentAccount?.AFKMessages.Select(m => m.Message).ToList() ?? new List<string>();

                    CMessages.Add("I'm using Mercury: The Ultimate free open source Steam Tool! - https://github.com/sp0ok3r/Mercury");


                    if (CMessages.Any())
                    {
                        Random random = new Random();
                        int r = random.Next(0, CMessages.Count);
                        steamFriends.SendChatMessage(callback.Sender, EChatEntryType.ChatMsg, CMessages[r] + "\r\n\r\n" + Program.TOOLNAME);
                    }
                }
            }
        }
        private void OnFriendEchoMsg(SteamFriends.FriendMsgEchoCallback callback)
        {
            if (ChatLogger && callback.EntryType == EChatEntryType.ChatMsg)
            {
                ulong friendID = callback.Recipient;
                string message = callback.Message;

                string friendName = steamFriends.GetFriendPersonaName(friendID);
                string cleanName = Regex.Replace(friendName, "[^A-Za-z0-9 _]", "");

                // Calculate the log file path and ensure the directory exists
                string steamIDString = steamClient.SteamID.ConvertToUInt64().ToString();
                string directoryPath = Path.Combine(Program.ChatLogsFolder, steamIDString);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                // Prepare final message and log file path
                string finalMessage = $"[{DateTime.Now}] {steamFriends.GetPersonaName()}: {message}";
                string logFilePath = Path.Combine(directoryPath, $"[{friendID}] - {cleanName}.txt");

                // Check if file exists and write the message accordingly
                string[] existingFiles = Directory.GetFiles(directoryPath, $"[{friendID}]*.txt");
                
                const string Separator = "───────────────────";

                if (existingFiles.Length > 0) // File exists
                {
                    string[] lastDate = File.ReadLines(existingFiles[0]).Last().Split(' ');
                    lastDate[0] = lastDate[0].Substring(1); // Remove the opening bracket

                    using (var tw = new StreamWriter(existingFiles[0], true))
                    {
                        if (lastDate[0] != DateTime.Now.Date.ToShortDateString())
                        {
                            tw.WriteLine($"{Separator}\n{finalMessage}");
                        }
                        else
                        {
                            tw.WriteLine(finalMessage);
                        }
                    }
                }
                else
                {
                    FileInfo file = new FileInfo(logFilePath);
                    file.Directory.Create();
                    File.WriteAllText(logFilePath, finalMessage + "\n");
                }
            }
        }

        public void SendMsg2AllFriends(string message, bool Only2Receipts)
        {
            isSendingMsgs = true;
            int princessas = 0;

            // Read the accounts file only once
            var accounts = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile)).Accounts;

            for (int i = 0; i <= steamFriends.GetFriendCount(); i++)
            {

                SteamID allfriends = steamFriends.GetFriendByIndex(i);
                ulong friendID = allfriends.ConvertToUInt64();

                if (friendID != 0)
                {
                    if (Only2Receipts)
                    {
                        var account = accounts.FirstOrDefault(a => a.username == CurrentUsername); // Find current account once

                        if (account != null && account.MsgRecipients.Any(s => s.Contains(friendID.ToString())))
                        {
                            princessas++;
                            steamFriends.SendChatMessage(friendID, EChatEntryType.ChatMsg, message + "\r\n\r\n" + Program.TOOLNAME);
                            Thread.Sleep(500); // Keep sleep if necessary
                        }
                    }
                    else
                    {
                        princessas++;
                        steamFriends.SendChatMessage(friendID, EChatEntryType.ChatMsg, message + "\r\n\r\n" + Program.TOOLNAME);
                        Thread.Sleep(500); // Keep sleep if necessary
                    }
                }
            }
            isSendingMsgs = false;
            MercuryBOT.InfoForm.InfoHelper.CustomMessageBox.Show("Info", "Sent message to: " + princessas + " Friends.");
        }

        public void RemoveFriend(ulong goodbye)
        {
            steamFriends.SendChatMessage(goodbye, EChatEntryType.ChatMsg, "Have an amazing day!" + "\r\n\r\n" + Program.TOOLNAME);
            Thread.Sleep(500);
            steamFriends.RemoveFriend(goodbye);
        }

        #endregion

        static void OnSteamNameChange(SteamFriends.PersonaChangeCallback callback)
        {
            UserPersonaName = callback.Name;
            Console.WriteLine("[" + Program.TOOLNAME + "] - Name changed to: " + callback.Name);
        }
        public string GetPersonaName(ulong steamid)
        {
            return steamFriends.GetFriendPersonaName((SteamID)steamid);
        }

        #region Change State Name Persona Flag
        public void ChangeCurrentState(EPersonaState state)
        {
            steamFriends.SetPersonaState(state);
            Console.WriteLine("[" + Program.TOOLNAME + "] - State Changed to: " + state);
        }

        public static void ChangeCurrentName(string Name)
        {
            steamFriends.SetPersonaName(Name);
            Console.WriteLine("[" + Program.TOOLNAME + "] - Name Changed to: " + Name);
        }

        public void ChangePersonaFlags(uint uimode)
        {
            ClientMsgProtobuf<CMsgClientChangeStatus> requestPersonaFlag = new ClientMsgProtobuf<CMsgClientChangeStatus>(EMsg.ClientChangeStatus)
            {
                Body = { persona_state_flags = uimode }
            };
            steamClient.Send(requestPersonaFlag);
        }
        #endregion

        public void ChatMode()
        {
            //https://github.com/SteamRE/SteamKit/issues/561
            ClientMsgProtobuf<CMsgClientUIMode> request = new ClientMsgProtobuf<CMsgClientUIMode>(EMsg.ClientCurrentUIMode) { Body = { chat_mode = 2 } };
            steamClient.Send(request);
        }

        public async void RedeemKey(string _key)
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
                if ((!ClanDictionary.ContainsKey(steamIDClan)) && steamFriends.GetClanRelationship(steamIDClan) == EClanRelationship.Member)
                {
                    ClanDictionary.Add(steamIDClan, steamFriends.GetClanName(steamIDClan));
                }
            }
        }

        public void gather_ClanOfficers()
        {
            foreach (var pair in ClanDictionary)
            {
                ClanOfficers(Convert.ToUInt64(pair.Key));
            }
        }

        public void UIMode(uint x)
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
        public void ClanOfficers(ulong clanID)
        {
            if (clanID == 0)
            {
                return;
            }
            var request = new ClientMsgProtobuf<CMsgClientAMGetClanOfficers>(EMsg.ClientAMGetClanOfficers);
            request.Body.steamid_clan = clanID;


            steamClient.Send(request);
        }
        public void Logout()
        {
            //webAPIUserNonce = "";
            user = null;
            isRunning = false;
            IsLoggedIn = false;
            steamUser.LogOff();
            Friends.Clear();
            CurrentPersonaState = 0;
            UserPersonaName = "";
            CurrentUsername = "";

            LastLogOnResult = EResult.NotLoggedOn;
        }

        public static void NotifBox(string title, string mess)
        {
            //Notification.NotifHelper.MessageBox.Show(title, mess);
        }

        public void PlayGames(List<uint> gameIDs, string NonSteam)
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

        private static string UnEscape(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return null;
            }
            return message.Replace("\\[", "[").Replace("\\\\", "\\");
        }

        class MyListener : IDebugListener
        {
            public void WriteLine(string category, string msg)
            {
                // this function will be called when internal steamkit components write to the debuglog

                // for this example, we'll print the output to the console
                Console.WriteLine("MyListener - {0}: {1}", category, msg);
            }
        }
    }
}