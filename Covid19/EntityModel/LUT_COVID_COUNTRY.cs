using System;
using System.Collections.Generic;

namespace Covid19.EntityModel
{
    public partial class LUT_COVID_COUNTRY
    {
        public int ID { get; set; }
        public string COUNTRY_CODE { get; set; }
        public string COUNTRY_SH_CODE { get; set; }
        public string COUNTRY_NAME { get; set; }
        public string COUNTRY_NAMT { get; set; }
        public bool? DISPLAY_FLAG { get; set; }
    }
}
