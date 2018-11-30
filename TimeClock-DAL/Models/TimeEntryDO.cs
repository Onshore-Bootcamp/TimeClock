namespace TimeClock_DAL.Models
{
    using System;

    public class TimeEntryDO
    {
        public Int64 Id { get; set; }

        public DateTime TimeIn { get; set; }

        public DateTime TimeOut { get; set; }

        public virtual Int64 UserId { get; set; }
    }
}
