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

        public RequestRunner(Request request)
        {
            System.Diagnostics.Debug.Assert(request != null);
            this.request = request;
        }

        public List<Result> retrieveItems()
        {
            preFilter();
            Bing bing = new Bing();
            ICrawler c = bing.createWebCrawler();
            List<Result> results = c.find(request.topic, webCount);

            c = bing.createImageCrawler();
            results.AddRange(c.find(request.topic, imageCount));

            c = bing.createVideoCrawler();
            results.AddRange(c.find(request.topic, videoCount));

            c = bing.createAudioCrawler();
            results.AddRange(c.find(request.topic, audioCount));

            return results;
        }

        private void preFilter()
        {
            webCount = request.studentInformation.visualPercent * RETRIEVAL_FACTOR;
            imageCount = request.studentInformation.visualPercent * RETRIEVAL_FACTOR;
            audioCount = request.studentInformation.auralPercent * RETRIEVAL_FACTOR;
            videoCount = request.studentInformation.visualPercent * RETRIEVAL_FACTOR;
            
            // scale video count by connection speed
            double speedFactor = request.connectionSpeed / 1500000;
            videoCount = (int) (videoCount * speedFactor);
        }
    }
}
