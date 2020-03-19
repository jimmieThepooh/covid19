using Covid19.Core;
using Covid19.EntityModel;
using Covid19.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Collections;
using Covid19.EntityModel.SPResults;

namespace Covid19.Models
{

    public class RptCovid19ExBOModel
    {
        Covid19Context context;
        public RptCovid19ExBOModel(Covid19Context pcontext)
        {
            context = pcontext;
        }
        public RptCovid19ExViewModel Find(string id)
        {
            RptCovid19ExViewModel result = new RptCovid19ExViewModel();

            var emp = context.EMPLOYEE.Where(o => o.PERSON_ID == id).FirstOrDefault();

            result.PERSON_ID = id;
            result.FULLNAME = emp != null ? emp.FULLNAME : "-";
            result.ORGANIZATION = emp != null ? emp.ORGDESC : "-";

            int[] myArr = new int[0];
            var country = context.LUT_COVID_COUNTRY;
            var countryList = new List<string>();
            var countryList2 = new List<string>();
            var displayCountry = context.LUT_COVID_COUNTRY.Where(o => o.DISPLAY_FLAG == true);
            var query = context.RPT_COVID19EX.Where(o => o.PERSON_ID == id && o.REPORTER == 1).OrderByDescending(o => o.CREATED_DT).FirstOrDefault();
            if (query != null)
            {
                if (query.REPORTER.HasValue)
                {
                    Array.Resize(ref myArr, myArr.Length + 1);
                    myArr[myArr.Length - 1] = query.REPORTER.Value;
                }

                if (query.RPT_COVID19EX_COUNTRY != null && query.RPT_COVID19EX_COUNTRY.Count() > 0)
                {
                    int i = 0;
                    result.COUNTRY = new int[query.RPT_COVID19EX_COUNTRY.Count()];
                    foreach (var item in query.RPT_COVID19EX_COUNTRY)
                    {

                        bool isDisplay = displayCountry.Where(o => o.ID == item.COUNTRY_ID).Count() > 0;

                        if (isDisplay)
                        {
                            result.COUNTRY[i] = item.COUNTRY_ID.Value;
                        }
                        else
                        {
                            result.COUNTRY[i] = 999;
                            result.COUNTRY_ID = item.COUNTRY_ID;
                        }

                        var c = country.Find(item.COUNTRY_ID).COUNTRY_NAMT;
                        countryList.Add(c);

                        i++;
                    }
                }

                result.ID = query.ID;
                result.TRAVEL_REASON = query.TRAVEL_REASON;
                result.ARRIVAL_DT = query.ARRIVAL_DT;
                result.ARRIVAL_FLIGHT = query.ARRIVAL_FLIGHT;
                result.DEPARTURE_DT = query.DEPARTURE_DT;
                result.DEPARTURE_FLIGHT = query.DEPARTURE_FLIGHT;

                result.TRAVEL_REASON_TXT = context.LUT_COVID_CAUSE.Find(query.TRAVEL_REASON) != null ? context.LUT_COVID_CAUSE.Find(query.TRAVEL_REASON).DESCR : string.Empty;
                result.ARRIVAL_DT_TXT = DateTimeUtils.ConvertDateTimeToString(query.ARRIVAL_DT, "d MMM yyyy", "th");
                result.DEPARTURE_DT_TXT = DateTimeUtils.ConvertDateTimeToString(query.DEPARTURE_DT, "d MMM yyyy", "th");
                result.COUNTRY_TXT = string.Join(",", countryList);

                result.HISTORY = context.RPT_COVID19EX.Where(o => o.PERSON_ID == id && o.REPORTER == 1).OrderBy(o => o.CREATED_DT).ToList().Select(o => new RptCovid19ExViewModel()
                {
                    REPORT_DT_TXT = DateTimeUtils.ConvertDateTimeToString(o.REPORT_DT, "d MMM yyyy", "th"),
                    HAS_FLU = o.HAS_FLU,
                    SYMTOMS_TXT = string.Join(",", o.RPT_COVID19EX_SYMTOMS.Join(context.LUT_COVID_SYMTOM, c => c.SYMTOMS_ID, lc => lc.ID, (c, lb) => new { lb }).Select(x => x.lb.SYMTOM).ToList())
                }).ToList();
            }

            var query2 = context.RPT_COVID19EX.Where(o => o.PERSON_ID == id && o.REPORTER == 2).OrderByDescending(o => o.CREATED_DT).FirstOrDefault();
            if (query2 != null)
            {
                if (query2.REPORTER.HasValue)
                {
                    Array.Resize(ref myArr, myArr.Length + 1);
                    myArr[myArr.Length - 1] = query2.REPORTER.Value;
                }


                if (query2.RPT_COVID19EX_COUNTRY != null && query2.RPT_COVID19EX_COUNTRY.Count() > 0)
                {
                    int i = 0;
                    result.COUNTRY2 = new int[query2.RPT_COVID19EX_COUNTRY.Count()];
                    foreach (var item in query2.RPT_COVID19EX_COUNTRY)
                    {
                        bool isDisplay = displayCountry.Where(o => o.ID == item.COUNTRY_ID).Count() > 0;

                        if (isDisplay)
                        {
                            result.COUNTRY2[i] = item.COUNTRY_ID.Value;
                        }
                        else
                        {
                            result.COUNTRY2[i] = 999;
                            result.COUNTRY_ID2 = item.COUNTRY_ID;
                        }

                        var c = country.Find(item.COUNTRY_ID).COUNTRY_NAMT;
                        countryList2.Add(c);

                        i++;
                    }
                }

                result.TRAVEL_REASON2 = query2.TRAVEL_REASON;
                result.ARRIVAL_DT2 = query2.ARRIVAL_DT;
                result.ARRIVAL_FLIGHT2 = query2.ARRIVAL_FLIGHT;
                result.DEPARTURE_DT2 = query2.DEPARTURE_DT;
                result.DEPARTURE_FLIGHT2 = query2.DEPARTURE_FLIGHT;
                result.COMPANION_NAME1 = query2.COMPANION_NAME1;
                result.COMPANION_NAME2 = query2.COMPANION_NAME2;
                result.COMPANION_NAME3 = query2.COMPANION_NAME3;
                result.COMPANION_RELATION1 = query2.COMPANION_RELATION1;
                result.COMPANION_RELATION2 = query2.COMPANION_RELATION2;
                result.COMPANION_RELATION3 = query2.COMPANION_RELATION3;

                result.TRAVEL_REASON_TXT2 = context.LUT_COVID_CAUSE.Find(query2.TRAVEL_REASON).DESCR;
                result.ARRIVAL_DT_TXT2 = DateTimeUtils.ConvertDateTimeToString(query2.ARRIVAL_DT, "d MMM yyyy", "th");
                result.DEPARTURE_DT_TXT2 = DateTimeUtils.ConvertDateTimeToString(query2.DEPARTURE_DT, "d MMM yyyy", "th");
                result.COUNTRY_TXT2 = string.Join(",", countryList2);
                result.HISTORY2 = context.RPT_COVID19EX.Where(o => o.PERSON_ID == id && o.REPORTER == 2).OrderBy(o => o.CREATED_DT).ToList().Select(o => new RptCovid19ExViewModel()
                {
                    REPORT_DT_TXT = DateTimeUtils.ConvertDateTimeToString(o.REPORT_DT, "d MMM yyyy", "th"),
                    HAS_FLU = o.HAS_FLU,
                    SYMTOMS_TXT = string.Join(",", o.RPT_COVID19EX_SYMTOMS.Join(context.LUT_COVID_SYMTOM, c => c.SYMTOMS_ID, lc => lc.ID, (c, lb) => new { lb }).Select(x => x.lb.SYMTOM).ToList())
                }).ToList();

            }

            result.REPORTERS = new int[myArr.Length];
            result.REPORTERS = myArr;

            return result;
        }

        public EMPLOYEE GetProfileByID(string id)
        {
            return context.EMPLOYEE.Where(o => string.Compare(o.PERSON_ID, id) == 0).FirstOrDefault();
        }

        public void Create(string id)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    //create new row
                    var newRow = new RPT_COVID19EX();
                    newRow.PERSON_ID = id;
                    newRow.REPORTER = 1;
                    newRow.CREATED_DT = DateTime.Now;
                    newRow.TRAVEL_FLAG = false;
                    newRow.REPORT_DT = DateTime.Now;

                    context.RPT_COVID19EX.Add(newRow);
                    context.SaveChanges();

                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Create(RptCovid19ExViewModel ViewModel)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (ViewModel.REPORTERS != null && ViewModel.REPORTERS.Count() > 0)
                    {
                        foreach (var item in ViewModel.REPORTERS)
                        {
                            //create new row
                            var newRow = new RPT_COVID19EX();
                            newRow.PERSON_ID = ViewModel.PERSON_ID;
                            newRow.REPORTER = item;
                            newRow.TRAVEL_REASON = item == 1 ? ViewModel.TRAVEL_REASON : ViewModel.TRAVEL_REASON2;
                            newRow.ARRIVAL_DT = item == 1 ? ViewModel.ARRIVAL_DT : ViewModel.ARRIVAL_DT2;
                            newRow.ARRIVAL_FLIGHT = item == 1 ? ViewModel.ARRIVAL_FLIGHT : ViewModel.ARRIVAL_FLIGHT2;
                            newRow.DEPARTURE_DT = item == 1 ? ViewModel.DEPARTURE_DT : ViewModel.DEPARTURE_DT2;
                            newRow.DEPARTURE_FLIGHT = item == 1 ? ViewModel.DEPARTURE_FLIGHT : ViewModel.DEPARTURE_FLIGHT2;
                            newRow.HAS_FLU = ViewModel.HAS_FLU == "1" ? "มีไข้" : "ไม่มีไข้";
                            newRow.HAS_FLU_OTHER = ViewModel.HAS_FLU_OTHER == "1" ? "มีไข้" : "ไม่มีไข้";
                            newRow.TEMPERATURE = ViewModel.TEMPERATURE;
                            newRow.TEMPERATURE_OTHER = ViewModel.TEMPERATURE_OTHER;
                            newRow.REPORT_DT = ViewModel.REPORT_DT;
                            newRow.TRAVEL_TOGETHER = ViewModel.TRAVEL_TOGETHER;
                            newRow.CREATED_DT = DateTime.Now;
                            newRow.TRAVEL_FLAG = true;
                            newRow.COMPANION_NAME1 = ViewModel.COMPANION_NAME1;
                            newRow.COMPANION_NAME2 = ViewModel.COMPANION_NAME2;
                            newRow.COMPANION_NAME3 = ViewModel.COMPANION_NAME3;
                            newRow.COMPANION_RELATION1 = ViewModel.COMPANION_RELATION1;
                            newRow.COMPANION_RELATION2 = ViewModel.COMPANION_RELATION2;
                            newRow.COMPANION_RELATION3 = ViewModel.COMPANION_RELATION3;
                            context.RPT_COVID19EX.Add(newRow);
                            context.SaveChanges();

                            if (item == 1)
                            {
                                if (ViewModel.COUNTRY != null && ViewModel.COUNTRY.Count() > 0)
                                {
                                    foreach (var ct in ViewModel.COUNTRY)
                                    {
                                        var c = new RPT_COVID19EX_COUNTRY();
                                        c.REPORTER_ID = newRow.ID;
                                        c.PERSON_TYPE_ID = 1;
                                        c.COUNTRY_ID = ct != 999 ? ct : ViewModel.COUNTRY_ID;
                                        context.RPT_COVID19EX_COUNTRY.Add(c);
                                        context.SaveChanges();
                                    }
                                }

                                if (ViewModel.SYMTOMS != null && ViewModel.SYMTOMS.Count() > 0)
                                {
                                    foreach (var st in ViewModel.SYMTOMS)
                                    {
                                        var c = new RPT_COVID19EX_SYMTOMS();
                                        c.REPORTER_ID = newRow.ID;
                                        c.PERSON_TYPE_ID = 1;
                                        c.SYMTOMS_ID = st;
                                        context.RPT_COVID19EX_SYMTOMS.Add(c);
                                        context.SaveChanges();
                                    }
                                }
                                if (ViewModel.SYMTOMS_OTHER != null && ViewModel.SYMTOMS_OTHER.Count() > 0)
                                {
                                    foreach (var sto in ViewModel.SYMTOMS_OTHER)
                                    {
                                        var c = new RPT_COVID19EX_SYMTOMS_OTHER();
                                        c.REPORTER_ID = newRow.ID;
                                        c.PERSON_TYPE_ID = 1;
                                        c.SYMTOMS_ID = sto;
                                        context.RPT_COVID19EX_SYMTOMS_OTHER.Add(c);
                                        context.SaveChanges();
                                    }
                                }
                            }

                            if (item == 2)
                            {
                                if (ViewModel.COUNTRY2 != null && ViewModel.COUNTRY2.Count() > 0)
                                {
                                    foreach (var ct2 in ViewModel.COUNTRY2)
                                    {
                                        var c = new RPT_COVID19EX_COUNTRY();
                                        c.REPORTER_ID = newRow.ID;
                                        c.PERSON_TYPE_ID = 2;
                                        c.COUNTRY_ID = ct2 != 999 ? ct2 : ViewModel.COUNTRY_ID2;
                                        context.RPT_COVID19EX_COUNTRY.Add(c);
                                        context.SaveChanges();
                                    }
                                }

                                if (ViewModel.SYMTOMS2 != null && ViewModel.SYMTOMS2.Count() > 0)
                                {
                                    foreach (var st2 in ViewModel.SYMTOMS2)
                                    {
                                        var c = new RPT_COVID19EX_SYMTOMS();
                                        c.REPORTER_ID = newRow.ID;
                                        c.PERSON_TYPE_ID = 2;
                                        c.SYMTOMS_ID = st2;
                                        context.RPT_COVID19EX_SYMTOMS.Add(c);
                                        context.SaveChanges();
                                    }
                                }
                            }

                        }
                    }

                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }
        }

        public EMPLOYEE RegisterOutsource(MeaOutsourceViewModel ViewModel)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var row = context.EMPLOYEE.Find(ViewModel.PERSON_ID);

                    if (row == null)
                    {
                        var newRow = new EMPLOYEE();
                        newRow.PERSON_ID = ViewModel.PERSON_ID;
                        newRow.FIRSTNAME = ViewModel.FIRSTNAME;
                        newRow.LASTNAME = ViewModel.LASTNAME;
                        newRow.FULLNAME = ViewModel.FIRSTNAME + " " + ViewModel.LASTNAME;
                        newRow.ORGDESC = ViewModel.ORGDESC;
                        newRow.JOBDESC = ViewModel.JOBDESC;
                        newRow.TEL = ViewModel.TEL;
                        newRow.EMAIL = ViewModel.EMAIL;
                        newRow.CREATED_DT = DateTime.Now;

                        context.EMPLOYEE.Add(newRow);
                        context.SaveChanges();
                        dbContextTransaction.Commit();

                        return newRow;
                    }
                    else
                    {
                        return row;
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }
        }

        public IEnumerable RPT_COVID19_DAILY(int? type, DateTime? startDt, DateTime? endDt)
        {
            var reportDaily = context.RPT_COVID19_DAILY_RESULT
                                .FromSqlInterpolated($"RPT_COVID19_DAILY {type}, {startDt}, {endDt}")
                                .AsEnumerable()
                                .ToList();

            if (reportDaily != null && reportDaily.Count() > 0)
            {
                return reportDaily;
            }
            return Enumerable.Empty<RPT_COVID19_DAILY_RESULT>();
        }

        public IEnumerable RPT_COVID19_DAILY_COUNTRY(int? type, DateTime? startDt, DateTime? endDt)
        {
            var reportDaily = context.RPT_COVID19_DAILY_COUNTRY_RESULT
                                .FromSqlInterpolated($"RPT_COVID19_DAILY_COUNTRY {type}, {startDt}, {endDt}")
                                .AsEnumerable()
                                .ToList();

            if (reportDaily != null && reportDaily.Count() > 0)
            {
                return reportDaily;
            }
            return Enumerable.Empty<RPT_COVID19_DAILY_COUNTRY_RESULT>();
        }

        public IEnumerable RPT_COVID19_DAILY_FLU(int? type, DateTime? startDt, DateTime? endDt)
        {
            var reportDaily = context.RPT_COVID19_DAILY_FLU_RESULT
                                .FromSqlInterpolated($"RPT_COVID19_DAILY_FLU {type}, {startDt}, {endDt}")
                                .AsEnumerable()
                                .ToList();

            if (reportDaily != null && reportDaily.Count() > 0)
            {
                return reportDaily;
            }
            return Enumerable.Empty<RPT_COVID19_DAILY_FLU_RESULT>();
        }

        public IEnumerable RPT_COVID19_DAILY_SYMTOM(int? type, DateTime? startDt, DateTime? endDt)
        {
            var reportDaily = context.RPT_COVID19_DAILY_SYMTOM_RESULT
                                .FromSqlInterpolated($"RPT_COVID19_DAILY_SYMTOM {type}, {startDt}, {endDt}")
                                .AsEnumerable()
                                .ToList();

            if (reportDaily != null && reportDaily.Count() > 0)
            {
                return reportDaily;
            }
            return Enumerable.Empty<RPT_COVID19_DAILY_SYMTOM_RESULT>();
        }


    }
}
