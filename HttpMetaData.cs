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

        public bool wasFound { get; private set; }

        public HttpMetaData(RDFMetaData metadata, HttpWebResponse headResponse = null)
        {
            this.metadata = metadata;
            this.headResponse = headResponse;
            this.wasFound = false;
        }

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
