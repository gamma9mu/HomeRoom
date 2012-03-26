using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeRoom
{
    /// <summary>
    /// Factory class for Bing searches.
    /// </summary>
    public static class Bing : ISearchFactory
    {
        /// <summary>
        /// Get a BingWebCrawler.
        /// </summary>
        /// <returns>A fresh BingWebCrawler</returns>
        public static ICrawler createWebCrawler()
        {
            return new BingWebCrawler();
        }

        /// <summary>
        /// Get a BingImageCrawler.
        /// </summary>
        /// <returns>A fresh BingImageCrawler</returns>
        public static ICrawler createImageCrawler()
        {
            return new BingImageCrawler();
        }

        public static ICrawler createAudioCrawler()
        {
            throw new NotImplementedException("Coming soon...");
        }

        public static ICrawler createVideoCrawler()
        {
            throw new NotImplementedException("Coming soon...");
        }
    }
}
