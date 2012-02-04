using System;
using System.IO;
using System.Xml;
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
            FileStream file = File.Open(filename, FileMode.CreateNew, FileAccess.Write);
            Write(file);
        }

        public void Write(FileStream file)
        {
            if (!file.CanWrite)
            {
                throw new ArgumentException(rdf.RDFMetaData_Write_File_does_not_have_write_permissions_, "file");
            }
        }

        public XmlDocument ToXml()
        {
            var xns = new XNamespace("http://www.w3.org/1999/02/22-rdf-syntax-ns#");
            var xml = new XmlDocument();
            var nsmgr = new XmlNamespaceManager(xml.NameTable);
            nsmgr.AddNamespace("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
            nsmgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
            
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", String.Empty, String.Empty));
            
            XmlElement rdf = xml.CreateElement("rdf:Description");
            XmlAttribute about = xml.CreateAttribute("rdf:about");
            about.InnerText = Location.ToString();
            rdf.Attributes.Append(about);
            
            if (Title != null)
            {
                XmlElement rdftag = xml.CreateElement("dc:Title");
                rdftag.InnerText = Title;
                rdf.AppendChild(rdftag);
            }

            if (Description != null)
            {
                XmlElement rdftag = xml.CreateElement("dc:Description");
                rdftag.InnerText = Description;
                rdf.AppendChild(rdftag);
            }

            if (Format != null)
            {
                XmlElement rdftag = xml.CreateElement("dc:Format");
                rdftag.InnerText = Format;
                rdf.AppendChild(rdftag);
            }

            if (Creator != null)
            {
                XmlElement rdftag = xml.CreateElement("dc:Creator");
                rdftag.InnerText = Creator;
                rdf.AppendChild(rdftag);
            }

            xml.AppendChild(rdf);
            return xml;
        }

        override public String ToString()
        {
            return ToXml().ToString();
        }

        public static void Main(String[] args)
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

            Console.Out.WriteLine(r.ToXml().OuterXml);
        }
    }
}
