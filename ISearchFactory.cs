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
        ICrawler createWebCrawler();
        ICrawler createImageCrawler();
        ICrawler createAudioCrawler();
        ICrawler createVideoCrawler();
    }
}
