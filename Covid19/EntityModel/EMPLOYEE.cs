using System;
using System.Collections.Generic;

namespace Covid19.EntityModel
{
    public partial class EMPLOYEE
    {
        public string PERSON_ID { get; set; }
        public string FULLNAME { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public string ORGDESC { get; set; }
        public string JOBDESC { get; set; }
        public string EMAIL { get; set; }
        public string TEL { get; set; }
        public DateTime? STARTDATE { get; set; }
        public DateTime? ENDDATE { get; set; }
        public DateTime? CREATED_DT { get; set; }
    }
}
