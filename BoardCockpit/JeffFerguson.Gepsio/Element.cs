using System;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace JeffFerguson.Gepsio
{
	/// <summary>
	/// A definition of a fact as found in an XBRL schema.
	/// </summary>
    public class Element
    {
		/// <summary>
		/// The set of possible substitution groups for an element.
		/// </summary>
        public enum ElementSubstitutionGroup
        {
			/// <summary>
			/// An unknown substitution group.
			/// </summary>
            Unknown,
			/// <summary>
			/// An item substitution group.
			/// </summary>
            Item,
			/// <summary>
			/// A tuple substitution group.
			/// </summary>
            Tuple,
			/// <summary>
			/// A dimension item substitution group.
			/// </summary>
            DimensionItem,
			/// <summary>
			/// A hypercube item substitution group.
			/// </summary>
            HypercubeItem
        }

		/// <summary>
		/// The set of possible period types for an element.
		/// </summary>
        public enum ElementPeriodType
        {
			/// <summary>
			/// An unknown period type.
			/// </summary>
            Unknown,
			/// <summary>
			/// An instant period type.
			/// </summary>
            Instant,
			/// <summary>
			/// A duration period type.
			/// </summary>
            Duration
        }

        private XmlSchemaElement thisSchemaElement;

		/// <summary>
		/// The name of the element.
		/// </summary>
		public string Name
		{
			get;
			private set;
		}

		/// <summary>
		/// The ID of the element.
		/// </summary>
		public string Id
		{
			get;
			private set;
		}

		/// <summary>
		/// The substitution group for the element.
		/// </summary>
		public ElementSubstitutionGroup SubstitutionGroup
		{
			get;
			private set;
		}

		/// <summary>
		/// The period type for the element.
		/// </summary>
		public ElementPeriodType PeriodType
		{
			get;
			private set;
		}

		/// <summary>
		/// A reference to the schema which contains the element.
		/// </summary>
		public XbrlSchema Schema
		{
			get;
			private set;
		}

		/// <summary>
		/// The type name of this element.
		/// </summary>
		public XmlQualifiedName TypeName
		{
			get;
			private set;
		}

        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
        internal Element(XbrlSchema Schema, XmlNode ElementNode)
        {
            throw new NotSupportedException("no more hardcoded parsing of schema elements!");
        }

        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
        internal Element(XbrlSchema Schema, XmlSchemaElement SchemaElement)
        {
            this.Schema = Schema;
            thisSchemaElement = SchemaElement;
            this.Id = SchemaElement.Id;
            this.Name = SchemaElement.Name;
            this.TypeName = SchemaElement.SchemaTypeName;
            SetSubstitutionGroup(SchemaElement.SubstitutionGroup);
            SetPeriodType();
        }

		/// <summary>
		/// Determines whether or not a supplied object equals the current element.
		/// </summary>
		/// <param name="obj">
		/// The object to compare to the current element.
		/// </param>
		/// <returns>
		/// Returns true if the supplied object equals the current element. Returns false if
		/// the supplied object does not equal the current element.
		/// </returns>
        public override bool Equals(object obj)
        {
            if ((obj is Element) == false)
                return false;
            Element OtherElement = obj as Element;
            return OtherElement.Id.Equals(this.Id);
        }

		/// <summary>
		/// Calculates a hash code for the element.
		/// </summary>
		/// <returns>
		/// A hash code for the element.
		/// </returns>
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
        private void SetPeriodType()
        {
            string PeriodTypePrefix = this.Schema.GetPrefixForUri("http://www.xbrl.org/2003/instance");
            string AttributeName;
            if (PeriodTypePrefix == null)
                AttributeName = "periodType";
            else
                AttributeName = PeriodTypePrefix + ":periodType";
            this.PeriodType = ElementPeriodType.Unknown;
            if (thisSchemaElement.UnhandledAttributes != null)
            {
                foreach (XmlAttribute CurrentAttribute in thisSchemaElement.UnhandledAttributes)
                {
                    if (CurrentAttribute.Name.Equals(AttributeName) == true)
                    {
                        SetPeriodType(CurrentAttribute.Value);
                        return;
                    }
                }
            }
        }

        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
        private void SetPeriodType(string PeriodType)
        {
            this.PeriodType = ElementPeriodType.Unknown;
            if (PeriodType == "instant")
                this.PeriodType = ElementPeriodType.Instant;
            else if (PeriodType == "duration")
                this.PeriodType = ElementPeriodType.Duration;
            else
            {

                // We can't identify the type, so throw an exception.

                string MessageFormat = AssemblyResources.GetName("InvalidElementPeriodType");
                StringBuilder MessageFormatBuilder = new StringBuilder();
                MessageFormatBuilder.AppendFormat(MessageFormat, this.Schema.Path, PeriodType, this.Name);
                this.Schema.Fragment.AddValidationError(new ElementValidationError(this, MessageFormatBuilder.ToString()));
            }
        }

        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
        private void SetSubstitutionGroup(XmlQualifiedName SubstitutionGroupValue)
        {
            this.SubstitutionGroup = ElementSubstitutionGroup.Unknown;
            if ((SubstitutionGroupValue.Name.Length == 0) && (SubstitutionGroupValue.Namespace.Length == 0))
                return;
            if((SubstitutionGroupValue.Namespace.Equals("http://www.xbrl.org/2003/instance") == true) && (SubstitutionGroupValue.Name.Equals("item") == true))
                this.SubstitutionGroup = ElementSubstitutionGroup.Item;
            else if ((SubstitutionGroupValue.Namespace.Equals("http://www.xbrl.org/2003/instance") == true) && (SubstitutionGroupValue.Name.Equals("tuple") == true))
                this.SubstitutionGroup = ElementSubstitutionGroup.Tuple;
            else if ((SubstitutionGroupValue.Namespace.Equals("http://xbrl.org/2005/xbrldt") == true) && (SubstitutionGroupValue.Name.Equals("dimensionItem") == true))
                this.SubstitutionGroup = ElementSubstitutionGroup.DimensionItem;
            else if ((SubstitutionGroupValue.Namespace.Equals("http://xbrl.org/2005/xbrldt") == true) && (SubstitutionGroupValue.Name.Equals("hypercubeItem") == true))
                this.SubstitutionGroup = ElementSubstitutionGroup.HypercubeItem;
            else if ((SubstitutionGroupValue.Namespace.Equals("http://www.xbrl.de/de/fr/gaap/ci/2007-12-01") == true) && (SubstitutionGroupValue.Name.Equals("hbst.changeposition.item") == true))
                this.SubstitutionGroup = ElementSubstitutionGroup.Item;
            else
            {

                //// We can't identify the type, so throw an exception.

                //string MessageFormat = AssemblyResources.GetName("InvalidElementSubstitutionGroup");
                //StringBuilder MessageFormatBuilder = new StringBuilder();
                //MessageFormatBuilder.AppendFormat(MessageFormat, thisSchema.Path, SubstitutionGroupValue, thisName);
                //throw new XbrlException(MessageFormatBuilder.ToString());
            }
        }
    }
}
