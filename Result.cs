using System;

namespace HomeRoom
{
    /// <summary>
    /// A storage class for passing Bing search results.
    /// </summary>
    public class Result
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime Datetime { get; private set; }
        public Uri Url { get; private set; }

        public Result(string title, string description, DateTime datetime, string url)
        {
            Title = title;
            Description = description;
            Datetime = datetime;
            if (url != null)
                Url = new Uri(url);
        }

        public override string ToString()
        {
            return "      title: " + Title + "\ndescription: " + Description
                + "\n   datetime: " + Datetime + "\n        url: " + Url + "\n";
        }
    }
}