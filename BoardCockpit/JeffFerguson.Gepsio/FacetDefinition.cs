﻿using System.Collections.Generic;

namespace JeffFerguson.Gepsio
{
	/// <summary>
	/// The definition of an XML schema facet defined in the http://www.w3.org/2001/XMLSchema namespace. 
	/// </summary>
	/// <remarks>
	/// <para>
	/// This class should be considered deprecated and will most likely be removed in a future version of Gepsio. In early CTPs,
	/// Gepsio implemented its own XML schema parser, and this class was created for the implementation of the XML schema parser
	/// type system. In later CTPs, Gepsio levergaed the XML schema support already available in the .NET Framework, which rendered
	/// Gepsio's XML schema type system obsolete.
	/// </para>
	/// </remarks>
    public class FacetDefinition
    {
		internal string Name
		{
			get;
			private set;
		}

		internal List<FacetPropertyDefinition> PropertyDefinitions
		{
			get;
			private set;
		}

        internal FacetDefinition(string FacetName)
        {
            this.Name = FacetName;
            this.PropertyDefinitions = new List<FacetPropertyDefinition>();
        }

        internal void AddFacetPropertyDefinition(FacetPropertyDefinition NewFacetPropertyDefinition)
        {
			this.PropertyDefinitions.Add(NewFacetPropertyDefinition);
        }
    }
}
