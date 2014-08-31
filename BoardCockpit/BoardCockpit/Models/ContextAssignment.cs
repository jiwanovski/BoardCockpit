using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    //public enum ContextType
    //{
    //    BalanceSheet,IncomeStatement
    //}
    public class ContextAssignment
    {
        public int ContextAssignmentID { get; set; }
        //public ContextType Type { get; set; }

        public int ContextContainerID { get; set; }
        public int ContextID { get; set; }

        public virtual ContextContainer ContextContainer { get; set; }
        public virtual Context Context { get; set; }
    }
}