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
    public abstract class BingCrawler : ICrawler
    {
        /// XML namespace lookup aide.
        protected XmlNamespaceManager nsmgr = null;
        /// <summary>
        /// An enumeration of the different data elements that can be found in
        /// a result set.
        /// </summary>
        protected enum DATATYPES { TITLE, DESCRIPTION, DATETIME, URL };

        /// <summary>
        /// From ICrawler
        /// </summary>
        /// <param name="query">A search string.</param>
        /// <returns>The results of a Bing search.</returns>
        abstract public List<Result> find(string query);

        public List<Result> findImage(string query, string sources = "Image")
        {
            string completeUri = String.Format(Properties.Resources.SearchUrl,
                Properties.Resources.AppId, sources, HttpUtility.UrlEncode(query));
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(completeUri);
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            XmlReader reader = XmlReader.Create(webResponse.GetResponseStream());

            // The namespace manager is needed to handle XML prefixes in the results
            nsmgr = new XmlNamespaceManager(reader.NameTable);
            nsmgr.AddNamespace("mms", Properties.Resources.imageSchema);

            XPathDocument doc = new XPathDocument(reader);
            XPathNavigator nav = doc.CreateNavigator();

            // Check for errors
            XPathNodeIterator err = (XPathNodeIterator)nav.Evaluate("//Errors", nsmgr);
            if (err == null) return null; // TODO log this

            // Find the results
            XPathNodeIterator iter = (XPathNodeIterator)nav.Evaluate("//mms:ImageResult", nsmgr);
            if (iter == null) return null;

            // Return the search results
            List<Result> results = new List<Result>(10);
            while (iter.MoveNext())
                results.Add(createImageResultFromXml(iter));
            return results;
        }

        /// <summary>
        /// Create a <code>Result</code> object from the current result in the Bing XML
        /// response.
        /// </summary>
        /// <param name="iterator">An <code>XPathNodeIterator</code> pointing to a
        /// <code>web:WebResult</code> in Bing's response.</param>
        /// <param name="xmlNamespace">The XML namespace that the result tags use.</param>
        /// <param name="mapping">A Dictionary mapping the <code>DATATYPE</code>s above
        /// to XML element names (of the namespace <code>namespace</code> above) in the
        /// results.</param>
        /// <returns>A <code>Result</code> object with the read data.</returns>
        protected Result createResultFromXml(XPathNodeIterator iterator, string xmlNamespace,
            Dictionary<DATATYPES, string> mapping)
        {
            XPathNavigator iter = iterator.Current;
            // nav will be reused as a temporary throughout this section to handle
            // null results without throwing a NullReferenceException.  Pardon the ternaries...
            XPathNavigator nav;

            string title = null;
            if (mapping.ContainsKey(DATATYPES.TITLE))
            {
                nav = iter.SelectSingleNode(xmlNamespace + ":" + mapping[DATATYPES.TITLE], nsmgr);
                if (nav != null)
                    title = nav.InnerXml;
            }

            string description = null;
            if (mapping.ContainsKey(DATATYPES.DESCRIPTION))
            {
                nav = iter.SelectSingleNode(xmlNamespace + ":" + mapping[DATATYPES.DESCRIPTION], nsmgr);
                if (nav != null)
                    description = nav.InnerXml;
            }

            DateTime datetime = DateTime.UtcNow;
            if (mapping.ContainsKey(DATATYPES.DATETIME))
            {
                nav = iter.SelectSingleNode(xmlNamespace + ":" + mapping[DATATYPES.DATETIME], nsmgr);
                if (nav != null) 
                    datetime = DateTime.Parse(nav.InnerXml);
            }
            

            string url = null;
            if (mapping.ContainsKey(DATATYPES.URL))
            {
                nav = iter.SelectSingleNode(xmlNamespace + ":" + mapping[DATATYPES.URL], nsmgr);
                if (nav != null) 
                    url = nav.InnerXml;
            }
            
            //Console.Out.WriteLine(dt.ToString());
            return new Result(title, description, datetime, url);
        }

        private Result createImageResultFromXml(XPathNodeIterator iterator)
        {
            XPathNavigator iter = iterator.Current;
            // nav will be reused as a temporary throughout this section to handle
            // null results without throwing a NullReferenceException.  Pardon the ternaries...
            XPathNavigator nav = iter.SelectSingleNode("mms:Title", nsmgr);
            string title = (nav != null) ? nav.InnerXml : null;

            //nav = iter.SelectSingleNode("mms:Description", nsmgr);
            //string description = (nav != null) ? nav.InnerXml : null;
            string description = "picture";

            nav = iter.SelectSingleNode("mms:DateTime", nsmgr);
            DateTime datetime = (nav != null) ? DateTime.Parse(nav.InnerXml) : DateTime.UtcNow;
            nav = iter.SelectSingleNode("mms:MediaUrl", nsmgr);
            string url = (nav != null) ? nav.InnerXml : null;

            //Console.Out.WriteLine(dt.ToString());
            return new Result(title, description, datetime, url);
        }

        // testing main()
        static void Main(string[] args)
        {
            BingCrawler c = new BingWebCrawler();
            foreach (var item in c.findImage("cool+stuff"))
                Console.Out.WriteLine(item);
        }
    }
}