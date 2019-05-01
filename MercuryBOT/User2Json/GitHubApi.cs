using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercuryBOT.User2Json
{
   public class GitHubApi
    {
        public class GithubRelease
        {
            public string html_url { get; set; }
            public string tag_name { get; set; }
            public List<asset> assets { get; set; }
            public string body { get; set; }
        }

        public class asset
        {
            public string browser_download_url { get; set; }
        }
    }
}
