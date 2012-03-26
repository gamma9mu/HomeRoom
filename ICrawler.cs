using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeRoom
{
    /// <summary>
    /// Provide a common interface for different content crawlers.
    /// </summary>
    interface ICrawler
    {
        public List<Result> find(string query);
    }
}
