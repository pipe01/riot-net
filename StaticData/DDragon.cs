using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Riot.NET.StaticData
{
    public class DDragon
    {
        private static List<string> VersionsCache;
        public static List<string> GetAllVersions()
        {
            if (VersionsCache == null || VersionsCache.Count == 0)
            {
                var resp = RiotAPI.MakeRequest("https://ddragon.leagueoflegends.com/api/versions.json", true);
                string str = resp.GetResponseString();

                List<string> versions = new List<string>();

                JArray a = JArray.Parse(str);

                foreach (var o in a)
                {
                    versions.Add(o.ToObject<string>());
                }

                VersionsCache = versions;

                return versions;
            }
            else
            {
                return VersionsCache;
            }
        }

        public static Image GetProfileIcon(int id)
        {
            string filepath = "./datacache/profileicons/" + id + ".png";
            string url = "http://ddragon.leagueoflegends.com/cdn/VER/img/profileicon/ID.png";
            url = url.Replace("VER", GetAllVersions().First())
                     .Replace("ID", id.ToString());

            /*WebClient client = new WebClient();
            byte[] data = client.DownloadData(url);
            FileStream fs = File.Create(filepath);
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();*/
            return CacheManager.GetImage(url, "profileicons");
        }
    }
}
