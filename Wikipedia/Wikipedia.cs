using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeRoom
{
    /// <summary>
    /// Factory class for Wikipedia searches.
    /// </summary>
    class Wikipedia : ISearchFactory
    {
        /// <summary>
        /// Get a WikipediaWebCrawler.
        /// </summary>
        /// <returns>A fresh WikipediaWebCrawler</returns>
        public ICrawler createWebCrawler()
        {
            return new WikipediaWebCrawler();
        }

        public ICrawler createImageCrawler()
        {
            return new NoopCrawler();
        }

        public ICrawler createAudioCrawler()
        {
            return new NoopCrawler();
        }

        public ICrawler createVideoCrawler()
        {
            return new NoopCrawler();
        }
    }
}
