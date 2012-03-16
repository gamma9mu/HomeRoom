using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace HomeRoom
{
    public class RdfFile
    {
        private XElement rdfRoot;

        public RdfFile()
        {
            XNamespace rdfns = rdf.RDF_NS;
            XNamespace dcns = rdf.DC_NS;
            XNamespace mdns = rdf.MD_NS;
            rdfRoot = new XElement(rdfns + "RDF",
                new XAttribute(XNamespace.Xmlns + "rdf", rdfns.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "dc", dcns.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "md", mdns));
        }

        public void addDescription(XElement rdfDescription)
        {
            rdfRoot.Add(rdfDescription);
        }

        public void Save(StreamWriter stream)
        {
            rdfRoot.Save(stream);
        }
    }
}
