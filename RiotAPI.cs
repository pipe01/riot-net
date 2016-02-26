using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Drawing;

namespace Riot.NET
{
    public class HttpRequestRet
    {
        public HttpWebResponse Response;
        public Status Status;
        public string CacheString;

        internal string GetResponseString()
        {
            if (CacheString != null)
                return CacheString;
            Stream stream = Response.GetResponseStream();
            StreamReader objReader = new StreamReader(stream);
            return objReader.ReadToEnd();
        }
    }

    public class RiotException : Exception
    {
        public StatusStruct Status;

        public RiotException (Status status)
        {
            this.Status = status.Struct;
        }
    }

    public class RiotAPI
    {
        internal const string ApiKey = "2873fc75-84b5-493e-bc1b-47aaa93491b2";
        internal const string APIEndpoint = "https://euw.api.pvp.net";

        public static string Region = "euw";

        private static Dictionary<string, HttpRequestRet> ResponseCache = new Dictionary<string, HttpRequestRet>();

        internal static HttpRequestRet MakeRequest(string url, bool cache = false)
        {
            var furl = url
                   .Replace("$AE", APIEndpoint)
                   .Replace("$AK", "?api_key=" + ApiKey)
                   .Replace("$SR", Region);
            if (ResponseCache.ContainsKey(furl) && cache)
            {
                return ResponseCache[furl];
            }

            var req = (HttpWebRequest) WebRequest.Create(furl);
            WebResponse resp = null;

            HttpRequestRet ret = new HttpRequestRet();

            try
            {
                resp = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    var r = e.Response.GetResponseStream();
                    byte[] d = new byte[r.Length];
                    r.Read(d, 0, (int)r.Length);
                    string str = Encoding.ASCII.GetString(d);

                    ret.Status = JObject.Parse(str).ToObject<Status>();

                    Console.WriteLine("Status Code : {0}", ((HttpWebResponse)e.Response).StatusCode);
                    Console.WriteLine("Status Description : {0}", ((HttpWebResponse)e.Response).StatusDescription);
                }
            }

            ret.Response = (HttpWebResponse)resp;

            if (cache)
            {
                ResponseCache[furl] = ret;
                ResponseCache[furl].CacheString = ret.GetResponseString();
            }
            return ret;
        }

        public static Dictionary<string, object> GetAllPropertiesStrings<T>(T obj)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            Type ty = obj.GetType();

            FieldInfo[] fields = ty.GetFields();

            foreach (var item in fields)
            {
                dic.Add(item.Name, item.GetValue(obj));
            }

            return dic;
        }
    }

    public static class Extensions
    {
        internal static string CombineIDs(this Dictionary<string, PlayerSBN> dic)
        {
            StringBuilder str = new StringBuilder();
            foreach (var item in dic)
            {
                str.Append(item.Value.SummonerID.ToString());
                if (dic.Last().Value != item.Value)
                {
                    str.Append(",");
                }
            }
            return str.ToString();
        }

        internal static Dictionary<string, object> GetAllRiotData(this RiotData obj)
        {
            return RiotAPI.GetAllPropertiesStrings(obj);
        }

        internal static IEnumerable<JProperty> GetAllJSONProperties(this string str)
        {
            return JObject.Parse(str).Properties();
        }

        internal static T ParseJSON<T>(this string json)
        {
            return JObject.Parse(json).ToObject<T>();
        }

        internal static string Combine<T>(this T[] arr, string separator = ",")
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                str.Append(arr[i].ToString());
                if (i != arr.Length - 1)
                    str.Append(separator);
            }
            return str.ToString();
        }

        public static string FirstUpper(this string str)
        {
            return str[0].ToString().ToUpper() + str.Substring(1).ToLower();
        }

        public static DateTime FromUnixTime(this long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        public static long ToUnixTime(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }
    }
}
