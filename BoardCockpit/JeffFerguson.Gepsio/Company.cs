using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace JeffFerguson.Gepsio
{
    public class Company
    {
        private XmlNode thisContextNode;
        private XmlNode thisInstantPeriodNode;

        /// <summary>
        /// The ID of this company.
        /// </summary>
        public string Id
        {
            get;
            private set;
        }

        /// <summary>
        /// The identifier for this company.
        /// </summary>
        public string Identifier
        {
            get;
            private set;
        }

        /// <summary>
        /// The identifier scheme for this context.
        /// </summary>
        public string IdentifierScheme
        {
            get;
            private set;
        }

        /// <summary>
        /// The segment node defined for this context. If this context was not marked up with a segment node, then
        /// this property will return null.
        /// </summary>
        public XmlNode Segment
        {
            get;
            private set;
        }

        /// <summary>
        /// The scenario node defined for this context. If this context was not marked up with a scenario node, then
        /// this property will return null.
        /// </summary>
        public XmlNode Scenario
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

        public Int64 CompanyID
        {
            get;
            private set;
        }

        public int SizeClass
        {
            get;
            private set;
        }       

        public string Name
        {
            get;
            private set;
        }

        public string Location
        {
            get;
            private set;
        }

        public string Street
        {
            get;
            private set;
        }

        public int ZIPCode
        {
            get;
            private set;
        }

        public string City
        {
            get;
            private set;
        }

        public string Country
        {
            get;
            private set;
        }

        public ICollection<Industry> Industires
        {
            get;
            private set;
        }

        internal Company(XbrlFragment Fragment, IEnumerable<XmlElement> CompanyNodes)//)
        {
            this.Fragment = Fragment;
            this.Industires = new List<Industry>();
            //thisContextNode = ContextNode;
            //this.Id = thisContextNode.Attributes["id"].Value;
            //this.PeriodStartDate = System.DateTime.MinValue;
            //this.PeriodEndDate = System.DateTime.MinValue;
            //foreach (XmlNode CurrentChild in thisContextNode.ChildNodes)
            foreach (XmlNode CurrentChild in CompanyNodes)
            {
                switch (CurrentChild.LocalName) 
                {
                    case "genInfo.company.id":
                        CompanyID = Convert.ToInt64(CurrentChild.InnerText);
                        break;
                    case "genInfo.company.id.sizeClass":
                        SizeClass = Convert.ToInt32(CurrentChild.InnerText);
                        break;
                    case "genInfo.company.id.name":
                        Name = CurrentChild.InnerText;
                        break;
                    case "genInfo.company.id.location":
                        Location = CurrentChild.InnerText;
                        break;
                    case "genInfo.company.id.location.street":
                        Street = CurrentChild.InnerText;
                        break;
                    case "genInfo.company.id.location.zipCode":
                        ZIPCode = Convert.ToInt32(CurrentChild.InnerText);
                        break;
                    case "genInfo.company.id.location.city":
                        City = CurrentChild.InnerText;
                        break;
                    case "genInfo.company.id.location.country":
                        Country = CurrentChild.InnerText;
                        break;
                    case "genInfo.company.id.industry.keyType.industryKey.WZ2008":
                        if (!System.String.IsNullOrEmpty(CurrentChild.InnerText)) { 
                            int localId = Convert.ToInt32(CurrentChild.InnerText);
                            Industires.Add(new Industry
                                               {
                                                   ID = localId
                                               });
                        }
                        break;
                }
                
            }
        }
    }
}
