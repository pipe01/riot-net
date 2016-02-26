using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riot.NET.StaticData
{
    public class SMasteries
    {
        public static MasteryPageStatic GetMasteryPage(string language = "en_US")
        {
            string latVer = DDragon.GetAllVersions()[0];
            var resp = RiotAPI.MakeRequest(
                "http://ddragon.leagueoflegends.com/cdn/" + latVer + "/data/" + language + "/mastery.json");
            if (resp.Status != null) throw new RiotException(resp.Status);

            string str = resp.GetResponseString();

            MasteryPageStatic page = new MasteryPageStatic();
            dynamic ret = str.ParseJSON<dynamic>();
            JObject tree = ret.tree;
            JObject data = ret.data;

            page.Type = ret.type;
            page.Version = ret.version;
            page.Tree = new List<MasteryTreeStatic>();
            page.Data = new MasteryDataStatic();
            page.Data.Entries = new List<MasteryDataEntryStatic>();

            foreach (var item in tree)
            {
                MasteryTreeStatic t = new MasteryTreeStatic();
                t.TreeLevels = new List<MasteryEntryStatic>();
                t.Name = item.Key;
                foreach (var child in item.Value.Children())
                {
                    MasteryEntryStatic entry = new MasteryEntryStatic();
                    foreach (var level in child.Children().Where(o => o.Children().Count() > 0))
                    {
                        entry.MasteryID = level.First.ToObject<string>();
                        entry.Prequisites = level.Last.ToObject<string>();
                    }
                    t.TreeLevels.Add(entry);
                }
                page.Tree.Add(t);
            }

            foreach (var item in data)
            {
                page.Data.Entries.Add(item.Value.ToObject<MasteryDataEntryStatic>());
            }

            return page;
        }
    }
}
