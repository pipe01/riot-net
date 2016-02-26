using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riot.NET
{
    public class Summoner
    {
        public static Dictionary<string, PlayerSBN> GetByName(string summonerNames)
        {
            if (summonerNames == null)
                throw new ArgumentNullException();

            var resp = RiotAPI.MakeRequest
                ("$AE/api/lol/$SR/v1.4/summoner/by-name/" + summonerNames + "$AK", true);
            if (resp.Status != null) throw new RiotException(resp.Status);


            var line = resp.GetResponseString();

            Dictionary<string, PlayerSBN> infDic = new Dictionary<string, PlayerSBN>();

            line.GetAllJSONProperties().All(o =>
            {
                var obj = o.First.ToObject<PlayerSBN>();
                infDic.Add(obj.SummonerName, obj);
                return true;
            });

            return infDic;
        }

        public static Dictionary<string, PlayerSBN> GetByID(string summonerIDs)
        {
            if (summonerIDs == null)
                throw new ArgumentNullException();

            var resp = RiotAPI.MakeRequest
                ("$AE/api/lol/$SR/v1.4/summoner/" + summonerIDs + "$AK", true);
            if (resp.Status != null) throw new RiotException(resp.Status);


            var line = resp.GetResponseString();

            Dictionary<string, PlayerSBN> infDic = new Dictionary<string, PlayerSBN>();

            line.GetAllJSONProperties().All(o =>
            {
                var obj = o.First.ToObject<PlayerSBN>();
                infDic.Add(obj.SummonerName, obj);
                return true;
            });

            return infDic;
        }

        public static Dictionary<long, MasteryPagesDto> GetMasteryPages(string summonerIDs)
        {
            var resp = RiotAPI.MakeRequest("$AE/api/lol/euw/v1.4/summoner/" + summonerIDs + "/masteries$AK", true);
            if (resp.Status != null) throw new RiotException(resp.Status);

            string str = resp.GetResponseString();

            Dictionary<long, MasteryPagesDto> mastDic = new Dictionary<long, MasteryPagesDto>();

            str.GetAllJSONProperties().All(o =>
            {
                var obj = o.First.ToObject<MasteryPagesDto>();
                mastDic.Add(obj.SummonerID, obj);
                return true;
            });

            return mastDic;
        }
    }
}
