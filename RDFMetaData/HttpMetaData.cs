using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace HomeRoom
{
    /// <summary>
    /// This class augments existing metadata by adding details gathered
    /// by an HTTP HEAD request.
    /// </summary>
    public class HttpMetaData
    {
        private HttpWebResponse headResponse;
        private RDFMetaData metadata;

        /// <summary>
        /// Does the resource exist?
        /// 
        /// This value is only valid after <code>addMetaData</code> has been
        /// called.
        /// </summary>
        public bool wasFound { get; private set; }

        /// <summary>
        /// Setup the HTTP HEAD retriever with a possibly existing HTTP
        /// response.
        /// </summary>
        /// <param name="metadata">The metadata to add to.</param>
        /// <param name="headResponse">An HTTP response, if one has already
        /// been received.</param>
        public HttpMetaData(RDFMetaData metadata, HttpWebResponse headResponse = null)
        {
            this.metadata = metadata;
            this.headResponse = headResponse;
            this.wasFound = false;
        }

        /// <summary>
        /// Add metadata to the known set from an HTTP read request.
        /// 
        /// If no request was passed to the constructor, one will be created here.
        /// </summary>
        public void addMetaData()
        {
            if (headResponse == null)
                retrieveHeadResponse();
            if (headResponse == null)
                return;

            if (headResponse.StatusCode == HttpStatusCode.OK)
            {
                wasFound = true;
                if (headResponse.LastModified != null) metadata.Date = headResponse.LastModified;
                if (headResponse.ContentType != null) metadata.Format = headResponse.ContentType;
                if (headResponse.ContentLength > 0) metadata.Size = headResponse.ContentLength;
            }
            else if (
              headResponse.StatusCode == HttpStatusCode.BadRequest ||
              headResponse.StatusCode == HttpStatusCode.Forbidden ||
              headResponse.StatusCode == HttpStatusCode.Gone ||
              headResponse.StatusCode == HttpStatusCode.NotFound ||
              headResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                metadata.clear();
            }
        }

        /// <summary>
        /// Performs the HTTP HEAD request.
        /// </summary>
        private void retrieveHeadResponse()
        {
            if (metadata.Identifier == null) return; // cannot lookup without an address
            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(metadata.Identifier);
            req.Method = "HEAD";

            try
            {
                headResponse = (HttpWebResponse) req.GetResponse();
            }
            catch (WebException we)
            {
                Console.Error.WriteLine("Error looking up: \"" + metadata.Identifier
                    + "\": " + we.Message);
            }
        }
    }
}
