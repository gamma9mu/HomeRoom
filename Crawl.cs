using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace Crawler
{
    public class Crawl
    {
        /// Holds urls returned by query
        static string[] urls = new string[50];

        public void find(string query, string sources = "web")
        {
            string completeUri = String.Format(Properties.Resources.SearchUrl,
                Properties.Resources.AppId, sources, query);
            HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(completeUri);
            HttpWebResponse webResponse = (HttpWebResponse) webRequest.GetResponse();
            XmlReader reader = XmlReader.Create(webResponse.GetResponseStream());

            int i = 0;
            while (i < 10)
            {
                reader.ReadToFollowing("web:Title");
                string title = reader.ReadString();
                if (title == null) break;

                reader.ReadToFollowing("web:Description");
                string description = reader.ReadString();

                reader.ReadToFollowing("web:DateTime");
                string datetime = reader.ReadString();

                reader.ReadToFollowing("web:Url");
                string url = reader.ReadString();

                Console.Out.WriteLine("web:Title: " + title);
                Console.Out.WriteLine("web:Description: " + description);
                Console.Out.WriteLine("web:DateTime: " + datetime);
                Console.Out.WriteLine(url + "\n");

                urls[i] = url;
                i++;
            }

            /// To be implemented...
            /// Distribute url results to another program to be ranked
        }

        static void Main(string[] args)
        {
            Crawl c = new Crawl();
            c.find("cool+stuff");
        }
    }
}