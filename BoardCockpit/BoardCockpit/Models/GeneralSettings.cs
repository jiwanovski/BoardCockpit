using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardCockpit.Models
{
    public class GeneralSetting
    {
        public int GeneralSettingID { get; set; }

        public int CompanyID { get; set; }

        public virtual Company Company { get; set; }
    }
}