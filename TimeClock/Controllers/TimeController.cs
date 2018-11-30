using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using TimeClock.Custom;
using TimeClock.Mapping;
using TimeClock.Models;
using TimeClock_DAL;

namespace TimeClock.Controllers
{
    public class TimeController : Controller
    {
        public static List<string> Bootcampers = new List<string> { "ALeon26", "Josh", "Jodi", "Carlos", "Dana", "Fisher" };

        public static Dictionary<string, List<TimeEntry>> Times = new Dictionary<string, List<TimeEntry>>();

        private readonly TimeEntryDAO timeEntryDataAccess;
        private readonly UserDAO userDataAccess;
        public TimeController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            timeEntryDataAccess = new TimeEntryDAO(connectionString);
            userDataAccess = new UserDAO(connectionString);
        }
        
        [SecurityFilter("/Home/Index", "Role", "Instructor")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [SecurityFilter("/Home/Index", "Role", "Instructor")]
        public ActionResult TimeEntries()
        {
            ViewBag.Bootcampers = UserMapper.MapDoToList(userDataAccess.ViewUsers());
            return View();
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTime(Int64 userId)
        {
            ViewBag.Username = userDataAccess.ViewUserByUserId(userId).Username;
            List<IGrouping<int, TimeEntry>> usersTimes = TimeEntryMapper.MapDoToList(timeEntryDataAccess.ViewByUserId(userId)).GroupBy(x => x.Week).OrderBy(x => x.Key).ToList();
            
            //List<TimeEntry> usersTimes = new List<TimeEntry>();
            //Times.TryGetValue(username, out usersTimes);
            return PartialView("_UsersTimesTable", usersTimes);
        }
    }
}