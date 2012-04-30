using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeRoom
{
    /// <summary>
    /// Common interface for search factories.
    /// </summary>
    public interface ISearchFactory
    {
        /// <summary>
        /// Provide a web page crawler.
        /// </summary>
        /// <returns>An <code>ICrawler</code> specialized to retrieve web page
        /// items.</returns>
        ICrawler createWebCrawler();

        /// <summary>
        /// Provide a image crawler.
        /// </summary>
        /// <returns>An <code>ICrawler</code> specialized to retrieve image
        /// items.</returns>
        ICrawler createImageCrawler();

        /// <summary>
        /// Provide a audio crawler.
        /// </summary>
        /// <returns>An <code>ICrawler</code> specialized to retrieve audio
        /// items.</returns>
        ICrawler createAudioCrawler();

        /// <summary>
        /// Provide a video crawler.
        /// </summary>
        /// <returns>An <code>ICrawler</code> specialized to retrieve video
        /// items.</returns>
        ICrawler createVideoCrawler();
    }
}
