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
        /// <summary>
        /// Lock for queuedRequests.
        /// </summary>
        private static Mutex queueLock = new Mutex();

        /// <summary>
        /// A queue of Request objects that need to be processed by the
        /// Controller.
        /// </summary>
        private static Queue<Request> queuedRequests = new Queue<Request>();
        
        /// <summary>
        /// The sole instance of the class.
        /// </summary>
        private static Controller singleton = new Controller();

        /// <summary>
        /// Singleton "constructor"
        /// </summary>
        /// <returns>The sole instance of Controller.</returns>
        public static Controller getInstance()
        {
            return singleton;
        }

        /// <summary>
        /// Create a controller.  If the background processing thread cannot be
        /// started, this will fail and throw an <code>Exception</code>.
        /// </summary>
        private Controller()
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
        /// <param name="state">ignored</param>
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

        /// <summary>
        /// Process a single <code>Request</code>.
        /// </summary>
        /// <param name="state">The <code>Request</code> to process.</param>
        static void processRequest(Object state)
        {
            Request request = (Request) state;
            if (request == null) return;

            List<Result> results = new RequestRunner(request).retrieveItems();

            RdfFile rdf = new RdfFile();
            foreach (Result r in results)
            {
                RDFMetaData rdfmd = new RDFMetaData();
                rdfmd.Subject = request.topic;
                rdfmd.Title = r.Title;
                rdfmd.Description = r.Description;
                rdfmd.Date = r.Datetime;
                rdfmd.Identifier = r.Url;
                rdfmd.Size = r.Size;
                rdfmd.Format = r.MimeType;
                //Length, Height, Width could be used, but Dublin Core doesn't care

                HttpMetaData httpmd = new HttpMetaData(rdfmd);
                if (httpmd.wasFound)
                    httpmd.addMetaData();

                rdf.addDescription(rdfmd.getDescription());
            }

            StreamWriter fw = new StreamWriter(ControllerConfig.ouputDirectory
                            + DateTime.UtcNow.ToFileTimeUtc().ToString()
                            + ControllerConfig.outputFileSuffix);
            fw.AutoFlush = true;
            rdf.Save(fw);
            fw.Close();
        }
    }
}
