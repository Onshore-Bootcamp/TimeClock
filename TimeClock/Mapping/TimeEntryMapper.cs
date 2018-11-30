namespace TimeClock.Mapping
{
    using System;
    using System.Collections.Generic;
    using TimeClock.Models;
    using TimeClock_DAL.Models;

    public static class TimeEntryMapper
    {
        public static TimeEntry MapDoToSingle(TimeEntryDO from)
        {
            TimeEntry to = new TimeEntry();
            to.Id = from.Id;
            to.TimeIn = from.TimeIn;
            to.TimeOut = from.TimeOut;
            to.UserId = from.UserId;
            return to;
        }

        public static List<TimeEntry> MapDoToList(List<TimeEntryDO> from)
        {
            if (from == null)
                throw new ArgumentException();

            List<TimeEntry> to = new List<TimeEntry>();

            foreach (TimeEntryDO dataObject in from)
            {
                to.Add(TimeEntryMapper.MapDoToSingle(dataObject));
            }
            return to;
        }
    }
}