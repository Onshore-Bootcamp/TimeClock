namespace TimeClock_DAL.Models
{
    using System;

    public class UserDO
    {
        public Int64 Id { get; set; }

        public Int64 LMSId { get; set; }

        public Int64 GroupId { get; set; }

        public Int64 CourseId { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public bool Active { get; set; }
    }
}
