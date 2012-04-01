using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeRoom
{
    class RequestRunner
    {
        private int RETRIEVAL_FACTOR = 3;

        private int webCount;
        private int imageCount;
        private int audioCount;
        private int videoCount;
        
        private Request request;

        /// <summary>
        /// Create a new request service object.
        /// </summary>
        /// <param name="request">The request to service.</param>
        public RequestRunner(Request request)
        {
            System.Diagnostics.Debug.Assert(request != null);
            this.request = request;
        }

        /// <summary>
        /// Use any available search providers to crawl for items.
        /// </summary>
        /// <returns>A list of any items that were found.</returns>
        public List<Result> retrieveItems()
        {
            preFilter();
            int expectedResultCount = webCount + imageCount + videoCount + audioCount;

            List<Result> results = new List<Result>(expectedResultCount);
            foreach (ISearchFactory factory
                in SearchFactoryRegistry.getInstance().getAllFactories())
            {
                results.AddRange(searchWith(factory));
            }

            return results;
        }

        /// <summary>
        /// Searches with a given ISearchFactory's crawlers.
        /// </summary>
        /// <param name="searchfactory">The search factory to use.</param>
        /// <returns>The combined results of the searches.</returns>
        private List<Result> searchWith(ISearchFactory searchfactory)
        {
            ICrawler c = searchfactory.createWebCrawler();
            List<Result> results = c.find(request.topic, webCount);

            c = searchfactory.createImageCrawler();
            results.AddRange(c.find(request.topic, imageCount));

            c = searchfactory.createVideoCrawler();
            results.AddRange(c.find(request.topic, videoCount));

            c = searchfactory.createAudioCrawler();
            results.AddRange(c.find(request.topic, audioCount));

            return results;
        }

        /// <summary>
        /// Weight the amount of each type of file to retrieve based on the
        /// user's learning style and connection speed.
        /// </summary>
        private void preFilter()
        {
            StudentInformation si = request.studentInformation;
            webCount = si.visualPercent * RETRIEVAL_FACTOR;
            imageCount = si.visualPercent * RETRIEVAL_FACTOR;
            audioCount = si.auralPercent * RETRIEVAL_FACTOR;
            videoCount = si.visualPercent * RETRIEVAL_FACTOR;
            
            // scale video count by connection speed
            double speedFactor = request.connectionSpeed / 1500000;
            videoCount = (int) (videoCount * speedFactor);
        }
    }
}
