using JeffFerguson.Gepsio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoardCockpit.Models;
using System.Web.Mvc;
using BoardCockpit.DAL;

namespace BoardCockpit.Helpers
{
    public class ImportXBRL
    {
        private BoardCockpitContext db = new BoardCockpitContext();

        private XbrlDocument Doc;

        public JeffFerguson.Gepsio.Company Company
        {
            get
            {
                return Doc.XbrlFragments.First().Company;//Doc.XbrlFragment[1].Company;
            }
            
        }

        public Document Document
        {
            get
            {
                return Doc.XbrlFragments.First().GenInfoDocument;//Doc.XbrlFragment[1].Company;
            }
            
        }

        public XbrlDocument XbrlDocument
        {
            get { return Doc; }
        }

        public JeffFerguson.Gepsio.Report Report
        {
            get
            {
                return Doc.XbrlFragments.First().Report;//Doc.XbrlFragment[1].Company;
            }
            
        }

        public ICollection<JeffFerguson.Gepsio.Context> Contexts
        {
            get
            {
                return Doc.XbrlFragments.First().Contexts;
            }
        }

        public ICollection<JeffFerguson.Gepsio.Unit> Units
        {
            get
            {
                return Doc.XbrlFragments.First().Units;
            }
        }

        public ICollection<JeffFerguson.Gepsio.Fact> FinancialFacts
        {
            get
            {
                return Doc.XbrlFragments.First().FinancialFacts;
            }
        }

        public BoardCockpit.Models.Company DbCompanyUpdate(BoardCockpit.Models.Company existingCompany, ref bool companyExists, List<BoardCockpit.Models.Industry> Industries)
        {            
            // ----- COMPANY -------             
            BoardCockpit.Models.Company company;

            if (existingCompany != null)
            {
                companyExists = true;

                existingCompany.City = Company.City;
                existingCompany.Country = Company.Country;
                existingCompany.Location = Company.Location;
                existingCompany.Name = Company.Name;
                existingCompany.SizeClass = Company.SizeClass;
                existingCompany.Street = Company.Street;
                existingCompany.ZipCode = Company.ZIPCode;
                company = existingCompany;
            }
            else
            {
                company = new BoardCockpit.Models.Company
                {
                    CompanyIDXbrl = (int)Company.CompanyID,
                    Name = Company.Name,
                    Location = Company.Location,
                    Street = Company.Street,
                    ZipCode = Company.ZIPCode,
                    City = Company.City,
                    Country = Company.Country,
                    SizeClass = Company.SizeClass
                };

                company.Industies = new List<BoardCockpit.Models.Industry>();
            };

            foreach (JeffFerguson.Gepsio.Industry industry in Company.Industires)
            {
                BoardCockpit.Models.Industry industry2 = Industries.Where(n => n.IndustryKey == industry.ID).First();//new BoardCockpit.Models.Industry { IndustryID = industry.ID };

                if (industry2.Companies == null) { 
                    industry2.Companies = new List<BoardCockpit.Models.Company>();
                }
                
                industry2.Companies.Add(company);

                company.Industies.Add(industry2);
            }

            return company;
        }        

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

                    // set data
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