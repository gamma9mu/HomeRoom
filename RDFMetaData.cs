using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace HomeRoom
{
    public class RDFMetaData
    {
        private bool cleared = false;

        /// <summary>
        /// A name for the item.
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// The primary creator of the item.
        /// </summary>
        public String Creator { get; set; }

        /// <summary>
        /// The item's topic.
        /// </summary>
        public String Subject { get; set; }

        /// <summary>
        /// A brief description of the item.
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// The item's publisher.
        /// </summary>
        public String Publisher { get; set; }

        // TODO should this be a list?
        /// <summary>
        /// Contributors to the item.
        /// </summary>
        public String Contributor { get; set; }

        /// <summary>
        /// A "relavent" date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The genre of the item?
        /// </summary>
        public String Type { get; set; }

        /// <summary>
        /// The item's format (MIME type).
        /// </summary>
        public String Format { get; set; }
        
        /// <summary>
        /// The item's URI.
        /// </summary>
        public Uri Identifier { get; set; }

        /// <summary>
        /// The language of the item in 2-char international code.
        /// </summary>
        public String Language { get; set; }

        // TODO is this useful?
        /// <summary>
        /// A related item.
        /// </summary>
        public String Relation { get; set; }

        /// <summary>
        /// What the item applies to.
        /// </summary>
        public String Coverage { get; set; }

        /// <summary>
        /// Intellectual property information of the item.
        /// </summary>
        public String Rights { get; set; }

        /// <summary>
        /// The item's file size in bytes.
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Obtain an XML representation of the metadata.
        /// </summary>
        /// <returns>An <code>XElement</code> created with the metadata mapped
        /// to Dublin Core RDF.</returns>
        public XElement getDescription()
        {
            if (cleared) return new XElement("");

            XNamespace rdfns = rdf.RDF_NS;
            XNamespace dcns = rdf.DC_NS;
            XNamespace mdns = rdf.MD_NS;
            var root = new XElement(
                rdfns + "Description",
                new XAttribute(rdfns + "about", (isValid()) ? Identifier.ToString() : ""),
                new XElement(dcns + "Title", Title),
                new XElement(dcns + "Description", Description),
                new XElement(dcns + "Format", Format),
                new XElement(dcns + "Creator", Creator),
                new XElement(dcns + "Language", Language),
                new XElement(dcns + "Relation", Relation),
                new XElement(dcns + "Coverage", Coverage),
                new XElement(dcns + "Subject", Subject),
                new XElement(dcns + "Publisher", Publisher),
                new XElement(dcns + "Contributer", Contributor),
                new XElement(dcns + "Rights", Rights),
                new XElement(dcns + "Date", Date.ToShortDateString()),
                new XElement(mdns + "Size", Size.ToString(CultureInfo.InvariantCulture)),
                new XElement(dcns + "Type", Type)
            );

            // Remove empty and Size==0 tags.
            root.Descendants().Where(x => x.IsEmpty).Remove();
            root.Descendants(mdns + "Size").Where(x => x.Value == "0").Remove();
            
            return root;
        }

        /// <summary>
        /// Clear the metadata set.
        ///
        /// This is useful if, for instance, attempting to retrieve the resource
        /// fails because it does not exist.  If this has been called, getDescription()
        /// will return an empty XElement.
        ///
        /// Individual properties on the object will remain accessible so that the
        /// application can attempt to recover the information.
        ///
        /// N.B.: THIS IS NOT REVERSIBLE!  Once this method has been called, it cannot
        /// be undone.
        /// </summary>
        public void clear()
        {
            cleared = true;
        }

        public bool isValid()
        {
            return !cleared && Identifier != null;
        }

        public override string ToString()
        {
            return getDescription().ToString();
        }
    }
}
