using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Riot.NET
{
    public class CacheManager
    {
        private static WebClient WClient = new WebClient();

        #region Public methods
        internal readonly static Dictionary<string, Image> ImageCache = new Dictionary<string, Image>();
        internal static Image GetImage(string url, string dir = "")
        {
            if (!ImageCache.ContainsKey(url))
            {
                string d = dir != "" ? "datacache/" + dir : "datacache/img";
                string localpath = Path.Combine(d, Path.GetFileName(url));
                if (!Directory.Exists(d))
                {
                    Directory.CreateDirectory(d);
                }
                if (!File.Exists(localpath))
                {
                    WClient.DownloadFile(url, localpath);
                }
                ImageCache[url] = FreeFileImage(localpath);
            }

            return ImageCache[url];
        }

        internal readonly static Dictionary<string, string> TextFileCache = new Dictionary<string, string>();
        internal static string GetTextFile(string url, string dir = "")
        {
            if (!TextFileCache.ContainsKey(url))
            {
                string d = "datacache/" + dir;
                string localpath = Path.Combine(d, Path.GetFileName(url));
                if (!Directory.Exists(d))
                {
                    Directory.CreateDirectory(d);
                }
                if (!File.Exists(localpath))
                {
                    WClient.DownloadFile(url, localpath);
                }
                TextFileCache[url] = File.ReadAllText(localpath);
            }

            return TextFileCache[url];
        }

        public static void CleanAllCache()
        {
            Directory.Delete("datacache", true);
        }
        #endregion

        #region Private methods
        private static Image FreeFileImage(string path)
        {
            FileStream fs = File.OpenRead(path);
            Image img = Image.FromStream(fs);
            fs.Close();
            return img;
        }

        private static void DownloadFile(string url, string path)
        {

        }
        #endregion
    }
}
