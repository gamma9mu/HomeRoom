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
    class WikipediaWebCrawler : ICrawler
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
            HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(completeUri);
            HttpWebResponse webResponse = (HttpWebResponse) webRequest.GetResponse();
            XmlReader reader = XmlReader.Create(webResponse.GetResponseStream());

            // The namespace manager is needed to handle XML prefixes in the results
            nsmgr = new XmlNamespaceManager(reader.NameTable);
            //nsmgr.AddNamespace("web", Properties.Resources.webSchema);

            XPathDocument doc = new XPathDocument(reader);
            XPathNavigator nav = doc.CreateNavigator();

            // Find the results
            XPathNodeIterator iter = (XPathNodeIterator) nav.Evaluate("//Section", nsmgr);
            if (iter == null) return results; // which is currently empty

            while (iter.MoveNext())
            {
                XPathNavigator iterator = iter.Current;

                nav = iterator.SelectSingleNode("Url", nsmgr);
                System.Diagnostics.Debug.WriteLine(nav);
                if (nav == null) continue;
                string url = nav.InnerXml;
                Result result = new Result(url);

                nav = iterator.SelectSingleNode("Text", nsmgr);
                if (nav != null)
                    result.Title = nav.InnerXml;
                

                nav = iterator.SelectSingleNode("Description", nsmgr);
                if (nav != null)
                    result.Description = nav.InnerXml;

                result.MimeType = "WIKIPEDIA";

                results.Add(result);
            }

            return results;
        }
    }
}
