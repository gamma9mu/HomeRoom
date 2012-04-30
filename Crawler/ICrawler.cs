using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeRoom
{
    /// <summary>
    /// Provide a common interface for different content crawlers.
    /// </summary>
    public interface ICrawler
    {
        /// <summary>
        /// Given a topic to search and an expected number of items to
        /// retrieve, perform a search.
        /// </summary>
        /// <param name="query">The topic to look for.</param>
        /// <param name="count">The expected number of items to return.</param>
        /// <returns>A <code>List&lt;Result&gt; of the items found.</code></returns>
        List<Result> find(string query, int count = 10);
    }
}
