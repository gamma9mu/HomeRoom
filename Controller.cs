﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;

namespace HomeRoom
{
    /// <summary>
    /// The <code>Controller</code> accepts <code>Request</code>s for processing
    /// and routes them through the crawling and tagging modules before writing
    /// the LV information for selection.
    /// </summary>
    public class Controller
    {
        private static Mutex queueLock = new Mutex();
        private static Queue<Request> queuedRequests = new Queue<Request>();

        /// <summary>
        /// Create a controller.  If the background processing thread cannot be
        /// started, this will fail and throw an <code>Exception</code>.
        /// </summary>
        public Controller()
        {
            if (!ThreadPool.QueueUserWorkItem(new WaitCallback(processQueue)))
            {
                throw new Exception("Cannot create Controller.");
            }
        }

        /// <summary>
        /// Add a request to the controller's processing queue.
        /// </summary>
        /// <param name="request"></param>
        public void addRequest(Request request)
        {
            Thread t = new Thread(processRequest);
            t.Start(request);
        }

        /// <summary>
        /// Add a request to the list of pending requests.
        /// </summary>
        /// <param name="request">The request to enqueue.</param>
        private void enqueueRequest(Request request)
        {
            queueLock.WaitOne();
            queuedRequests.Enqueue(request);
            queueLock.ReleaseMutex();
        }

        /// <summary>
        /// Attempt to enqueue all waiting requests.
        /// </summary>
        /// <param name="state"></param>
        static void processQueue(Object state)
        {
            Controller.queueLock.WaitOne();
            while (Controller.queuedRequests.Count != 0)
            {
                Request r = queuedRequests.Peek();
                if (ThreadPool.QueueUserWorkItem(new WaitCallback(processRequest), r))
                {
                    Controller.queuedRequests.Dequeue();
                }
            }
            Controller.queueLock.ReleaseMutex();
        }

        static void processRequest(Object state)
        {
            Request request = (Request) state;
            if (request == null) return;

            Crawler c = new Crawler();
            RdfFile rdf = new RdfFile();
            List<Result> results = c.find(request.topic);
            foreach (Result r in results)
            {
                RDFMetaData rdfmd = new RDFMetaData();
                rdfmd.Subject = request.topic;
                rdfmd.Title = r.Title;
                rdfmd.Description = r.Description;
                rdfmd.Date = r.Datetime;
                rdfmd.Identifier = r.Url;

                // Use header info from an HTTP HEAD request to fill in some more metadata
                HttpWebRequest req = (HttpWebRequest) WebRequest.Create(r.Url);
                req.Method = "HEAD";
                HttpWebResponse resp = (HttpWebResponse) req.GetResponse();
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    if (resp.LastModified != null) rdfmd.Date = resp.LastModified;
                    if (resp.ContentType != null) rdfmd.Format = resp.ContentType;
                    if (resp.ContentLength > 0) rdfmd.Size = resp.ContentLength;
                }

                rdf.addDescription(rdfmd.getDescription());
            }

            StreamWriter fw = new StreamWriter(controller.ouputDirectory
                            + DateTime.UtcNow.ToFileTimeUtc().ToString()
                            + controller.outputFileSuffix);
            fw.AutoFlush = true;
            rdf.Save(fw);
        }

        public static void Main(string[] args)
        {
            string topic = "jim jones";
            StudentInformation si = new StudentInformation(37, 46, 17);
            Request r = new Request(si, topic, 1500000);
            Controller c = new Controller();
            c.addRequest(r);
        }
    }
}
