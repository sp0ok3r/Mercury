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
using System.Linq;

namespace MercuryBOT.FriendsList
{
    //Thanks to https://github.com/waylaidwanderer/Mist
    internal class ListFriendRequests
    {
        private static List<ListFriendRequests> list = new List<ListFriendRequests>();
        private string name;
        private ulong sid;
        private string status;


        public ListFriendRequests(string name, ulong sid, string status = "Offline")
        {
            this.name = name;
            this.sid = sid;
            this.status = status;
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

        public ulong SteamID
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

        public static void Add(string name, ulong sid, string status = "Offline")
        {
            ListFriendRequests listFriendRequests = new ListFriendRequests(name, sid, status);
            ListFriendRequests.list.Add(listFriendRequests);
        }

        public static void Remove(ulong sid)
        {
            ListFriendRequests listFriendRequests = ListFriendRequests.list.Find((Predicate<ListFriendRequests>)(x => (long)x.sid == (long)sid));
            ListFriendRequests.list.Remove(listFriendRequests);
        }

        public static void Clear()
        {
            ListFriendRequests.list.Clear();
        }

        public static void UpdateStatus(ulong sid, string status)
        {
            try
            {
                ListFriendRequests.list.Find((Predicate<ListFriendRequests>)(x => (long)x.SteamID == (long)sid)).Status = status;
            }
            catch
            {
            }
        }

        public static bool Find(ulong sid)
        {
            return ListFriendRequests.list.Any<ListFriendRequests>((Func<ListFriendRequests, bool>)(x => (long)x.SteamID == (long)sid));
        }

        public static ulong GetSID(string name)
        {
            try
            {
                return ListFriendRequests.list.Find((Predicate<ListFriendRequests>)(x => x.name == name)).sid;
            }
            catch
            {
            }
            return 0;
        }

        internal static List<ListFriendRequests> Get()
        {
            List<ListFriendRequests> listFriendRequestsList = new List<ListFriendRequests>();
            foreach (ListFriendRequests listFriendRequests in ListFriendRequests.list)
            {
                if (listFriendRequests.status.ToString() == "Online")
                    listFriendRequestsList.Add(listFriendRequests);
            }
            foreach (ListFriendRequests listFriendRequests in ListFriendRequests.list)
            {
                if (listFriendRequests.status.ToString() == "LookingToTrade")
                    listFriendRequestsList.Add(listFriendRequests);
            }
            foreach (ListFriendRequests listFriendRequests in ListFriendRequests.list)
            {
                if (listFriendRequests.status.ToString() == "LookingToPlay")
                    listFriendRequestsList.Add(listFriendRequests);
            }
            foreach (ListFriendRequests listFriendRequests in ListFriendRequests.list)
            {
                if (listFriendRequests.status.ToString() == "Busy")
                    listFriendRequestsList.Add(listFriendRequests);
            }
            foreach (ListFriendRequests listFriendRequests in ListFriendRequests.list)
            {
                if (listFriendRequests.status.ToString() == "Away")
                    listFriendRequestsList.Add(listFriendRequests);
            }
            foreach (ListFriendRequests listFriendRequests in ListFriendRequests.list)
            {
                if (listFriendRequests.status.ToString() == "Snooze")
                    listFriendRequestsList.Add(listFriendRequests);
            }
            foreach (ListFriendRequests listFriendRequests in ListFriendRequests.list)
            {
                if (listFriendRequests.status.ToString() == "Offline")
                    listFriendRequestsList.Add(listFriendRequests);
            }
            return listFriendRequestsList;
        }
    }
}
