using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeRoom
{
    /// <summary>
    /// Factory class for Bing searches.
    /// </summary>
    public class Bing : ISearchFactory
    {
        /// <summary>
        /// Get a BingWebCrawler.
        /// </summary>
        /// <returns>A fresh BingWebCrawler</returns>
        public ICrawler createWebCrawler()
        {
            return new BingWebCrawler();
        }

        /// <summary>
        /// Get a BingImageCrawler.
        /// </summary>
        /// <returns>A fresh BingImageCrawler</returns>
        public ICrawler createImageCrawler()
        {
            return new BingImageCrawler();
        }

        /// <summary>
        /// Get a BingAudioCrawler.
        /// </summary>
        /// <returns>A fresh BingAudioCrawler</returns>
        public ICrawler createAudioCrawler()
        {
            return new NoopCrawler();
        }

        /// <summary>
        /// Get a BingVideoCrawler.
        /// </summary>
        /// <returns>A fresh BingVideoCrawler</returns>
        public ICrawler createVideoCrawler()
        {
            return new BingVideoCrawler();
        }
    }
}
