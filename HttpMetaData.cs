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

        public HttpMetaData(RDFMetaData metadata, HttpWebResponse headResponse = null)
        {
            this.metadata = metadata;
            this.headResponse = headResponse;
        }

        public void addMetaData()
        {
            if (headResponse == null)
                retrieveHeadResponse();
            if (headResponse == null)
                return;

            if (headResponse.StatusCode == HttpStatusCode.OK)
            {
                if (headResponse.LastModified != null) metadata.Date = headResponse.LastModified;
                if (headResponse.ContentType != null) metadata.Format = headResponse.ContentType;
                if (headResponse.ContentLength > 0) metadata.Size = headResponse.ContentLength;
            }
        }

        private void retrieveHeadResponse()
        {
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
