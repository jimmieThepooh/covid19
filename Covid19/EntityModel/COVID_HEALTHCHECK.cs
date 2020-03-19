using System;
using System.Collections.Generic;

namespace Covid19.EntityModel
{
    public partial class COVID_HEALTHCHECK
    {
        public COVID_HEALTHCHECK()
        {
            COVID_HEALTHCHECK_COUNTRY = new HashSet<COVID_HEALTHCHECK_COUNTRY>();
            COVID_HEALTHCHECK_SYMTOMS = new HashSet<COVID_HEALTHCHECK_SYMTOMS>();
        }

        public int ID { get; set; }
        public bool? TRAVEL_IN_14_DAYS_FLAG { get; set; }
        public string TRAVEL_IN_14_DAYS_COUNTRY { get; set; }
        public bool? CLOSE_TO_FOREIGNER_FLAG { get; set; }
        public int? GROUP_OCCUPATION_ID { get; set; }
        public bool? CLOSE_TO_PATIENT_FLAG { get; set; }
        public bool? TRAVEL_IN_14_DAYS_OTHER_FLAG { get; set; }
        public string TRAVEL_IN_14_DAYS_COUNTRY_OTHER { get; set; }
        public bool? MEDICAL_STAFF_FLAG { get; set; }
        public int? AGE { get; set; }
        public int? AMPHUR_ID { get; set; }
        public int? PROVINCE_ID { get; set; }
        public bool? FRIENT_HAS_FLU_FLAG { get; set; }
        public int? LOCATION_ID { get; set; }
        public DateTime? CREATED_DT { get; set; }

        public virtual ICollection<COVID_HEALTHCHECK_COUNTRY> COVID_HEALTHCHECK_COUNTRY { get; set; }
        public virtual ICollection<COVID_HEALTHCHECK_SYMTOMS> COVID_HEALTHCHECK_SYMTOMS { get; set; }
    }
}
