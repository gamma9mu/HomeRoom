using System;

namespace HomeRoom
{
    /// <summary>
    /// A storage class for returning search results.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// The URL of the resource.
        /// </summary>
        public Uri Url { get; private set; }

        /// <summary>
        /// The title of the resource.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Any available description of the resource.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The most-relevant date of the item.
        /// </summary>
        public DateTime Datetime { get; set; }

        /// <summary>
        /// For multimedia items, the height.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// For multimedia items, the width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The file size of the item.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// For multimedia items, the duration in seconds.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// The MIME type of the item.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Construct a new Result.
        /// </summary>
        /// <param name="url">The URL of the resource. This cannot be
        /// <code>null</code>.</param>
        public Result(string url)
        {
            if (url == null)
                throw new ArgumentNullException("url", "URL for a result must be set.");
            Url = new Uri(url);
        }

        public override string ToString()
        {
            return "      title: " + Title + "\ndescription: " + Description
                + "\n   datetime: " + Datetime + "\n        url: " + Url + "\n";
        }
    }
}