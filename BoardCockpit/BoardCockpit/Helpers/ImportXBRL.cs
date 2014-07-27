using JeffFerguson.Gepsio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoardCockpit.Models;

namespace BoardCockpit.Helpers
{
    public class ImportXBRL
    {
        XbrlDocument Doc;

        public ImportXBRL()
        {
            Doc = new XbrlDocument();
        }
        public void Import(int importID, string filename)
        {
            Doc.Load(filename);
            foreach (XbrlFragment fragment in Doc.XbrlFragments)
            {
                foreach (Fact fact in fragment.Facts)
                {
                    // ImportNode node = new ImportNode()
                    // {
                    //     ImportID = importID,
                    //     Name = fact.Name
                    // };
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