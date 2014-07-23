﻿using System.Xml;

namespace JeffFerguson.Gepsio
{
	/// <summary>
	/// An encapsulation of the locator functionality as defined in the http://www.w3.org/1999/xlink namespace. 
	/// </summary>
	public class Locator
	{
		/// <summary>
		/// The hyperlink reference defined by this locator.
		/// </summary>
		public string Href
		{
			get;
			private set;
		}

		/// <summary>
		/// The label of this locator.
		/// </summary>
		public string Label
		{
			get;
			private set;
		}

		/// <summary>
		/// The title of this locator.
		/// </summary>
		public string Title
		{
			get;
			private set;
		}

		/// <summary>
		/// The document URI for the locator's hyperlink reference.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Hyperlink references for locators may be specified with a fragment identifier,
		/// which has the form "url#id". If the locator's hyperlink reference includes a
		/// fragment identifier, then the document URI will contain the URL portion of the
		/// hyperlink reference. If the locator's hyperlink reference does not include a
		/// fragment identifier, then the document URI will match the hyperlink reference.
		/// </para>
		/// <list type="table">
		/// <listeader>
		/// <term>
		/// Href
		/// </term>
		/// <description>
		/// HrefDocumentUri
		/// </description>
		/// </listeader>
		/// <item>
		/// <term>
		/// http://www.xbrl.org/doc.xml#ElementID
		/// </term>
		/// <description>
		/// http://www.xbrl.org/doc.xml
		/// </description>
		/// </item>
		/// <item>
		/// <term>
		/// http://www.xbrl.org/doc.xml
		/// </term>
		/// <description>
		/// http://www.xbrl.org/doc.xml
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public string HrefDocumentUri
		{
			get;
			private set;
		}

		/// <summary>
		/// The resource ID for the locator's hyperlink reference.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Hyperlink references for locators may be specified with a fragment identifier,
		/// which has the form "url#id". If the locator's hyperlink reference includes a
		/// fragment identifier, then the resource ID will specify the identifer. If the
		/// locator's hyperlink reference does not include a fragment identifier, then the
		/// resource ID will be empty.
		/// </para>
		/// <list type="table">
		/// <listeader>
		/// <term>
		/// Href
		/// </term>
		/// <description>
		/// HrefResourceId
		/// </description>
		/// </listeader>
		/// <item>
		/// <term>
		/// http://www.xbrl.org/doc.xml#ElementID
		/// </term>
		/// <description>
		/// ElementID
		/// </description>
		/// </item>
		/// <item>
		/// <term>
		/// http://www.xbrl.org/doc.xml
		/// </term>
		/// <description>
		/// (empty string)
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public string HrefResourceId
		{
			get;
			private set;
		}

		//-------------------------------------------------------------------------------
		//-------------------------------------------------------------------------------
		internal Locator(XmlNode LocatorNode)
		{
			foreach (XmlAttribute CurrentAttribute in LocatorNode.Attributes)
			{
				if (CurrentAttribute.NamespaceURI.Equals("http://www.w3.org/1999/xlink") == false)
					continue;
				if (CurrentAttribute.LocalName.Equals("href") == true)
				{
					this.Href = CurrentAttribute.Value;
					ParseHref();
				}
				else if (CurrentAttribute.LocalName.Equals("label") == true)
					this.Label = CurrentAttribute.Value;
				else if (CurrentAttribute.LocalName.Equals("title") == true)
					this.Title = CurrentAttribute.Value;
			}
		}

		/// <summary>
		/// Compares a supplied href references with the href stored in the Locator.
		/// </summary>
		/// <remarks>
		/// This method describes a match on the href portion of the locator, not the ID
		/// portion.
		/// </remarks>
		/// <param name="HrefMatchCandidate">
		/// The hyperlink reference to compare against the locator.
		/// </param>
		/// <returns>
		/// Returns true if the supplied href references the same location as the href
		/// stored in the Locator, and false otherwise.
		/// </returns>
		internal bool HrefEquals(string HrefMatchCandidate)
		{
			if (HrefMatchCandidate.Length == 0)
				return true;
			if (this.HrefDocumentUri.Length < HrefMatchCandidate.Length)
				return HrefMatchCandidate.EndsWith(this.HrefDocumentUri);
			return this.Href.Equals(HrefMatchCandidate);
		}

		//-------------------------------------------------------------------------------
		//-------------------------------------------------------------------------------
		private void ParseHref()
		{
			string[] HrefSplit = this.Href.Split(new char[] { '#' });
			this.HrefDocumentUri = HrefSplit[0];
			if (HrefSplit.Length > 1)
				this.HrefResourceId = HrefSplit[1];
		}
	}
}
