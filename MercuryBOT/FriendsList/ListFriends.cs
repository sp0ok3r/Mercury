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
using System.Collections.Generic;

namespace MercuryBOT.FriendsList
{

    //Thanks to https://github.com/waylaidwanderer/Mist
    internal class ListFriends
    {
        private static object locker = new object();
        private static List<ListFriends> list = new List<ListFriends>();
        private string name = "";
        private string game = "";
        private ulong sid = 0;
        private string status = "";
        private string lastlogoff = "";

        public ListFriends(string name, ulong sid, string game, string status, string lastlogoff)
        {
            this.name = name;
            this.sid = sid;
            this.game = game;
            this.status = status;
            this.lastlogoff = lastlogoff;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public string Game
        {
            get
            {
                return this.game;
            }
            set
            {
                this.game = value;
            }
        }

        public ulong SID
        {
            get
            {
                return this.sid;
            }
            set
            {
                this.sid = value;
            }
        }

        public string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }


        public string LastLogOff
        {
            get
            {
                return this.lastlogoff;
            }
            set
            {
                this.lastlogoff = value;
            }
        }

        public static void Add(string name, ulong sid, string game = "", string status = "Offline", string lastlogoff = "")
        {
            lock (ListFriends.locker)
            {
                ListFriends listFriends = new ListFriends(name, sid, game, status, lastlogoff);
                ListFriends.list.Add(listFriends);
            }
        }

        public static void Remove(ulong sid)
        {
            lock (ListFriends.locker)
            {
                ListFriends listFriends = ListFriends.list.Find((Predicate<ListFriends>)(x => (long)x.SID == (long)sid));
                ListFriends.list.Remove(listFriends);
            }
        }

        public static void Clear()
        {
            lock (ListFriends.locker)
                ListFriends.list.Clear();
        }

        public static void UpdateStatus(ulong sid, string status)
        {
            lock (ListFriends.locker)
            {
                try
                {
                    ListFriends.list.Find((Predicate<ListFriends>)(x => (long)x.SID == (long)sid)).Status = status;
                }
                catch
                {
                }
            }
        }

        public static void UpdateName(ulong sid, string name)
        {
            lock (ListFriends.locker)
            {
                try
                {
                    ListFriends.list.Find((Predicate<ListFriends>)(x => (long)x.SID == (long)sid)).Name = name;
                }
                catch
                {
                }
            }
        }
       
        public static void UpdateLastLogOff(ulong sid, string lastlogoff)
        {
            lock (ListFriends.locker)
            {
                try
                {
                    ListFriends.list.Find((Predicate<ListFriends>)(x => (long)x.SID == (long)sid)).Name = lastlogoff;
                }
                catch
                {
                }
            }
        }


        public static ulong GetSID(string name)
        {
            lock (ListFriends.locker)
            {
                try
                {
                    return ListFriends.list.Find((Predicate<ListFriends>)(x => x.name == name)).sid;
                }
                catch
                {
                }
                return 0;
            }
        }

        internal static List<ListFriends> Get(string name)
        {
            lock (ListFriends.locker)
            {
                name = name.ToLower();
                List<ListFriends> listFriendsList = new List<ListFriends>();
                foreach (ListFriends listFriends in ListFriends.list)
                {
                    if (listFriends.name.ToLower().Contains(name))
                        listFriendsList.Add(listFriends);
                }
                return listFriendsList;
            }
        }

        internal static ListFriends GetFriend(ulong steamId)
        {
            lock (ListFriends.locker)
            {
                ListFriends listFriends = ListFriends.list.Find((Predicate<ListFriends>)(x => (long)x.SID == (long)steamId));
                if (listFriends != null)
                {
                    if (listFriends.status == "LookingToTrade")
                        return new ListFriends(listFriends.Name, listFriends.SID, listFriends.Game, listFriends.Status , listFriends.LastLogOff)
                        {
                            status = "Looking to Trade"
                        };
                    if (listFriends.status == "LookingToPlay")
                        return new ListFriends(listFriends.Name, listFriends.SID, listFriends.Game, listFriends.Status, listFriends.LastLogOff)
                        {
                            status = "Looking to Play"
                        };
                }
                return listFriends;
            }
        }

        internal static List<ListFriends> Get(bool onlineOnly = false)
        {
            lock (ListFriends.locker)
            {
                List<ListFriends> listFriendsList = new List<ListFriends>();
                foreach (ListFriends listFriends in ListFriends.list)
                {
                    if (listFriends.status == "Online")
                        listFriendsList.Add(listFriends);
                }
                foreach (ListFriends listFriends in ListFriends.list)
                {
                    if (listFriends.status == "LookingToTrade")
                        listFriendsList.Add(new ListFriends(listFriends.Name, listFriends.SID, listFriends.Game, listFriends.Status, listFriends.LastLogOff)
                        {
                            status = "Looking to Trade"
                        });
                }
                foreach (ListFriends listFriends in ListFriends.list)
                {
                    if (listFriends.status == "LookingToPlay")
                        listFriendsList.Add(new ListFriends(listFriends.Name, listFriends.SID, listFriends.Game, listFriends.Status, listFriends.LastLogOff)
                        {
                            status = "Looking to Play"
                        });
                }
                foreach (ListFriends listFriends in ListFriends.list)
                {
                    if (listFriends.status == "Busy")
                        listFriendsList.Add(listFriends);
                }
                foreach (ListFriends listFriends in ListFriends.list)
                {
                    if (listFriends.status == "Away")
                        listFriendsList.Add(listFriends);
                }
                foreach (ListFriends listFriends in ListFriends.list)
                {
                    if (listFriends.status == "Snooze")
                        listFriendsList.Add(listFriends);
                }
                if (!onlineOnly)
                {
                    foreach (ListFriends listFriends in ListFriends.list)
                    {
                        if (listFriends.status == "Offline")
                            listFriendsList.Add(listFriends);
                    }
                }
                return listFriendsList;
            }
        }
    }
}
