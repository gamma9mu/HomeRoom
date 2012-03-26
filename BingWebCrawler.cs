using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Xml;
using System.Xml.XPath;

namespace HomeRoom
{
    public class BingWebCrawler : BingCrawler
    {

        private static Dictionary<DATATYPES, string> dataMapping = new Dictionary<DATATYPES, string>()
        {
            {DATATYPES.TITLE, "Title"},
            {DATATYPES.DESCRIPTION, "Description"},
            {DATATYPES.DATETIME, "DateTime"},
            {DATATYPES.URL, "Url"}
        };

        /// <summary>
        /// Search Bing for web resources.
        /// </summary>
        /// <param name="query">A search query word or phrase to use as the keywords in the
        /// web search.</param>
        /// <returns>A <code>List&lt;Result&gt;</code> of the results found by Bing.</returns>
        override public List<Result> find(string query)
        {
            string sources = "Web";
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
                results.Add(createResultFromXml(iter, "web", dataMapping));
            return results;
        }
    }
}
