namespace TimeClock.Mapping
{
    using System;
    using System.Collections.Generic;
    using TimeClock.Models;
    using TimeClock_DAL.Models;

    public static class UserMapper
    {
        public static User MapDoToSingle(UserDO from)
        {
            User to = new User();
            to.Id = from.Id;
            to.LMSId = from.LMSId;
            to.Username = from.Username;
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;
            to.Password = from.Password;
            to.Role = from.Role;
            to.Active = from.Active;

            to.GroupId = from.GroupId;
            to.CourseId = from.CourseId;
            return to;
        }

        public static UserDO MapPoToSingle(User from)
        {
            UserDO to = new UserDO();
            to.Id = from.Id;
            to.LMSId = from.LMSId;
            to.Username = from.Username;
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;
            to.Password = from.Password;
            to.Role = from.Role;
            to.Active = from.Active;

            to.GroupId = from.GroupId;
            to.CourseId = from.CourseId;
            return to;
        }

        public static List<User> MapDoToList(List<UserDO> from)
        {
            if (from == null)
                throw new ArgumentException();

            List<User> to = new List<User>();

            foreach (UserDO dataObject in from)
            {
                to.Add(UserMapper.MapDoToSingle(dataObject));
            }
            return to;
        }
    }
}