/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/
using MercuryBOT.FriendsList;
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

namespace MercuryBOT
{
    public class AccountLoginOFF
    {
        public static string UserPersonaName, UserCountry, CurrentUsername;
        public static bool UserPlaying = false;

        public static ulong CurrentAdmin;
        public static int CurrentPersonaState = 1;
        public static ulong CurrentSteamID = 0;
        public static string webAPIUserNonce;

        public static IDictionary<string, int> PrivacySettings = new Dictionary<string, int>();

        public static string myWebAPIUser, myUserNonce, myUniqueId, APIKey;

        public static List<SteamID> Friends { get; private set; }
        public static Dictionary<ulong, string> ClanDictionary = new Dictionary<ulong, string>();
        public static Dictionary<ulong, ulong> OfficerClanDictionary = new Dictionary<ulong, ulong>();


        public static SteamClient steamClient;
        private static SteamUser steamUser;
        public static SteamFriends steamFriends;
        private static CallbackManager MercuryManager;
        public static SteamWeb steamWeb;
        public static GamesHandler gamesHandler;

        public static SteamMatchmaking steamMM;
        public static SteamUnifiedMessages steamUnified;
        static SteamUnifiedMessages.UnifiedService<IPlayer> playerService;
        static JobID playerRequest = JobID.Invalid;


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
        public static EResult LastLogOnResult;
        public static string LoginStatus = "Not connected...";

       // private static string NewloginKey = null;
        private static bool cookiesAreInvalid = true;
        public static string user, pass, authCode, twoFactorAuth,steamID,AvatarHash, MessageString;

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
            steamUnified = steamClient.GetHandler<SteamUnifiedMessages>();

            MercuryManager.Subscribe<SteamClient.ConnectedCallback>( OnConnected );
            MercuryManager.Subscribe<SteamClient.DisconnectedCallback>(OnDisconnected);

            MercuryManager.Subscribe<SteamUser.LoggedOnCallback>(OnLoggedOn);
            MercuryManager.Subscribe<SteamUser.LoggedOffCallback>(OnLoggedOff);
            MercuryManager.Subscribe<SteamUser.AccountInfoCallback>(OnAccountInfo);
            MercuryManager.Subscribe<SteamUser.UpdateMachineAuthCallback>(OnMachineAuth);
            MercuryManager.Subscribe<SteamUser.WebAPIUserNonceCallback>(WebAPIUser);

            MercuryManager.Subscribe<SteamFriends.FriendsListCallback>(OnFriendsList);
            MercuryManager.Subscribe<SteamFriends.FriendMsgCallback>(OnFriendMsg);
            MercuryManager.Subscribe<SteamFriends.FriendMsgEchoCallback>(OnFriendEchoMsg);

            MercuryManager.Subscribe<SteamFriends.PersonaStateCallback>(OnPersonaState);
            MercuryManager.Subscribe<SteamFriends.PersonaChangeCallback>(OnSteamNameChange);


            MercuryManager.Subscribe<SteamUnifiedMessages.ServiceMethodNotification>(OnServiceMethod);
            MercuryManager.Subscribe<SteamUnifiedMessages.ServiceMethodResponse>(OnMethodResponse);

            // we also want to create our local service interface, which will help us build requests to the unified api
            playerService = steamUnified.CreateService<IPlayer>();

            //MercuryManager.Subscribe<SteamMatchmaking.GetLobbyListCallback>(OnLobbyList);
            //MercuryManager.Subscribe<SteamMatchmaking.CreateLobbyCallback>(OnCreateLobby);
            //MercuryManager.Subscribe<SteamMatchmaking.JoinLobbyCallback>(OnJoinLobby);
            //MercuryManager.Subscribe<SteamMatchmaking.LeaveLobbyCallback>(OnLeaveLobby);
            //MercuryManager.Subscribe<SteamMatchmaking.Lobby>(OnLobby);
            //MercuryManager.Subscribe<SteamMatchmaking.LobbyDataCallback>(OnLobbyData);
            //MercuryManager.Subscribe<SteamMatchmaking.UserJoinedLobbyCallback>(OnUserJoinedLobby);

            #endregion

            steamClient.AddHandler(gamesHandler);
            MercuryManager.Subscribe<PurchaseResponseCallback>(OnPurchaseResponse);

            Console.WriteLine("[" + Program.BOTNAME + "] - Connecting to Steam...");
            LoginStatus = "Connecting to Steam...";
            steamClient.Connect();

            while (isRunning)
            {
                MercuryManager.RunWaitCallbacks(TimeSpan.FromMilliseconds(500));
            }
        }

        static void OnServiceMethod(SteamUnifiedMessages.ServiceMethodNotification notification)
        {
            if (notification == null)
            {
                //nameof(notification);

                return;
            }

            switch (notification.MethodName)
            {
                case "ChatRoomClient.NotifyIncomingChatMessage#1":
                    OnIncomingChatMessage((CChatRoom_IncomingChatMessage_Notification)notification.Body);

                    break;
                case "FriendMessagesClient.IncomingMessage#1":
                    OnIncomingMessage((CFriendMessages_IncomingMessage_Notification)notification.Body);

                    break;
            }
        }

        static void OnMethodResponse(SteamUnifiedMessages.ServiceMethodResponse callback)
        {
            if (callback.JobID != playerRequest)
            {
                return;
            }

            if (callback.Result != EResult.OK)
            {
                Console.WriteLine($"Unified service request failed with {callback.Result}");
                return;
            }

            // retrieve the deserialized response for the request we made
            // notice the naming pattern
            // for requests: CMyService_Method_Request
            // for responses: CMyService_Method_Response
            CPlayer_GetPrivacySettings_Response resp = callback.GetDeserializedResponse<CPlayer_GetPrivacySettings_Response>();


            var dictionary = new Dictionary<string, int>{
                        { "PrivacyProfile",         resp.privacy_settings.privacy_state},
                        { "PrivacyFriendsList",     resp.privacy_settings.privacy_state_friendslist},
                        { "PrivacyPlaytime",        resp.privacy_settings.privacy_state_playtime},
                        { "PrivacyOwnedGames",      resp.privacy_settings.privacy_state_ownedgames},
                        { "PrivacyInventoryGifts",  resp.privacy_settings.privacy_state_gifts},
                        { "PrivacyInventory",       resp.privacy_settings.privacy_state_inventory}
                    };
            PrivacySettings = dictionary;

            playerRequest = JobID.Invalid;


            // now that we've completed our task, lets log off
        }



        static void OnIncomingChatMessage(CChatRoom_IncomingChatMessage_Notification notification)
        {
            if (notification == null)
            {
                // ArchiLogger.LogNullError(nameof(notification));

                return;
            }

            // Under normal circumstances, timestamp must always be greater than 0, but Steam already proved that it's capable of going against the logic
            //   if ((notification.steamid_sender != SteamID) && (notification.timestamp > 0) && BotConfig.BotBehaviour.HasFlag(BotConfig.EBotBehaviour.MarkReceivedMessagesAsRead))
            //   {
            //       Utilities.InBackground(() => ArchiHandler.AckChatMessage(notification.chat_group_id, notification.chat_id, notification.timestamp));
            //   }

            string message;

            // Prefer to use message without bbcode, but only if it's available
            if (!string.IsNullOrEmpty(notification.message_no_bbcode))
            {
                message = notification.message_no_bbcode;
            }
            else if (!string.IsNullOrEmpty(notification.message))
            {
                message = UnEscape(notification.message);
            }
            else
            {
                return;
            }

            // ArchiLogger.LogChatMessage(false, message, notification.chat_group_id, notification.chat_id, notification.steamid_sender);
            //  if ((notification.chat_group_id != MasterChatGroupID) || (BotConfig.OnlineStatus == EPersonaState.Offline))
            //  {
            //     return;
            //   }

            // await HandleMessage(notification.chat_group_id, notification.chat_id, notification.steamid_sender, message).ConfigureAwait(false);            
            Console.WriteLine(notification.chat_group_id + notification.chat_id + notification.steamid_sender);

        }

        static void OnIncomingMessage(CFriendMessages_IncomingMessage_Notification notification)
        {

            if (notification == null)
            {
                //  ArchiLogger.LogNullError(nameof(notification));

                return;
            }

            if ((EChatEntryType)notification.chat_entry_type != EChatEntryType.ChatMsg)
            {
                return;
            }

            // Under normal circumstances, timestamp must always be greater than 0, but Steam already proved that it's capable of going against the logic
            //  if (!notification.local_echo && (notification.rtime32_server_timestamp > 0) && BotConfig.BotBehaviour.HasFlag(BotConfig.EBotBehaviour.MarkReceivedMessagesAsRead))
            // {
            //Utilities.InBackground(() => ArchiHandler.AckMessage(notification.steamid_friend, notification.rtime32_server_timestamp));
            //  }

            string message;

            // Prefer to use message without bbcode, but only if it's available
            if (!string.IsNullOrEmpty(notification.message_no_bbcode))
            {
                message = notification.message_no_bbcode;
            }
            else if (!string.IsNullOrEmpty(notification.message))
            {
                message = UnEscape(notification.message);
            }
            else
            {
                return;
            }

            // ArchiLogger.LogChatMessage(notification.local_echo, message, steamID: notification.steamid_friend);
            //  if (notification.local_echo || (BotConfig.OnlineStatus == EPersonaState.Offline))
            //  {
            //      return;
            // }
            //  await HandleMessage(notification.steamid_friend, message).ConfigureAwait(false);
            //  Console.WriteLine("message from: "+notification.steamid_friend + message);
        }

        private static string UnEscape(string message) //ArchiSteamFarm
        {
            if (string.IsNullOrEmpty(message))
            {
                // ASF.ArchiLogger.LogNullError(nameof(message));
                return null;
            }
            return message.Replace("\\[", "[").Replace("\\\\", "\\");
        }

        static void OnConnected(SteamClient.ConnectedCallback callback)
        {

            if (callback.ToString() != "SteamKit2.SteamClient+ConnectedCallback")
            {
                Console.WriteLine("[" + Program.BOTNAME + "] - Unable to connect to Steam: {0}", callback);
                LoginStatus = "Unable to connect to Steam:" + callback;
                isRunning = false;
                return;
            }
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            //Sucess
            Console.WriteLine("[" + Program.BOTNAME + "] - Connected to Steam! Logging in '{0}'...", user);
            // Notification.NotifHelper.MessageBox.Show("Info", "Connected to Steam!\nLogging in " + user + "...");
            LoginStatus = "Connected to Steam! Logging in " + user;

            byte[] sentryHash = null;
            if (File.Exists(Program.SentryFolder + user + ".bin"))
            {
                byte[] sentryFile = File.ReadAllBytes(Program.SentryFolder + user + ".bin");
                sentryHash = CryptoHelper.SHAHash(sentryFile);
            }

            //Set LoginKey for user
            /*
            var list = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
            foreach (var a in list.Accounts)
            {
                if (a.username == user)
                {
                    // if (a.LoginKey.ToString() == "undefined") // deu?
                    if (string.IsNullOrEmpty(a.LoginKey) || a.LoginKey.ToString() == "undefined")
                    {
                        // NewloginKey = null;
                        // a.LoginKey = "";
                    }
                    else
                    {
                        NewloginKey = a.LoginKey;
                        myUniqueId = a.LoginKey;
                        File.WriteAllText(Program.AccountsJsonFile, JsonConvert.SerializeObject(list, Formatting.Indented)); // update login key
                    }
                }
            }
            */
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
                ShouldRememberPassword = true
               // LoginKey = NewloginKey
            });
        }


        static void OnLoggedOn(SteamUser.LoggedOnCallback callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }
            
            LastLogOnResult = callback.Result;
            



            bool isSteamGuard = callback.Result == EResult.AccountLogonDenied;
            bool is2FA = callback.Result == EResult.AccountLoginDeniedNeedTwoFactor;
           // bool isLoginKey = callback.Result == EResult.InvalidPassword && NewloginKey != null;

            if (isSteamGuard || is2FA)
            {
               /* if (!isLoginKey)
                {
                    Console.WriteLine("[" + Program.BOTNAME + "] - This account is SteamGuard protected!");
                }
                */

                if (is2FA)
                {
                    //Notification.NotifHelper.MessageBox.Show("Info", "Steam Guard detected, showing input form!");
                    LoginStatus = "Steam Guard detected, showing input form!";

                    SteamGuard SteamGuard = new SteamGuard("Phone", user,false);
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
                else
                {
                    //Notification.NotifHelper.MessageBox.Show("Info", "Steam Guard detected, showing input form!");
                    LoginStatus = "Steam Guard detected, showing input form!";

                    SteamGuard SteamGuard = new SteamGuard(callback.EmailDomain, user,false);
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
                LoginStatus = "Unable to logon to Steam: " + callback.Result;
                isRunning = false;
                return;
            }


            Console.WriteLine("[" + Program.BOTNAME + "] - Successfully logged on! \n Valve Time:" + callback.ServerTime.ToString("R"));
            Notification.NotifHelper.MessageBox.Show("Info", "Successfully logged on!");
            LoginStatus = "Successfully logged on!";


            CurrentSteamID = steamClient.SteamID.ConvertToUInt64();

            myUserNonce = callback.WebAPIUserNonce;
            // UserWebLogOn();


            // myUserNonce = callback.WebAPIUserNonce;
            UserCountry = callback.IPCountryCode;

            UserClanIDS();
            gather_ClanOfficers();

            //ChatMode();

            IsLoggedIn = true;
            LastLogOnResult = EResult.OK;



            var ListUserSettings = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Program.AccountsJsonFile));
            foreach (var a in ListUserSettings.Accounts)
            {
                if (a.username == user)
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


        public static void GetPrivacySettings()
        {
            CPlayer_GetPrivacySettings_Request req = new CPlayer_GetPrivacySettings_Request { };
            playerRequest = playerService.SendMessage(x => x.GetPrivacySettings(req));
        }



        static void OnDisconnected(SteamClient.DisconnectedCallback callback)
        {
            if (callback.UserInitiated)
            {
                return;
            }

            EResult lastLogOnResult = LastLogOnResult;
            LastLogOnResult = EResult.Invalid;
            CurrentPersonaState = 0;
            switch (lastLogOnResult)
            {
                case EResult.AccountDisabled:
                    // Do not attempt to reconnect, those failures are permanent
                    return;
                case EResult.InvalidPassword:
                case EResult.NoConnection:
                case EResult.ServiceUnavailable:
                case EResult.Timeout:
                case EResult.TryAnotherCM:
                case EResult.TwoFactorCodeMismatch:
                    //retry
                    TimeSpan.FromSeconds(5);
                    steamClient.Connect();
                    Console.WriteLine("Disconnected from steam, reconnecting in 5 sec... [" + lastLogOnResult + "]");
                    break;
                case EResult.AccountLoginDeniedNeedTwoFactor:
                    steamClient.Connect();
                    break;

                case EResult.RateLimitExceeded:
                    //retry
                    Console.WriteLine("Disconnected from steam, please try again in 30min [" + lastLogOnResult + "]");

                    break;
                    // default: //test
                    //   TimeSpan.FromSeconds(5);
                    //   steamClient.Connect();
                    //   Console.WriteLine("Disconnected from steam, reconnecting in 5 sec... [" + lastLogOnResult + "]");
                    //    break;

            }
        }


        static void WebAPIUser(SteamUser.WebAPIUserNonceCallback webCallback)
        {
            Console.WriteLine("[" + Program.BOTNAME + "] - Received new WebAPIUserNonce.");

            if (webCallback.Result == EResult.OK)
            {
                if (myUserNonce == webCallback.Nonce)
                {
                    Thread.Sleep(30000);
                    steamUser.RequestWebAPIUserNonce();

                }
                else
                {
                    myUserNonce = webCallback.Nonce;
                    UserWebLogOn();
                }
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
#pragma warning disable CA1308
                    avatarHash = BitConverter.ToString(callback.AvatarHash).Replace("-", "").ToLowerInvariant();
#pragma warning restore CA1308

                    if (string.IsNullOrEmpty(avatarHash) || avatarHash.All(singleChar => singleChar == '0'))
                    {
                        avatarHash = null;
                    }
                }

                AvatarHash = avatarHash;
            }
        }
        /*
        static void OnLoginKey(SteamUser.LoginKeyCallback callback)
        {
            myUniqueId = callback.UniqueID.ToString();
            UserWebLogOn();
            steamUser.RequestWebAPIUserNonce();
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
        */

        static void UserWebLogOn()
        {
            do
            {
                //myUniqueId, steamClient, myUserNonce
               // IsWebLoggedIn = steamWeb.Authenticate(myUniqueId, steamClient, myUserNonce);

                if (!IsWebLoggedIn)
                {
                    Console.WriteLine("[" + Program.BOTNAME + "] - Authentication failed, retrying in 10s...");
                    Thread.Sleep(10000);
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

        //public static bool CheckCookies()
        //{
        //    if (cookiesAreInvalid)
        //    {
        //        return false;
        //    }
        //    try
        //    {
        //        if (!steamWeb.VerifyCookies())
        //        {
        //            Console.WriteLine("[" + Program.BOTNAME + "] - Cookies are invalid. Need to re-authenticate.");

        //            cookiesAreInvalid = true;
        //            steamUser.RequestWebAPIUserNonce();
        //            return false;
        //        }
        //    }
        //    catch
        //    {
        //        Console.WriteLine("[" + Program.BOTNAME + "] - Cookie check failed. http://steamcommunity.com is possibly down.");
        //    }
        //    IsWebLoggedIn = true;
        //    return true;
        //}


        static void OnLoggedOff(SteamUser.LoggedOffCallback callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            LastLogOnResult = callback.Result;

            if (callback.Result.ToString() == "ServiceUnavailable")
            {
                Notification.NotifHelper.MessageBox.Show("Info", "Steam servers down\nreconnecting in 5min " + user + "...");
                Thread.Sleep(5000);
                steamClient.Disconnect();
            }

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
                var id = allfriends.ConvertToUInt64().ToString();
                if (id.StartsWith("7") && allfriends.ConvertToUInt64() != 0 && steamFriends.GetFriendRelationship(allfriends.ConvertToUInt64()) == EFriendRelationship.Friend)
                {
                    Friends.Add(allfriends.ConvertToUInt64());
                }
            }
        }


        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

        static void OnFriendMsg(SteamFriends.FriendMsgCallback callback) // Auto MSG
        {
            if (ChatLogger == true && callback.EntryType == EChatEntryType.ChatMsg)
            {
                ulong FriendID = callback.Sender;
                string Message = callback.Message;

                string FriendName = steamFriends.GetFriendPersonaName(FriendID);
                string nameClean = Regex.Replace(FriendName, "[^A-Za-z0-9 _]", "");

                if (!Directory.Exists(Program.ChatLogsFolder + @"\" + steamClient.SteamID.ConvertToUInt64()))
                {
                    Directory.CreateDirectory(Program.ChatLogsFolder + @"\" + steamClient.SteamID.ConvertToUInt64());
                }

                string FriendIDName = @"\[" + FriendID + "] - " + nameClean + ".txt";
                string pathLog = Program.ChatLogsFolder + @"\" + steamClient.SteamID.ConvertToUInt64() + FriendIDName;

                string FinalMsg = "[" + DateTime.Now + "] " + FriendName + ": " + Message;

                string Separator = "───────────────────";

                string[] files = Directory.GetFiles(Program.ChatLogsFolder + @"\" + steamClient.SteamID.ConvertToUInt64(), "[" + FriendID + "]*.txt");

                if (files.Length > 0)//file exist
                {
                    string[] LastDate = File.ReadLines(files[0]).Last().Split(' '); LastDate[0] = LastDate[0].Substring(1);

                    using (var tw = new StreamWriter(files[0], true))
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
                        case ".pcsleep":
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Sleeping..." + "\r\n\r\n" + Program.BOTNAME);
                            Thread.Sleep(500);
                            SetSuspendState(false, true, true); //sleep
                            break;
                        case ".pchiber":
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Hibernating..." + "\r\n\r\n" + Program.BOTNAME);
                            Thread.Sleep(500);
                            SetSuspendState(true, true, true); //hibernate
                            break;
                        case ".pcrr":
                            var reboot = new ProcessStartInfo("shutdown", "/r /t 0") { CreateNoWindow = true, UseShellExecute = false };
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Restarting..." + "\r\n\r\n" + Program.BOTNAME);
                            Thread.Sleep(500);
                            Process.Start(reboot);
                            break;
                        case ".pcoff":
                            var shutdown = new ProcessStartInfo("shutdown", "/s /t 0") { CreateNoWindow = true, UseShellExecute = false };
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Going down..." + "\r\n\r\n" + Program.BOTNAME);
                            Thread.Sleep(500);
                            Process.Start(shutdown);
                            break;
                        case ".close":
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Closing... :c" + "\r\n\r\n" + Program.BOTNAME);
                            Thread.Sleep(500);
                            Environment.Exit(1);
                            break;
                        case ".logoff":
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Logged off... :c" + "\r\n\r\n" + Program.BOTNAME);
                            Thread.Sleep(500);
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
                                           // Utils.PlayGames(gameuints, clearGames + " | Mercury ☿");
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
                                Utils.PlayNonSteamGame(clearNoN + " | Mercury ☿");
                                steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Playing: " + clearNoN + "\r\n\r\n" + Program.BOTNAME);
                            }
                            else
                            {
                                steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Error: Only 50 chars allowed." + "\r\n\r\n" + Program.BOTNAME);
                            }
                            break;
                        case ".stopgames":
                            Utils.StopGames();
                            steamFriends.SendChatMessage(CurrentAdmin, EChatEntryType.ChatMsg, "Stopping games." + "\r\n\r\n" + Program.BOTNAME);
                            break;
                        case ".steamrep ":
                            // not yet
                            break;
                        case ".trolha":
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
                    // CMessages.Add("I'm using Mercury: Ultimate free open source Steam Tool! - https://github.com/sp0ok3r/Mercury"); // *

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

                string FriendName = steamFriends.GetFriendPersonaName(FriendID);
                string nameClean = Regex.Replace(FriendName, "[^A-Za-z0-9 _]", "");

                string FriendIDName = @"\[" + FriendID + "] - " + nameClean + ".txt";
                string pathLog = Program.ChatLogsFolder + @"\" + steamClient.SteamID.ConvertToUInt64() + FriendIDName;

                if (!Directory.Exists(Program.ChatLogsFolder + @"\" + steamClient.SteamID.ConvertToUInt64()))
                {
                    Directory.CreateDirectory(Program.ChatLogsFolder + @"\" + steamClient.SteamID.ConvertToUInt64());
                }

                string FinalMsg = "[" + DateTime.Now + "] " + steamFriends.GetPersonaName() + ": " + Message;
                string[] files = Directory.GetFiles(Program.ChatLogsFolder + @"\" + steamClient.SteamID.ConvertToUInt64(), "[" + FriendID + "]*.txt");

                string Separator = "───────────────────";

                if (files.Length > 0)//file exist
                {
                    string[] LastDate = File.ReadLines(files[0]).Last().Split(' '); LastDate[0] = LastDate[0].Substring(1);
                    using (var tw = new StreamWriter(files[0], true))
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
                                Thread.Sleep(1000);// my friend needs some OXYGEN 😌
                            }
                        }
                    }
                    else
                    {
                        princessas++;
                        steamFriends.SendChatMessage(allfriends.ConvertToUInt64(), EChatEntryType.ChatMsg, message + "\r\n\r\n" + Program.BOTNAME);
                        Thread.Sleep(1000);// my friend needs some OXYGEN 😌
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
            ClientMsgProtobuf<CMsgClientChangeStatus> requestPersonaFlag = new ClientMsgProtobuf<CMsgClientChangeStatus>(EMsg.ClientChangeStatus)
            {
                Body = { persona_state_flags = uimode }
            };
            steamClient.Send(requestPersonaFlag);
        }
        #endregion

        public static void ChatMode()
        {
            //https://github.com/SteamRE/SteamKit/issues/561
            ClientMsgProtobuf<CMsgClientUIMode> request = new ClientMsgProtobuf<CMsgClientUIMode>(EMsg.ClientCurrentUIMode) { Body = { chat_mode = 2 } };
            steamClient.Send(request);
        }

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
                if ((!ClanDictionary.ContainsKey(steamIDClan)) && steamFriends.GetClanRelationship(steamIDClan) == EClanRelationship.Member)
                {
                    ClanDictionary.Add(steamIDClan, steamFriends.GetClanName(steamIDClan));
                }
            }
        }

        public static void gather_ClanOfficers()
        {
            foreach (var pair in ClanDictionary)
            {
                ClanOfficers(Convert.ToUInt64(pair.Key));
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
            webAPIUserNonce = "";
            user = null;
            isRunning = false;
            IsLoggedIn = false;
            steamUser.LogOff();

            CurrentPersonaState = 0;
            UserPersonaName = "";
            CurrentUsername = "";

            LastLogOnResult = EResult.NotLoggedOn;
        }

        public static void NotifBox(string title, string mess)
        {
            Notification.NotifHelper.MessageBox.Show(title, mess);
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