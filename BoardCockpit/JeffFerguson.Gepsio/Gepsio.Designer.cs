﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.34014
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JeffFerguson.Gepsio {
    using System;
    
    
    /// <summary>
    ///   Eine stark typisierte Ressourcenklasse zum Suchen von lokalisierten Zeichenfolgen usw.
    /// </summary>
    // Diese Klasse wurde von der StronglyTypedResourceBuilder automatisch generiert
    // -Klasse über ein Tool wie ResGen oder Visual Studio automatisch generiert.
    // Um einen Member hinzuzufügen oder zu entfernen, bearbeiten Sie die .ResX-Datei und führen dann ResGen
    // mit der /str-Option erneut aus, oder Sie erstellen Ihr VS-Projekt neu.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Gepsio {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Gepsio() {
        }
        
        /// <summary>
        ///   Gibt die zwischengespeicherte ResourceManager-Instanz zurück, die von dieser Klasse verwendet wird.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("JeffFerguson.Gepsio.Gepsio", typeof(Gepsio).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Überschreibt die CurrentUICulture-Eigenschaft des aktuellen Threads für alle
        ///   Ressourcenzuordnungen, die diese stark typisierte Ressourcenklasse verwenden.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Cannot find a &lt;context&gt; tag for contextRef &apos;{0}&apos;. ähnelt.
        /// </summary>
        internal static string CannotFindContextForContextRef {
            get {
                return ResourceManager.GetString("CannotFindContextForContextRef", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die The schema element for fact {0} cannot be found in schema {1}. ähnelt.
        /// </summary>
        internal static string CannotFindFactElementInSchema {
            get {
                return ResourceManager.GetString("CannotFindFactElementInSchema", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Cannot find a fact for element {0}. ähnelt.
        /// </summary>
        internal static string CannotFindFactForElement {
            get {
                return ResourceManager.GetString("CannotFindFactForElement", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Footnote arc {0} references fact {1}, but no fact with that name can be found. ähnelt.
        /// </summary>
        internal static string CannotFindFactForFootnoteArc {
            get {
                return ResourceManager.GetString("CannotFindFactForFootnoteArc", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Footnote arc {0} references footnote {1}, but no footnote with that name can be found. ähnelt.
        /// </summary>
        internal static string CannotFindFootnoteForFootnoteArc {
            get {
                return ResourceManager.GetString("CannotFindFootnoteForFootnoteArc", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Footnote arc {0} references a footnote locator with a label of {1}, but the locator cannot be found. ähnelt.
        /// </summary>
        internal static string CannotFindFootnoteLocator {
            get {
                return ResourceManager.GetString("CannotFindFootnoteLocator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Cannot find a &lt;unit&gt; tag for unitRef &apos;{0}&apos;. ähnelt.
        /// </summary>
        internal static string CannotFindUnitForUnitRef {
            get {
                return ResourceManager.GetString("CannotFindUnitForUnitRef", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Schema {0} defines fact element {1} with a duration-based period, but context {2}, used by the fact, does not implement a duration-based period. ähnelt.
        /// </summary>
        internal static string ElementSchemaDefinesDurationButUsedWithNonDurationContext {
            get {
                return ResourceManager.GetString("ElementSchemaDefinesDurationButUsedWithNonDurationContext", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Schema {0} defines fact element {1} with an instant-based period, but context {2}, used by the fact, does not implement an instant-based period. ähnelt.
        /// </summary>
        internal static string ElementSchemaDefinesInstantButUsedWithNonInstantContext {
            get {
                return ResourceManager.GetString("ElementSchemaDefinesInstantButUsedWithNonInstantContext", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Error loading XBRL document. ähnelt.
        /// </summary>
        internal static string ErrorLoadingXbrlDocument {
            get {
                return ResourceManager.GetString("ErrorLoadingXbrlDocument", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Facts named {0} are defined as being in an essence alias relationship with facts named {1}. However, the fact with ID {2} has a rounded value of {3}, which differs from the fact with ID {4}, which has a rounded value of {5}. These two facts are therefore not in a valid essence alias relationship. ähnelt.
        /// </summary>
        internal static string EssenceAliasFactsHaveDifferentRoundedValues {
            get {
                return ResourceManager.GetString("EssenceAliasFactsHaveDifferentRoundedValues", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Facts named {0} are defined as being in an essence alias relationship with facts named {1}. However, the fact with ID {2} is not context equal with the fact with ID {3}. These two facts are therefore not in a valid essence alias relationship. ähnelt.
        /// </summary>
        internal static string EssenceAliasFactsNotContextEquals {
            get {
                return ResourceManager.GetString("EssenceAliasFactsNotContextEquals", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Facts named {0} are defined as being in an essence alias relationship with facts named {1}. However, the fact with ID {2} is not parent equal with the fact with ID {3}. These two facts are therefore not in a valid essence alias relationship. ähnelt.
        /// </summary>
        internal static string EssenceAliasFactsNotParentEquals {
            get {
                return ResourceManager.GetString("EssenceAliasFactsNotParentEquals", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Facts named {0} are defined as being in an essence alias relationship with facts named {1}. However, the fact with ID {2} is not unit equal with the fact with ID {3}. These two facts are therefore not in a valid essence alias relationship. ähnelt.
        /// </summary>
        internal static string EssenceAliasFactsNotUnitEquals {
            get {
                return ResourceManager.GetString("EssenceAliasFactsNotUnitEquals", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Facet creation does not support creation of facets defined by type {0}. ähnelt.
        /// </summary>
        internal static string FacetDefinitionNotSupportedForFacetCreation {
            get {
                return ResourceManager.GetString("FacetDefinitionNotSupportedForFacetCreation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Footnote references fact {0} in external document {1}. Footnotes cannot reference facts in external documents. ähnelt.
        /// </summary>
        internal static string FootnoteReferencesFactInExternalDoc {
            get {
                return ResourceManager.GetString("FootnoteReferencesFactInExternalDoc", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Schema {0} contains invalid item type {1} on element {2}. ähnelt.
        /// </summary>
        internal static string InvalidElementItemType {
            get {
                return ResourceManager.GetString("InvalidElementItemType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Schema {0} contains invalid period type value {1} on element {2}. ähnelt.
        /// </summary>
        internal static string InvalidElementPeriodType {
            get {
                return ResourceManager.GetString("InvalidElementPeriodType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Schema {0} contains invalid substitution group value {1} on element {2}. ähnelt.
        /// </summary>
        internal static string InvalidElementSubstitutionGroup {
            get {
                return ResourceManager.GetString("InvalidElementSubstitutionGroup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Fact {0} is defined as a numeric item with a nil value. All numeric-based facts with a nil value must not specify either a precision attribute or a decimals atribute. The fact with ID {1} specifies one or both of these attributes. ähnelt.
        /// </summary>
        internal static string NilNumericFactWithSpecifiedPrecisionOrDecimals {
            get {
                return ResourceManager.GetString("NilNumericFactWithSpecifiedPrecisionOrDecimals", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Footnote references a location of {0}, but none of the facts uses that ID. ähnelt.
        /// </summary>
        internal static string NoFactForFootnoteReference {
            get {
                return ResourceManager.GetString("NoFactForFootnoteReference", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Footnote {0} does not include any language identifier information. Ensure that the footnote includes an xml:lang attribute. ähnelt.
        /// </summary>
        internal static string NoLangForFootnote {
            get {
                return ResourceManager.GetString("NoLangForFootnote", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die The current XBRL fragment does not have any associated schemas. ähnelt.
        /// </summary>
        internal static string NoSchemasForFragment {
            get {
                return ResourceManager.GetString("NoSchemasForFragment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Facts named {0} are defined as being in a requires-element relationship with facts named {1}. However, there are less instances of the {1} fact than of the {0} fact. A requires-element relationship mandates that there be one {1} fact instance for every {0} fact instance. ähnelt.
        /// </summary>
        internal static string NotEnoughToFactsInRequiresElementRelationship {
            get {
                return ResourceManager.GetString("NotEnoughToFactsInRequiresElementRelationship", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Fact {0} is defined as a numeric item. All numeric-based facts must specify either a precision attribute or a decimals atribute. The fact with ID {1} does not specify either a precision or a decimals attribute. ähnelt.
        /// </summary>
        internal static string NumericFactWithoutSpecifiedPrecisionOrDecimals {
            get {
                return ResourceManager.GetString("NumericFactWithoutSpecifiedPrecisionOrDecimals", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Fact {0} is defined as a numeric item. All numeric-based facts must specify either a precision attribute or a decimals atribute. The fact with ID {1} specifies both a precision and a decimals attribute. ähnelt.
        /// </summary>
        internal static string NumericFactWithSpecifiedPrecisionAndDecimals {
            get {
                return ResourceManager.GetString("NumericFactWithSpecifiedPrecisionAndDecimals", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Context ID {0} contains period information that specifies a start date that is later than the end date. ähnelt.
        /// </summary>
        internal static string PeriodEndDateLessThanPeriodStartDate {
            get {
                return ResourceManager.GetString("PeriodEndDateLessThanPeriodStartDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Fact {0} is defined as a pure item type. The fact also references a unit named {1}. The unit defines a measure referencing a local name of {2}. Local names for units of type pureItemType must be &quot;pure&quot;. ähnelt.
        /// </summary>
        internal static string PureItemTypeUnitLocalNameNotPure {
            get {
                return ResourceManager.GetString("PureItemTypeUnitLocalNameNotPure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Context ID {0} contains a node in its scenario structure named &lt;{1}&gt;. This node is defined in the schema at {2} with a substitution group setting that references the XBRL namespace. XBRL namespace substitution group references are not allowed in context scenarios. ähnelt.
        /// </summary>
        internal static string ScenarioNodeUsingSubGroupInXBRLNamespace {
            get {
                return ResourceManager.GetString("ScenarioNodeUsingSubGroupInXBRLNamespace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Context ID {0} contains a node in its scenario structure named &lt;{1}&gt;. This namespace for this node is the XBRL namespace. XBRL namespace node names are not allowed in context segments. ähnelt.
        /// </summary>
        internal static string ScenarioNodeUsingXBRLNamespace {
            get {
                return ResourceManager.GetString("ScenarioNodeUsingXBRLNamespace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die The file {0} at is referenced as a XBRL taxonomy schema. However, this file does not contain a root &lt;schema&gt; node and is not a valid XBRL taxonomy schema. ähnelt.
        /// </summary>
        internal static string SchemaFileCandidateDoesNotContainSchemaRootNode {
            get {
                return ResourceManager.GetString("SchemaFileCandidateDoesNotContainSchemaRootNode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Context ID {0} contains a node in its segment structure named &lt;{1}&gt;. This node is defined in the schema at {2} with a substitution group setting that references the XBRL namespace. XBRL namespace substitution group references are not allowed in context segments. ähnelt.
        /// </summary>
        internal static string SegmentNodeUsingSubGroupInXBRLNamespace {
            get {
                return ResourceManager.GetString("SegmentNodeUsingSubGroupInXBRLNamespace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Context ID {0} contains a node in its segment structure named &lt;{1}&gt;. This namespace for this node is the XBRL namespace. XBRL namespace node names are not allowed in context segments. ähnelt.
        /// </summary>
        internal static string SegmentNodeUsingXBRLNamespace {
            get {
                return ResourceManager.GetString("SegmentNodeUsingXBRLNamespace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Fact {0} is defined as a shares item type. The fact also references a unit named {1}. The unit defines a measure referencing a local name of {2}. Local names for units of type sharesItemType must be &quot;shares&quot;. ähnelt.
        /// </summary>
        internal static string SharesItemTypeUnitLocalNameNotShares {
            get {
                return ResourceManager.GetString("SharesItemTypeUnitLocalNameNotShares", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Fact {0} is based on an element that is named as the summation concept in a calculation link. The fact&apos;s value, after rounding, is {1}; however, the sum of the values of the contributing concepts, after rounding, is {2}. These values do not match; therefore, the rule specified by the fact&apos;s calculation link has been broken. ähnelt.
        /// </summary>
        internal static string SummationConceptError {
            get {
                return ResourceManager.GetString("SummationConceptError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Unit {0} is defined as a ratio. The ratio makes uses of a measure called {1} in both the numerator and the denominator. Ratios in units must not use the same measure in both the numerator and the denominator. ähnelt.
        /// </summary>
        internal static string UnitRatioUsesSameMeasureInNumeratorAndDenominator {
            get {
                return ResourceManager.GetString("UnitRatioUsesSameMeasureInNumeratorAndDenominator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die A facet named {0} was found in a schema and used for XML datatype {1}. This facet is not supported for this XML datatype. ähnelt.
        /// </summary>
        internal static string UnsupportedFacet {
            get {
                return ResourceManager.GetString("UnsupportedFacet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Facet property {0} is not valid for facet {1}. See http://www.w3.org/TR/xmlschema-2/ for more information. ähnelt.
        /// </summary>
        internal static string UnsupportedFacetProperty {
            get {
                return ResourceManager.GetString("UnsupportedFacetProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Fact {0} is defined as a monetary item type. The fact also references a unit named {1}. The unit defines a code of {2}, which is to be interpreted as an ISO 4217 currency code. This code is not found in the list of supported ISO 4217 currency codes and is invalid. ähnelt.
        /// </summary>
        internal static string UnsupportedISO4217CodeForUnitMeasure {
            get {
                return ResourceManager.GetString("UnsupportedISO4217CodeForUnitMeasure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Simple type {0} is unsupported as a restriction base type. ähnelt.
        /// </summary>
        internal static string UnsupportedRestrictionBaseSimpleType {
            get {
                return ResourceManager.GetString("UnsupportedRestrictionBaseSimpleType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die The XBRL schema at {0} could not be read due to error that occurred while accessing the network through a pluggable protocol. More information is available from the thrown exception&apos;s inner exception. ähnelt.
        /// </summary>
        internal static string WebExceptionThrownDuringSchemaCreation {
            get {
                return ResourceManager.GetString("WebExceptionThrownDuringSchemaCreation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Fact {0} is defined as a monetary item type. The fact also references a unit named {1}. The unit defines a measure referencing a namespace of {2}. This differs from the standard monetary unit namespace of http://www.xbrl.org/2003/iso4217. Monetary-based unit measures must reference the http://www.xbrl.org/2003/iso4217 namespace. ähnelt.
        /// </summary>
        internal static string WrongMeasureNamespaceForMonetaryFact {
            get {
                return ResourceManager.GetString("WrongMeasureNamespaceForMonetaryFact", resourceCulture);
            }
        }
    }
}
