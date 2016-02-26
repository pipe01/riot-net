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
    public class SSummSpells
    {
        public static readonly Dictionary<int, string> SummonerSpells = new Dictionary<int, string>() {
            { 1, "SummonerBoost"},
            { 2, "SummonerClairvoyance" },
            { 21, "SummonerBarrier" },
            { 3, "SummonerExhaust" },
            { 4, "SummonerFlash" },
            { 6, "SummonerHaste"},
            { 7, "SummonerHeal" },
            { 12, "SummonerTeleport" },
            { 32, "SummonerSnowball" },
            { 11, "SummonerSmite" },
            { 30, "SummonerPoroRecall"},
            { 31, "SummonerPoroThrow" },
            { 13, "SummonerMana" },
            { 14, "SummonerDot" },
            { 17, "SummonerOdinGarrison" },
        };

        public static Image GetIcon(int id)
        {
            string name = SummonerSpells[id];
            return CacheManager.GetImage("http://ddragon.leagueoflegends.com/cdn/6.3.1/img/spell/" + name + ".png", "sspells");
        }
    }
}
