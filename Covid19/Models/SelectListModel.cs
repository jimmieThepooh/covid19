using Covid19.EntityModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19.Models
{
    public class SelectListModel
    {

        Covid19Context context;
        public SelectListModel(Covid19Context pcontext)
        {
            context = pcontext;
        }

        public SelectList LUT_COVID_COUNTRY(bool display)
        {
            return new SelectList(context.LUT_COVID_COUNTRY.Where(o => o.DISPLAY_FLAG == display).OrderBy(x => x.COUNTRY_NAMT).ToList(), "ID", "COUNTRY_NAMT");
        }

        public SelectList LUT_COVID_REPORTER()
        {
            return new SelectList(context.LUT_COVID_REPORTER.ToList(), "ID", "REPORTER");
        }

        public SelectList LUT_COVID_SYMTOM()
        {
            return new SelectList(context.LUT_COVID_SYMTOM.ToList(), "ID", "SYMTOM");
        }

        public SelectList LUT_COVID_CAUSE()
        {
            return new SelectList(context.LUT_COVID_CAUSE.ToList(), "ID", "DESCR");
        }

        public SelectList LUT_COVID_RELATION()
        {
            return new SelectList(context.LUT_COVID_RELATION.ToList(), "DESCR", "DESCR");
        }
    }
}
