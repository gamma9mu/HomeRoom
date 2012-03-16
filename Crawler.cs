using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.XPath;

namespace HomeRoom
{
    public class Crawler
    {
        /// XML namespace lookup aide.
        private XmlNamespaceManager nsmgr = null;

        /// <summary>
        /// Search Bing for web resources.
        /// </summary>
        /// <param name="query">A search query word or phrase to use as the keywords in the
        /// web search.</param>
        /// <param name="sources">The type of search to perform as a string-keyword.
        /// Available options are:
        /// <ul>
        ///     <li><code>"Web"</code> for website results (default).</li>
        /// </ul></param>
        /// <returns>A <code>List&lt;Result&gt;</code> of the results found by Bing.</returns>
        public List<Result> find(string query, string sources = "Web")
        {
            string completeUri = String.Format(Properties.Resources.SearchUrl,
                Properties.Resources.AppId, sources, HttpUtility.UrlEncode(query));
            HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(completeUri);
            HttpWebResponse webResponse = (HttpWebResponse) webRequest.GetResponse();
            XmlReader reader = XmlReader.Create(webResponse.GetResponseStream());

            // The namespace manager is needed to handle XML prefixes in the results
            nsmgr = new XmlNamespaceManager(reader.NameTable);
            nsmgr.AddNamespace("web", Properties.Resources.webSchema);

            XPathDocument doc = new XPathDocument(reader);
            XPathNavigator nav = doc.CreateNavigator();

            // Check for errors
            XPathNodeIterator err = (XPathNodeIterator) nav.Evaluate("//Errors", nsmgr);
            if (err == null) return null; // TODO log this

            // Find the results
            XPathNodeIterator iter = (XPathNodeIterator) nav.Evaluate("//web:WebResult", nsmgr);
            if (iter == null) return null;

            // Return the search results
            List<Result> results = new List<Result>(10);
            while (iter.MoveNext())
                results.Add(createResultFromXml(iter));
            return results;
        }

        /// <summary>
        /// Create a <code>Result</code> object from the current result in the Bing XML
        /// response.
        /// </summary>
        /// <param name="iterator">An <code>XPathNodeIterator</code> pointing to a
        /// <code>web:WebResult</code> in Bing's response.</param>
        /// <returns>A <code>Result</code> object with the read data.</returns>
        private Result createResultFromXml(XPathNodeIterator iterator)
        {
            XPathNavigator iter = iterator.Current;
            // nav will be reused as a temporary throughout this section to handle
            // null results without throwing a NullReferenceException.  Pardon the ternaries...
            XPathNavigator nav = iter.SelectSingleNode("web:Title", nsmgr);
            string title = (nav != null) ? nav.InnerXml : null;
            
            nav = iter.SelectSingleNode("web:Description", nsmgr);
            string description = (nav != null) ? nav.InnerXml : null;
            
            nav = iter.SelectSingleNode("web:DateTime", nsmgr);
            DateTime datetime = (nav != null) ? DateTime.Parse(nav.InnerXml) : DateTime.UtcNow;
            nav = iter.SelectSingleNode("web:Url", nsmgr);
            string url = (nav != null) ? nav.InnerXml : null;
            
            //Console.Out.WriteLine(dt.ToString());
            return new Result(title, description, datetime, url);
        }

        // testing main()
        static void Main(string[] args)
        {
            Crawler c = new Crawler();
            foreach (var item in c.find("cool+stuff"))
                Console.Out.WriteLine(item);
        }
    }
}