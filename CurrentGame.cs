using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riot.NET
{
    public class CurrentGame
    {
        public enum TeamID
        {
            Blue = 100,
            Red = 200
        }

        public static CurrentGameInfo GetCurrentGame(string id)
        {
            var resp = RiotAPI.MakeRequest
                ("$AE/observer-mode/rest/consumer/getSpectatorGameInfo/EUW1/" + id + "$AK");
            if (resp.Status != null) throw new RiotException(resp.Status);

            string str = resp.GetResponseString();

            CurrentGameInfo cgi = str.ParseJSON<CurrentGameInfo>();

            return cgi;
        }
    }
}
