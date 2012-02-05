using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace RDFGenerator
{
    public class RDFMetaData
    {
        public String Title { get; set; } // - Title
        public String Description { get; set; } // - A brief description
        public String Creator { get; set; } // Who wrote it
        //public String copyright_owner { get; set; } // 
        //public String copyright_year { get; set; } // 
        public String Format { get; set; } // - MIME type
        public String Type { get; set; } // - Genre
        public Uri Location { get; set; } // - For RDF:about
        public long Size { get; set; } // File size in bytes

        public void Write(String filename)
        {
            var file = File.Open(filename, FileMode.CreateNew, FileAccess.Write);
            Write(file);
        }

        public void Write(FileStream file)
        {
            if (!file.CanWrite)
            {
                throw new ArgumentException(rdf.RDFMetaData_Write_File_does_not_have_write_permissions_, "file");
            }
        }

        public String ToXml()
        {
            XNamespace rdfns = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";
            XNamespace dcns = "http://purl.org/dc/elements/1.1/";
            var root = new XElement(rdfns + "Description",
                new XAttribute(XNamespace.Xmlns + "rdf", rdfns.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "dc", dcns.NamespaceName),
                new XAttribute(rdfns + "about", Location.ToString()),
                new XElement(dcns + "Title", Title),
                new XElement(dcns + "Description", Description),
                new XElement(dcns + "Format", Format),
                new XElement(dcns + "Creator", Creator),
                new XElement(dcns + "Size", Size.ToString(CultureInfo.InvariantCulture)),
                new XElement(dcns + "Type", Type)
            );
            IEnumerable<XElement> empty = from el in root.Elements()
                        where (string)el == ""
                        select el;
            foreach (XElement emptyNode in empty)
            {
                emptyNode.Remove();
            }
            return root.ToString();
        }

        override public String ToString()
        {
            return ToXml();
        }

        public static void Main()
        {
            var r = new RDFMetaData
                        {
                            Creator = "Me",
                            Description = "",
                            Location = new Uri("http://www.google.com/"),
                            Title = "Googles",
                            Type = "Web Page",
                            Format = "text/html",
                            Size = 10034
                        };

            Console.Out.WriteLine(r.ToXml());
        }
    }
}
