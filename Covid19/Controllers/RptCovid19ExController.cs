using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid19.Core;
using Covid19.EntityModel;
using Covid19.Models;
using Covid19.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Covid19.Controllers
{
    public class RptCovid19ExController : Controller
    {
        RptCovid19ExBOModel model;
        SelectListModel listModel;
        private const string ENC_KEY = "b14ca5898a4e4133bbce2ea2315a1915";


        public RptCovid19ExController(Covid19Context context)
        {
            model = new RptCovid19ExBOModel(context);
            listModel = new SelectListModel(context);
        }

        // GET: RptCovid19Ex
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction("Index");
            }

            MeaOutsourceViewModel viewModel = new MeaOutsourceViewModel();
            viewModel.PERSON_ID = id;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(MeaOutsourceViewModel ViewModel)
        {
            try
            {
                model.RegisterOutsource(ViewModel);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public ActionResult Intro(string id)
        {
            ViewBag.PERSON_ID = id;
            return View();
        }

        [HttpPost]
        public ActionResult IntroUpdate(string id)
        {
            try
            {
                model.Create(id);
                return Json(new { success = true, date = DateTimeUtils.ConvertDateTimeToString(DateTime.Now, "dd/MM/yyyy", "th") });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public ActionResult Form(string id)
        {
            RptCovid19ExViewModel viewModel = new RptCovid19ExViewModel();

            viewModel = model.Find(id);
            viewModel.REPORT_DT = DateTime.Now.Date;

            var emp = model.GetProfileByID(id);
            viewModel.FULLNAME = emp != null ? emp.FULLNAME : string.Empty;

            ViewBag.ListModel = listModel;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Form(RptCovid19ExViewModel ViewModel)
        {
            try
            {
                model.Create(ViewModel);
                return Json(new { success = true, covidID = ViewModel.PERSON_ID, date = DateTimeUtils.ConvertDateTimeToString(DateTime.Now, "dd/MM/yyyy", "th") });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult GetEmpProfile(string id)
        {
            var result = model.GetProfileByID(id);
            if (result != null)
            {
                return Json(new { success = true, data = result });
            }
            else
            {
                return Json(new { success = false, msg = "ไม่พบข้อมูลพนักงาน" });
            }
        }
    }
}