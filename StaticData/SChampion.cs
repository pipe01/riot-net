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
    public class SChampion
    {
        public static Image GetChampionIcon(string champName)
        {
            string champN = champName
                .Replace(" ", "")
                .Replace("'", "")
                .Replace("Wukong", "MonkeyKing")
                .Replace("KhaZix", "Khazix")
                .Replace("ChoGath", "Chogath")
                .Replace("Fiddlesticks", "FiddleSticks")
                .Replace("Dr.Mundo", "DrMundo");
            
            return CacheManager.GetImage("http://ddragon.leagueoflegends.com/cdn/6.3.1/img/champion/" + champN + ".png", "champicon");
        }
        public static Image GetChampionIcon(int champId)
        {
            return GetChampionIcon(GetChampionsIDName()[champId]);
        }

        private static Dictionary<int, string> ChampionCache;
        public static Dictionary<int, string> GetChampionsIDName()
        {
            if (ChampionCache != null)
                return ChampionCache;

            string file = CacheManager.GetTextFile("http://ddragon.leagueoflegends.com/cdn/6.3.1/data/en_US/champion.json");

            var dyn = file.GetAllJSONProperties();

            Dictionary<int, string> dic = new Dictionary<int, string>();

            foreach (var item in dyn.Last().Children().First().Children())
            {
                var key = item.First["key"];
                var name = item.First["name"];
                dic.Add(key.ToObject<int>(), name.ToString());
            }

            ChampionCache = dic;
            return dic;
        }

        public static string GetChampionName(int id)
        {
            return GetChampionsIDName()[id];
        }
    }
}
