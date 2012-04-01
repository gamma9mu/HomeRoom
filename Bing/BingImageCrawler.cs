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
        private static string sources = "Image";

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
