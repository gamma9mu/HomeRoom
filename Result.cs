﻿using System;

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
        public string Url { get; private set; }

        public Result(string title, string description, DateTime datetime, string url)
        {
            Title = title;
            Description = description;
            Datetime = datetime;
            Url = url;
        }

        public override string ToString()
        {
            return "      title: " + Title + "\ndescription: " + Description
                + "\n   datetime: " + Datetime + "\n        url: " + Url + "\n";
        }
    }
}