using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using System.Net;

namespace HomeRoom
{
    public class BingImageCrawler : BingCrawler
    {
        private static Dictionary<DATATYPES, string> dataMapping = new Dictionary<DATATYPES, string>()
        {
            {DATATYPES.TITLE, "Title"},
            //{DATATYPES.DESCRIPTION, "Description"},
            {DATATYPES.DATETIME, "DateTime"},
            {DATATYPES.URL, "MediaUrl"}
        };

        /// <summary>
        /// Search Bing for image resources.
        /// </summary>
        /// <param name="query">A search query word or phrase to use as the keywords in the
        /// image search.</param>
        /// <returns>A <code>List&lt;Result&gt;</code> of the results found by Bing.</returns>
        override public List<Result> find(string query)
        {
            string sources = "Image";
            string completeUri = String.Format(Properties.Resources.SearchUrl,
                Properties.Resources.AppId, sources, HttpUtility.UrlEncode(query));
            HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(completeUri);
            HttpWebResponse webResponse = (HttpWebResponse) webRequest.GetResponse();
            XmlReader reader = XmlReader.Create(webResponse.GetResponseStream());

            // The namespace manager is needed to handle XML prefixes in the results
            nsmgr = new XmlNamespaceManager(reader.NameTable);
            nsmgr.AddNamespace("mms", Properties.Resources.imageSchema);

            XPathDocument doc = new XPathDocument(reader);
            XPathNavigator nav = doc.CreateNavigator();

            // Check for errors
            XPathNodeIterator err = (XPathNodeIterator) nav.Evaluate("//Errors", nsmgr);
            if (err == null) return null; // TODO log this

            // Find the results
            XPathNodeIterator iter = (XPathNodeIterator) nav.Evaluate("//mms:ImageResult", nsmgr);
            if (iter == null) return null;

            // Return the search results
            List<Result> results = new List<Result>(10);
            while (iter.MoveNext())
                results.Add(createResultFromXml(iter, "mms", dataMapping));
            return results;
        }
    }
}
