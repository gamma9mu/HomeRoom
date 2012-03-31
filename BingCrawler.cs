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
        /// <summary>
        /// The minimum value for the count parameter in a search as specified
        /// on MSDN.
        /// </summary>
        protected int COUNT_MIN = 1;

        /// <summary>
        /// The maximum value for the count parameter in a search as specified
        /// on MSDN.
        /// </summary>
        protected int COUNT_MAX = 50;

        /// <summary>
        /// The minimum value for the offset parameter in a search as specified
        /// on MSDN.
        /// </summary>
        protected int OFFSET_MIN = 0;

        /// <summary>
        /// The maximum value for the offset parameter in a search as specified
        /// on MSDN.
        /// </summary>
        protected int OFFSET_MAX = 1000;

        /// XML namespace lookup aide.
        protected XmlNamespaceManager nsmgr = null;

        /// <summary>
        /// An enumeration of the different data elements that can be found in
        /// a result set.
        /// </summary>
        protected enum DATATYPES { TITLE, DESCRIPTION, DATETIME, URL, LENGTH,
            SIZE, HEIGHT, WIDTH };

        /// <summary>
        /// Search Bing for resources of a given type.  Subclasses specify the
        /// actual type of resource to retrieve.
        /// </summary>
        /// <param name="query">A search query word or phrase to use as the keywords in the
        /// web search.</param>
        /// <param name="count">How many items to retrieve.</param>
        /// <returns>A <code>List&lt;Result&gt;</code> of the results found by Bing.</returns>
        public List<Result> find(string query, int count)
        {
            int start = 0;
            List<Result> results = new List<Result>(count);

            while (count > 0)
            {
                Console.Out.WriteLine(
                    String.Format("Retrieving at {0} # {1} by {2}", start, count, this.ToString()));
                int found = obtainResultRange(query, start, count, results);
                Console.Out.WriteLine(String.Format("Got {0}", found));
                if (found == 0) break; // No more results to be had
                start += found;
                count -= found;
            }

            return results;
        }

        /// <summary>
        /// Retrieve the Bing results for a search with indices from offset to
        /// offset+count.
        /// </summary>
        /// <param name="query">The search terms.</param>
        /// <param name="offset">The starting offset into the results.</param>
        /// <param name="count">The number of results to retrieve.</param>
        /// <param name="results">A list to store the results into.</param>
        /// <returns>The actual number of items found.</returns>
        abstract protected int obtainResultRange(string query, int offset, int count,
            List<Result> results);

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

            nav = iter.SelectSingleNode(xmlNamespace + ":" + mapping[DATATYPES.URL], nsmgr);
            if (nav == null) return null;
            string url = nav.InnerXml;
            Result result = new Result(url);
            
            if (mapping.ContainsKey(DATATYPES.TITLE))
            {
                nav = iter.SelectSingleNode(xmlNamespace + ":" + mapping[DATATYPES.TITLE], nsmgr);
                if (nav != null)
                    result.Title = nav.InnerXml;
            }

            if (mapping.ContainsKey(DATATYPES.DESCRIPTION))
            {
                nav = iter.SelectSingleNode(xmlNamespace + ":" + mapping[DATATYPES.DESCRIPTION], nsmgr);
                if (nav != null)
                    result.Description = nav.InnerXml;
            }

            
            if (mapping.ContainsKey(DATATYPES.DATETIME))
            {
                nav = iter.SelectSingleNode(xmlNamespace + ":" + mapping[DATATYPES.DATETIME], nsmgr);
                if (nav != null) 
                    result.Datetime = DateTime.Parse(nav.InnerXml);
            }
            
            if (mapping.ContainsKey(DATATYPES.LENGTH))
            {
                nav = iter.SelectSingleNode(xmlNamespace + ":" + mapping[DATATYPES.LENGTH], nsmgr);
                if (nav != null) 
                    result.Length = Int32.Parse(nav.InnerXml);
            }

            if (mapping.ContainsKey(DATATYPES.SIZE))
            {
                nav = iter.SelectSingleNode(xmlNamespace + ":" + mapping[DATATYPES.SIZE], nsmgr);
                if (nav != null) 
                    result.Size = Int32.Parse(nav.InnerXml);
            }

            if (mapping.ContainsKey(DATATYPES.HEIGHT))
            {
                nav = iter.SelectSingleNode(xmlNamespace + ":" + mapping[DATATYPES.HEIGHT], nsmgr);
                if (nav != null) 
                    result.Height = Int32.Parse(nav.InnerXml);
            }

            if (mapping.ContainsKey(DATATYPES.WIDTH))
            {
                nav = iter.SelectSingleNode(xmlNamespace + ":" + mapping[DATATYPES.WIDTH], nsmgr);
                if (nav != null) 
                    result.Width = Int32.Parse(nav.InnerXml);
            }
            
            return result;
        }
    }
}