using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using System.Net;
using System.Xml;
using System.Web;

namespace HomeRoom
{
    public class WikipediaWebCrawler : ICrawler
    {
        private XmlNamespaceManager nsmgr;

        /// <summary>
        /// Search Wikipedia for resources of on a given subject.
        /// </summary>
        /// <param name="query">A search query word or phrase to use as the keywords in the
        /// web search.</param>
        /// <param name="count">How many items to retrieve.</param>
        /// <returns>A <code>List&lt;Result&gt;</code> of the results found by Wikipedia.</returns>
        public List<Result> find(string query, int count)
        {
            List<Result> results = new List<Result>(count);

            string completeUri = String.Format(Properties.Resources.WikipediaUrl,
                HttpUtility.UrlEncode(query), count);
            HttpWebRequest webRequest = (HttpWebRequest) HttpWebRequest.Create(completeUri);
            webRequest.UserAgent = Properties.Resources.WikipediaUserAgent;
            HttpWebResponse webResponse = (HttpWebResponse) webRequest.GetResponse();
            XmlReader reader = XmlReader.Create(webResponse.GetResponseStream());

            // The namespace manager is needed to handle XML prefixes in the results
            nsmgr = new XmlNamespaceManager(reader.NameTable);
            // This is how you deal with an XML namespace that doesn't use a prefix:
            nsmgr.AddNamespace("zz", Properties.Resources.WikipediaResponseNs);

            XPathDocument doc = new XPathDocument(reader);
            XPathNavigator nav = doc.CreateNavigator();
            
            // Find the results
            XPathNodeIterator iter = nav.Select("//zz:Item", nsmgr);
            if (iter == null) return results; // which is currently empty

            while (iter.MoveNext())
            {
                XPathNavigator iterator = iter.Current;

                nav = iterator.SelectSingleNode("zz:Url", nsmgr);

                if (nav == null) continue;
                string url = nav.InnerXml;
                Result result = new Result(url);

                nav = iterator.SelectSingleNode("zz:Text", nsmgr);
                if (nav != null)
                    result.Title = nav.InnerXml;
                

                nav = iterator.SelectSingleNode("zz:Description", nsmgr);
                if (nav != null)
                    result.Description = nav.InnerXml;

                results.Add(result);
            }

            return results;
        }
    }
}
