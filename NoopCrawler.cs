using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeRoom
{
    /// <summary>
    /// A Crawler that does nothing.
    /// 
    /// This crawler can be used when a search provider (ISearchFactory)
    /// cannot provide a specific type of search.
    /// </summary>
    public class NoopCrawler : ICrawler
    {
        public List<Result> find(string query, int count)
        {
            return new List<Result>(0);
        }
    }
}
