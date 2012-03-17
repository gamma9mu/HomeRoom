using System;
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

            RdfFile rdf = new RdfFile();

            RequestRunner rr = new RequestRunner(request);
            List<Result> results = rr.retrieveItems();

            foreach (Result r in results)
            {
                RDFMetaData rdfmd = new RDFMetaData();
                rdfmd.Subject = request.topic;
                rdfmd.Title = r.Title;
                rdfmd.Description = r.Description;
                rdfmd.Date = r.Datetime;
                rdfmd.Identifier = r.Url;

                HttpMetaData httpmd = new HttpMetaData(rdfmd);
                httpmd.addMetaData();

                rdf.addDescription(rdfmd.getDescription());
            }

            StreamWriter fw = new StreamWriter(ControllerConfig.ouputDirectory
                            + DateTime.UtcNow.ToFileTimeUtc().ToString()
                            + ControllerConfig.outputFileSuffix);
            fw.AutoFlush = true;
            rdf.Save(fw);
        }

        public static void Main(string[] args)
        {
            string topic = "johnny carson";
            StudentInformation si = new StudentInformation(37, 46, 17);
            Request r = new Request(si, topic, 1500000);
            Controller c = new Controller();
            c.addRequest(r);
        }
    }
}
