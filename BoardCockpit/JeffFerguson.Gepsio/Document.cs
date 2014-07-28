using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace JeffFerguson.Gepsio
{
    public class Document
    {
        /// <summary>
        /// The ID of this document.
        /// </summary>
        public string Id
        {
            get;
            private set;
        }

        /// <summary>
        /// The identifier for this document.
        /// </summary>
        public string Identifier
        {
            get;
            private set;
        }

        /// <summary>
        /// The identifier scheme for this document.
        /// </summary>
        public string IdentifierScheme
        {
            get;
            private set;
        }

        /// <summary>
        /// A reference to the <see cref="XbrlFragment"/> in which this context is found.
        /// </summary>
        public XbrlFragment Fragment
        {
            get;
            private set;
        }

        public Int64 DocumentID
        {
            get;
            private set;
        }

        public System.DateTime GenerationDate
        {
            get;
            private set;
        }

        internal Document(XbrlFragment Fragment, IEnumerable<XmlElement> DocumentNodes)//)
        {
            this.Fragment = Fragment;
            
            //thisContextNode = ContextNode;
            //this.Id = thisContextNode.Attributes["id"].Value;
            //this.PeriodStartDate = System.DateTime.MinValue;
            //this.PeriodEndDate = System.DateTime.MinValue;
            //foreach (XmlNode CurrentChild in thisContextNode.ChildNodes)
            foreach (XmlNode CurrentChild in DocumentNodes)
            {
                switch (CurrentChild.LocalName) 
                {
                    case "genInfo.doc.id":
                        DocumentID = Convert.ToInt64(CurrentChild.InnerText);
                        break;
                    case "genInfo.doc.id.generationDate":
                        GenerationDate = Convert.ToDateTime(CurrentChild.InnerText);
                        break;                  
                }
                
            }
        }
    }
}
