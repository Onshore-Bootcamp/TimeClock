using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using TimeClock.Custom;
using TimeClock.Models;
using TimeClock_DAL;
using TimeClock_DAL.Models;

namespace TimeClock.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserDAO userDataAccess;
        private readonly TimeEntryDAO timeEntryDataAccess;

        public HomeController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            userDataAccess = new UserDAO(connectionString);
            timeEntryDataAccess = new TimeEntryDAO(connectionString);
        }

        public ActionResult Index()
        {
            return View();
        }

        Func<TimeEntry, bool> IsForToday = (entry) => 
        {
            return entry.TimeIn != default(DateTime) && 
                   entry.TimeIn.Day == DateTime.Now.Day && 
                   entry.TimeIn.Month == DateTime.Now.Month && 
                   entry.TimeIn.Year == entry.TimeIn.Year &&
                   entry.TimeOut == default(DateTime);
        };

        [HttpPost]
        [AjaxOnly]
        public ActionResult ClockIn(LoginForm form)
        {
            bool successful = false;
            string message = string.Empty;
            var userTimeEntries = new[] { new { TimeIn = "", TimeOut = ""} }.ToList();

            if (ModelState.IsValid)
            {
                UserDO user = userDataAccess.ViewUserByUsername(form.Username);
                if (user != null && user.Id != 0 && user.Password == form.Password)
                {
                    successful = true;
                    //Attempt to get todays entry, if none create one.

                    TimeEntryDO currentEntry = timeEntryDataAccess.ViewCurrentEntry(user.Id, DateTime.Now);

                    if (currentEntry.Id != 0)
                    {
                        currentEntry.TimeOut = DateTime.Now;
                        timeEntryDataAccess.Update(currentEntry);
                        message = "Successfully logged out";
                        //update.
                    }
                    else
                    {
                        currentEntry.UserId = user.Id;
                        currentEntry.TimeIn = DateTime.Now;
                        timeEntryDataAccess.Create(currentEntry);
                        message = "Successfully logged in";
                    }
                    userTimeEntries = timeEntryDataAccess.ViewByUserId(user.Id, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), DateTime.Now)
                        .Select(t => new { TimeIn = t.TimeIn.ToString("hh:mm:ss") + " " + t.TimeIn.ToString("tt", CultureInfo.InvariantCulture), TimeOut = t.TimeOut.ToString("hh:mm:ss") + " " + t.TimeOut.ToString("tt", CultureInfo.InvariantCulture)}).OrderByDescending(t => t.TimeIn).Take(4).ToList();
                    userTimeEntries.Reverse();
                }
                else
                {
                    successful = false;
                    message = "Username or password is incorrect.";
                }
            }
            else
            {
                successful = false;
                message = "Form not filled out properly.";
            }
            return Json(new { success = successful, message = message, timeEntries = userTimeEntries });
        }

        //[HttpPost]
        //public ActionResult ClockIn(LoginForm form)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        UserDO user = userDataAccess.ViewUserByUsername(form.Username);
        //        if(user != null && user.Id != 0 && user.Password == form.Password)
        //        {
        //            //Attempt to get todays entry, if none create one.

        //            TimeEntryDO currentEntry = timeEntryDataAccess.ViewCurrentEntry(user.Id, DateTime.Now);

        //            if(currentEntry.Id != 0)
        //            {
        //                currentEntry.TimeOut = DateTime.Now;
        //                timeEntryDataAccess.Update(currentEntry);
        //                //update.
        //            }
        //            else
        //            {
        //                currentEntry.UserId = user.Id;
        //                currentEntry.TimeIn = DateTime.Now;
        //                timeEntryDataAccess.Create(currentEntry);
        //            }
        //        }
        //    }
        //    return View("Index");
        //}
    }
}