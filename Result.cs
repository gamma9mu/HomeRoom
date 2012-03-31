using System;

namespace HomeRoom
{
    /// <summary>
    /// A storage class for passing Bing search results.
    /// </summary>
    public class Result
    {
        public Uri Url { get; private set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Datetime { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Size { get; set; }
        public int Length { get; set; }

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