using JeffFerguson.Gepsio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoardCockpit.Models;
using System.Web.Mvc;

namespace BoardCockpit.Helpers
{
    public class ImportXBRL
    {
        XbrlDocument Doc;

        public ImportXBRL()
        {
            Doc = new XbrlDocument();
        }
        public void Import(Import import, ref List<ImportNode> nodes, string filename, string schemaDirectory)
        {
            Doc.DefinitionDirectory = schemaDirectory;
            Doc.Load(filename);
            foreach (XbrlFragment fragment in Doc.XbrlFragments)
            {
                foreach (var fact in fragment.Facts)
                {
                    ImportNode node = null;

                    if (fact is Item) { 
                        node = new ImportNode()
                        {
                            ImportID = import.ImportID,
                            Import = import,
                            ContextRef = ((JeffFerguson.Gepsio.Item)(fact)).ContextRefName,
                            UnitRef = ((JeffFerguson.Gepsio.Item)(fact)).UnitRefName,
                            Precision = ((JeffFerguson.Gepsio.Item)(fact)).Precision,
                            Name = fact.Name,
                            Value = ((JeffFerguson.Gepsio.Item)(fact)).Value
                        };
                    };

                    if (fact is JeffFerguson.Gepsio.Tuple) {
                        JeffFerguson.Gepsio.Tuple tuple = (JeffFerguson.Gepsio.Tuple)fact;
                        JeffFerguson.Gepsio.Fact fact2 = (JeffFerguson.Gepsio.Fact)fact;
                        
                        
                        //string test = (tuple.thisFactNode).Attributes.First().Value;
                        node = new ImportNode()
                        {
                            ImportID = import.ImportID,
                            Import = import,
                            ContextRef = fact2.thisFactNode.Attributes[0].Value,  // .((System.Xml.XmlAttribute)(((System.Xml.XmlNamedNodeMap)((((JeffFerguson.Gepsio.Fact)(tuple)).thisFactNode).Attributes)).nodes.field)).Value,
                            Name = fact2.thisFactNode.LocalName, // (((JeffFerguson.Gepsio.Fact)(tuple)).thisFactNode).LocalName,
                            Value = fact2.thisFactNode.InnerText // (((JeffFerguson.Gepsio.Fact)(tuple)).thisFactNode).InnerText
                        };
                    }

                    nodes.Add(node);

                    // TODO In Tabelle ImportNode speichern --> Methode in ImportNodesController
                }
            }
        }

        private string GetValue(Fact fact)
        {
            string returnValue = "";

            if (fact is Item)
            {
                returnValue = ((JeffFerguson.Gepsio.Item)(fact)).Value;
            }
            return returnValue;
        }
    }
}