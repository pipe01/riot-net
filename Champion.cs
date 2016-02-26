using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riot.NET
{
    public class Champion
    {
        public static ChampionListDto GetChampionList()
        {
            var resp = RiotAPI.MakeRequest
                ("$AE/api/lol/euw/v1.2/champion$AK", true);
            if (resp.Status != null) throw new RiotException(resp.Status);

            string str = resp.GetResponseString();

            ChampionListDto lst = str.ParseJSON<ChampionListDto>();

            return lst;
        }
    }
}
