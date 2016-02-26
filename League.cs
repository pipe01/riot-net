using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riot.NET
{
    public class League
    {
        public static Dictionary<string, List<LeagueDto>> GetEntryByID(string summonerIds)
        {
            var resp = RiotAPI.MakeRequest
                ("$AE/api/lol/$SR/v2.5/league/by-summoner/" + summonerIds + "/entry$AK");
            if (resp.Status != null) throw new RiotException(resp.Status);

            var line = resp.GetResponseString();

            Dictionary<string, List<LeagueDto>> infDic = new Dictionary<string, List<LeagueDto>>();

            line.GetAllJSONProperties().All(o => 
            {
                var obj = o.First.ToObject<List<LeagueDto>>();
                string name = obj[0].Entries[0].PlayerOrTeamName;
                if (!infDic.ContainsKey(name))
                    infDic.Add(name, obj);
                return true;
            });

            return infDic;
        }
    }
}
