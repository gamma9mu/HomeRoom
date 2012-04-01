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
    /// <summary>
    /// Search for image items using Bing Search API.
    /// </summary>
    public class BingImageCrawler : BingCrawler
    {
        /// <summary>
        /// The source string for use in search queries.
        /// </summary>
        private static string sources = "Image";
        
        /// <summary>
        /// A mapping from XML element names to fields on a <code>Result</code>.
        /// </summary>
        private static Dictionary<DATATYPES, string> dataMapping = new Dictionary<DATATYPES, string>()
        {
            {DATATYPES.TITLE, "Title"},
            {DATATYPES.DATETIME, "DateTime"},
            {DATATYPES.URL, "MediaUrl"},
            {DATATYPES.WIDTH, "Width"},
            {DATATYPES.HEIGHT, "Height"},
            {DATATYPES.SIZE, "FileSize"},
            {DATATYPES.MIME, "ContentType"}
        };

        /// <summary>
        /// Implement the specific form of searching for images.
        /// </summary>
        /// <param name="query">The topic to search for.</param>
        /// <param name="offset">The required beginning offset into the
        /// results.</param>
        /// <param name="count">The number of results to request.</param>
        /// <param name="results">The collection to store results into.</param>
        /// <returns>The number of results that were stored into
        /// <code>results</code>.</returns>
        override protected int obtainResultRange(string query, int offset,
            int count, List<Result> results)
        {
            if (count < COUNT_MIN) count = COUNT_MIN;
            else if (count > COUNT_MAX) count = COUNT_MAX;

            if (offset < OFFSET_MIN) offset = OFFSET_MIN;
            else if (offset > OFFSET_MAX) offset = OFFSET_MAX;

            string completeUri = String.Format(Properties.Resources.SearchUrl,
                Properties.Resources.AppId, sources, HttpUtility.UrlEncode(query),
                offset, count);
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
            if (err == null) return 0; // TODO log this

            // Find the results
            XPathNodeIterator iter = (XPathNodeIterator) nav.Evaluate("//mms:ImageResult", nsmgr);
            if (iter == null) return 0;

            int foundCount = 0;
            while (iter.MoveNext())
            {
                results.Add(createResultFromXml(iter, "mms", dataMapping));
                foundCount++;
            }

            return foundCount;
        }
    }
}
