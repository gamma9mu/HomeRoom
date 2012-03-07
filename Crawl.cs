using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

class Crawl
{
    /// sources and query are to be filled with input from heuristics gathering

    static string sources = "web";	/// What source material to search for?  Separate with a '+'
    static string query = "star wars";	/// Query string
    static string AppId = "THX11384EB";	/// AppId given by Bing;	
    static string[] urls = new string[50];	/// Holds urls returned by query

    static void Main(string[] args)
    {
        /// The generic url for querying
        string url = "http://api.bing.net/xml.aspx?Appid={0}&sources={1}&query={2}";

        /// Fill the generic url with AppId, list of sources, and query string
        string completeUri = String.Format(url, AppId, sources, query);

        /// Create a web request to get a web page
        HttpWebRequest webRequest = null;
        webRequest = (HttpWebRequest) WebRequest.Create(completeUri);

        /// Retrieve the xml search results and store them in a web response
        HttpWebResponse webResponse = null;
        webResponse = (HttpWebResponse) webRequest.GetResponse();

        /// Insert the xml results into an xmlReader for parsing
        XmlReader reader = null;
        reader = XmlReader.Create(webResponse.GetResponseStream());

        /// Parse results here...
        /// Very simple parsing implementation.  Only designed for web source results.
        int i = 0;
        while (reader.Read() && i == 0)
        {
            if (reader.IsStartElement())
            {
                if (reader.Name == "web:Results")
                {
                    i = 1;
                }
            }
        }

        /// Extract urls from xml results
        i = 0;
        while (reader.Read() && i < 50)
        {
            if (reader.Name == "web:URL")
            {
                if (reader.Read())
                {
                    urls[i++] = reader.Value.Trim();
                }
            }
        }

        /// To be implemented...
        /// Distribute url results to another program to be ranked
    }
}
