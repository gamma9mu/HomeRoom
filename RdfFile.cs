using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace HomeRoom
{
    /// <summary>
    /// Manages a single RDF file with multiple descriptions.
    /// </summary>
    public class RdfFile
    {
        private XElement rdfRoot;

        /// <summary>
        /// Setup the boilerplate RDF/XML.
        /// </summary>
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

        /// <summary>
        /// Add an RDF description element to the file.
        /// </summary>
        /// <param name="rdfDescription">The description element.</param>
        public void addDescription(XElement rdfDescription)
        {
            rdfRoot.Add(rdfDescription);
        }

        /// <summary>
        /// Write the entire RDF/XML file to a stream.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        public void Save(StreamWriter stream)
        {
            rdfRoot.Save(stream);
        }
    }
}
