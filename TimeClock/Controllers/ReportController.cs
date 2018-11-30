using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using TimeClock.Custom;
using TimeClock.Mapping;
using TimeClock.Models;
using TimeClock_DAL;

namespace TimeClock.Controllers
{
    public class ReportController : Controller
    {
        private readonly UserDAO userDataAccess;
        private readonly TimeEntryDAO timeEntryDataAccess;
        public ReportController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            timeEntryDataAccess = new TimeEntryDAO(connectionString);
            userDataAccess = new UserDAO(connectionString);
        }
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        [AjaxOnly]
        public JsonResult GetChartData()
        {
            List<TimeEntry> allEntries = TimeEntryMapper.MapDoToList(timeEntryDataAccess.ViewByUserId(1));

            List<float> averageLateness = new List<float>();

            int daysMoving = 10;

            for (int i = 0; i < allEntries.Count; i++)
            {
                if (i > daysMoving - 2)
                {
                    float localSum = 0;
                    for (int j = i - (daysMoving - 1); j <= i; j++)
                    {
                        localSum += (allEntries[j].TimeIn - new DateTime(allEntries[j].TimeIn.Year, allEntries[j].TimeIn.Month,
                            allEntries[j].TimeIn.Day, 8, 0, 0)).Minutes;
                    }
                    averageLateness.Add(localSum / daysMoving);
                }
            }
            return Json(PackageData(averageLateness), JsonRequestBehavior.AllowGet);
        }

        private List<object> PackageData(List<float> unpacked)
        {
            List<object> packed = new List<object>();
            foreach (float f in unpacked)
            {
                packed.Add(f);
            }
            return packed;
        }


    }
}