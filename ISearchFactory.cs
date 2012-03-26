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
        static ICrawler createWebCrawler();
        static ICrawler createImageCrawler();
        static ICrawler createAudioCrawler();
        static ICrawler createVideoCrawler();
    }
}
