﻿using System;
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

        public String Title { get; set; } // A name
        public String Creator { get; set; } // Primary "maker"
        public String Subject { get; set; } // Topic
        public String Description { get; set; } // A brief description
        public String Publisher { get; set; } // what it says

        // TODO should this be a list?
        public String Contributor { get; set; } // what it says
        public DateTime Date { get; set; } // A relavent date
        public String Type { get; set; } // Genre?
        public String Format { get; set; } // Format, eg. MIME type
        public Uri Identifier { get; set; } // Reference (used in rdf:RDF>rdf:about)
        public String Language { get; set; } // maybe a 2-char international code

        // TODO is this useful?
        public String Relation { get; set; } // Related item
        public String Coverage { get; set; } // Applicability
        public String Rights { get; set; } //  IP info
        public long Size { get; set; } // File size in bytes

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
