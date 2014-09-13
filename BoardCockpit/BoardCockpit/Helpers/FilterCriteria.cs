using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardCockpit.Helpers
{
    public class FilterCriteria
    {
        #region Locals
        private int fromYear;
        private int toYear;        
        private int fromSizeClass;        
        private int toSizeClass;        
        private int industryNo;
        #endregion

        #region Properties
        public int FromYear
        {
            get { return fromYear; }
            set { fromYear = value; }
        }
        public int ToYear
        {
            get { return toYear; }
            set { toYear = value; }
        }
        public int FromSizeClass
        {
            get { return fromSizeClass; }
            set { fromSizeClass = value; }
        }
        public int ToSizeClass
        {
            get { return toSizeClass; }
            set { toSizeClass = value; }
        }
        public int IndustryNo
        {
            get { return industryNo; }
            set { industryNo = value; }
        }
        #endregion

        #region Constructor
        public FilterCriteria(int _fromYear, int _toYear, int _fromSizeClass, int _toSizeClass)
        {
            fromYear = _fromYear;
            toYear = _toYear;
            fromSizeClass = _fromSizeClass;
            toSizeClass = _toSizeClass;
        }
        public FilterCriteria() { }
        #endregion
    }
}