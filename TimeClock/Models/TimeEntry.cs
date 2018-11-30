namespace TimeClock.Models
{
    using System;
    using System.Globalization;

    public class TimeEntry
    {
        public TimeEntry()
        {
            TimeIn = default(DateTime);
            TimeOut = default(DateTime);
        }

        public Int64 Id { get; set; }

        public int Week { get { return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(TimeIn, CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday); } }

        public DateTime TimeIn { get; set; }

        public DateTime TimeOut { get; set; }

        public virtual Int64 UserId { get; set; }
    }
}