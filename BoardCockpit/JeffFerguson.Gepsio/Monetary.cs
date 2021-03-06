﻿using System;
using System.Text;
using System.Xml;

namespace JeffFerguson.Gepsio
{
	/// <summary>
	/// An encapsulation of the XML schema type "monetary" as defined in the http://www.w3.org/2001/XMLSchema namespace. 
	/// </summary>
	/// <remarks>
	/// <para>
	/// This class should be considered deprecated and will most likely be removed in a future version of Gepsio. In early CTPs,
	/// Gepsio implemented its own XML schema parser, and this class was created for the implementation of the XML schema parser
	/// type system. In later CTPs, Gepsio levergaed the XML schema support already available in the .NET Framework, which rendered
	/// Gepsio's XML schema type system obsolete.
	/// </para>
	/// </remarks>
    public class Monetary : Decimal
    {
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        internal Monetary(XmlNode StringRootNode)
            : base(StringRootNode)
        {
        }

        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        internal override void ValidateFact(Item FactToValidate)
        {
            base.ValidateFact(FactToValidate);

            Unit UnitReference = FactToValidate.UnitRef;
            if (UnitReference == null)
                return;
            if (UnitReference.MeasureQualifiedNames[0] == null)
                return;

            string Uri = UnitReference.MeasureQualifiedNames[0].NamespaceUri;
            if (Uri == null)
            {
                StringBuilder MessageBuilder = new StringBuilder();
                string StringFormat = AssemblyResources.GetName("WrongMeasureNamespaceForMonetaryFact");
                MessageBuilder.AppendFormat(StringFormat, FactToValidate.Name, UnitReference.Id, "unspecified");
                //throw new XbrlException(MessageBuilder.ToString());
            }

            if ((Uri.Length > 0) && (Uri.Equals("http://www.xbrl.org/2003/iso4217") == false))
            {
                StringBuilder MessageBuilder = new StringBuilder();
                string StringFormat = AssemblyResources.GetName("WrongMeasureNamespaceForMonetaryFact");
                MessageBuilder.AppendFormat(StringFormat, FactToValidate.Name, UnitReference.Id, UnitReference.MeasureQualifiedNames[0].NamespaceUri);
                //throw new XbrlException(MessageBuilder.ToString());
            }
            UnitReference.SetCultureAndRegionInfoFromISO4217Code(UnitReference.MeasureQualifiedNames[0].LocalName);
            if ((UnitReference.CultureInformation == null) && (UnitReference.RegionInformation == null))
            {
                StringBuilder MessageBuilder = new StringBuilder();
                string StringFormat = AssemblyResources.GetName("UnsupportedISO4217CodeForUnitMeasure");
                MessageBuilder.AppendFormat(StringFormat, FactToValidate.Name, UnitReference.Id, UnitReference.MeasureQualifiedNames[0].LocalName);
                //throw new XbrlException(MessageBuilder.ToString());
            }
        }
    }
}
