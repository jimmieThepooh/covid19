﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.EntityModel.SPResults
{
    public class RPT_COVID19_DAILY_FLU_RESULT
    {
        public Nullable<int> PERSON_TYPE_ID { get; set; }
        public string LABEL { get; set; }
        public Nullable<int> AMOUNT { get; set; }
    }
}
