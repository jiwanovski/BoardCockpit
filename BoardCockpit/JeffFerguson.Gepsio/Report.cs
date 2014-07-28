using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace JeffFerguson.Gepsio
{
    public class Report
    {
        /// <summary>
        /// The ID of this report.
        /// </summary>
        public string Id
        {
            get;
            private set;
        }

        /// <summary>
        /// The identifier for this report.
        /// </summary>
        public string Identifier
        {
            get;
            private set;
        }

        /// <summary>
        /// The identifier scheme for this report.
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

        public Int64 ReportID
        {
            get;
            private set;
        }

        public System.DateTime AccordingToYearEnd
        {
            get;
            private set;
        }

        public string ReportType
        {
            get;
            private set;
        }

        internal Report(XbrlFragment Fragment, IEnumerable<XmlElement> ReportNodes)//)
        {
            this.Fragment = Fragment;
            
            //thisContextNode = ContextNode;
            //this.Id = thisContextNode.Attributes["id"].Value;
            //this.PeriodStartDate = System.DateTime.MinValue;
            //this.PeriodEndDate = System.DateTime.MinValue;
            //foreach (XmlNode CurrentChild in thisContextNode.ChildNodes)
            foreach (XmlNode CurrentChild in ReportNodes)
            {
                switch (CurrentChild.LocalName) 
                {
                    case "genInfo.report.id":
                        ReportID = Convert.ToInt64(CurrentChild.InnerText);
                        break;
                    case "genInfo.report.id.accordingTo.yearEnd":
                        AccordingToYearEnd = Convert.ToDateTime(CurrentChild.InnerText);
                        break;
                    case "genInfo.report.id.reportType":
                        ReportType = CurrentChild.InnerText;
                        break;                    
                }
                
            }
        }
    }
}
