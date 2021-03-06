using System;
using System.Xml;

namespace JeffFerguson.Gepsio
{
	/// <summary>
	/// The encapsulation of a fact defined in an XBRL document. A fact is an occurrence in an instance
	/// document of a value or other information tagged by a taxonomy element.
	/// </summary>
    public class Fact
    {
        internal XbrlFragment thisParentFragment;
        // JIW Start
        // internal XmlNode thisFactNode; // was protected
        public XmlNode thisFactNode { get; set; }
        // JIW Stop
		/// <summary>
		/// The name of the fact.
		/// </summary>
		public string Name
		{
			get;
			private set;
		}

        internal Fact(XbrlFragment ParentFragment, XmlNode FactNode)
        {
            thisParentFragment = ParentFragment;
            thisFactNode = FactNode;
            this.Name = thisFactNode.LocalName;
        }

        internal static Fact Create(XbrlFragment ParentFragment, XmlNode FactNode)
        {
            Fact FactToReturn = null;

            if ((IsXbrlNamespace(FactNode.NamespaceURI) == false)
                && (IsW3Namespace(FactNode.NamespaceURI) == false)
                && (FactNode.NodeType != XmlNodeType.Comment))
            {

                // This item could be a fact, or it could be a tuple. Examine the schemas
                // to find out what we're dealing with.

                Element MatchingElement = null;
                foreach (var CurrentSchema in ParentFragment.Schemas)
                {
                    var FoundElement = CurrentSchema.GetElement(FactNode.LocalName);
                    if (FoundElement != null)
                    {
                        MatchingElement = FoundElement;
                    }
                }
                if (MatchingElement != null)
                {
                    switch (MatchingElement.SubstitutionGroup)
                    {
                        case Element.ElementSubstitutionGroup.Item:
                            FactToReturn = new Item(ParentFragment, FactNode);
                            break;
                        case Element.ElementSubstitutionGroup.Tuple:
                            FactToReturn = new Tuple(ParentFragment, FactNode);
                            break;
                        default:
                            // This type is unknown, so leave it alone.
                            break;
                    }
                }
            }
            return FactToReturn;
        }

        /// <summary>
        /// Determines whether or not a namespace URI is hosted by the www.xbrl.org domain.
        /// </summary>
        /// <param name="CandidateNamespace">
        /// A namespace URI.
        /// </param>
        /// <returns>
        /// True if the namespace URI is hosted by the www.xbrl.org domain; false otherwise.
        /// </returns>
        private static bool IsXbrlNamespace(string CandidateNamespace)
        {
            return NamespaceMatchesUri(CandidateNamespace, "www.xbrl.org");
        }

        /// <summary>
        /// Determines whether or not a namespace URI is hosted by the www.w3.org domain.
        /// </summary>
        /// <param name="CandidateNamespace">
        /// A namespace URI.
        /// </param>
        /// <returns>
        /// True if the namespace URI is hosted by the www.w3.org domain; false otherwise.
        /// </returns>
        private static bool IsW3Namespace(string CandidateNamespace)
        {
            return NamespaceMatchesUri(CandidateNamespace, "www.w3.org");
        }

        private static bool NamespaceMatchesUri(string CandidateNamespace, string Uri)
        {
            CandidateNamespace = CandidateNamespace.Trim();
            if (CandidateNamespace.Length == 0)
                return false;
            Uri NamespaceUri = new Uri(CandidateNamespace);
            return NamespaceUri.Host.ToLower().Equals(Uri);
        }
    }
}
