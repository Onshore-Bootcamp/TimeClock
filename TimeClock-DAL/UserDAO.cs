using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeClock_DAL.Models;

namespace TimeClock_DAL
{
    public class UserDAO
    {
        private readonly string _ConnectionString;
        public UserDAO(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        //Done
        public List<UserDO> ViewUsers()
        {
            List<UserDO> allUsers = new List<UserDO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                using (SqlCommand command = new SqlCommand("User_VIEW_USERS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserDO row = new UserDO();
                            row.Id = reader.GetInt64(0);
                            row.LMSId = reader.GetInt64(1);
                            row.Username = reader.GetString(2);
                            row.FirstName = reader.GetString(3);
                            row.LastName = reader.GetString(4);
                            row.Password = reader.GetString(5);
                            row.Role = reader.GetString(6);
                            row.Active = reader.GetBoolean(7);

                            row.GroupId = reader.GetInt64(8);
                            row.CourseId = reader.GetInt64(9);
                            allUsers.Add(row);
                        }
                    }
                }
            }
            catch (SqlException sqlException)
            {
                throw sqlException;
            }
            return allUsers;
        }

        //Done
        public UserDO ViewUserByUsername(string username)
        {
            UserDO oUser = new UserDO();
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                using (SqlCommand command = new SqlCommand("User_VIEW_BY_USERNAME", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", username);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            oUser.Id = reader.GetInt64(0);
                            oUser.LMSId = reader.GetInt64(1);
                            oUser.Username = reader.GetString(2);
                            oUser.FirstName = reader.GetString(3);
                            oUser.LastName = reader.GetString(4);
                            oUser.Password = reader.GetString(5);
                            oUser.Role = reader.GetString(6);
                            oUser.Active = reader.GetBoolean(7);

                            oUser.GroupId = reader.GetInt64(8);
                            oUser.CourseId = reader.GetInt64(9);
                        }
                    }
                }
            }
            catch (SqlException sqlException)
            {
                throw sqlException;
            }
            return oUser;
        }

        //Done
        public UserDO ViewUserByUserId(Int64 userId)
        {
            UserDO oUser = new UserDO();
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                using (SqlCommand command = new SqlCommand("User_VIEW_BY_USER_ID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", userId);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            oUser.Id = reader.GetInt64(0);
                            oUser.LMSId = reader.GetInt64(1);
                            oUser.Username = reader.GetString(2);
                            oUser.FirstName = reader.GetString(3);
                            oUser.LastName = reader.GetString(4);
                            oUser.Password = reader.GetString(5);
                            oUser.Role = reader.GetString(6);
                            oUser.Active = reader.GetBoolean(7);

                            oUser.GroupId = reader.GetInt64(8);
                            oUser.CourseId = reader.GetInt64(9);
                        }
                    }
                }
            }
            catch (SqlException sqlException)
            {
                throw sqlException;
            }
            return oUser;
        }

        //Done
        public void CreateUser(UserDO userDO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                using (SqlCommand command = new SqlCommand("User_CREATE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@LMSId", userDO.LMSId);
                    command.Parameters.AddWithValue("@Username", userDO.Username);
                    command.Parameters.AddWithValue("@FirstName", userDO.FirstName);
                    command.Parameters.AddWithValue("@LastName", userDO.LastName);
                    command.Parameters.AddWithValue("@Password", userDO.Password);
                    command.Parameters.AddWithValue("@Role", userDO.Role);
                    command.Parameters.AddWithValue("@Active", userDO.Active);

                    command.Parameters.AddWithValue("@GroupId", userDO.GroupId);
                    command.Parameters.AddWithValue("@CourseId", userDO.CourseId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlException)
            {
                throw sqlException;
            }
        }

        public void GenerateTimeSheetsForAllUsers()
        {
            try
            {
                TimeEntryDAO teDAO = new TimeEntryDAO(_ConnectionString);
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                using (SqlCommand command = new SqlCommand("SELECT DISTINCT USERID FROM USERS", connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Int64 id = reader.GetInt64(0);

                            for (int i = 0; i < (new DateTime(2018, 5, 18) - new DateTime(2018, 3, 26)).Days; i++)
                            {
                                Random rnd = new Random();
                                DateTime newMorningTi = new DateTime(2018, 3, 26).AddDays(i);
                                if (newMorningTi.DayOfWeek != DayOfWeek.Saturday && newMorningTi.DayOfWeek != DayOfWeek.Sunday)
                                {
                                    newMorningTi = new DateTime(2018, 3, 26, 8, rnd.Next(0, 15), 0).AddDays(i);
                                    DateTime newMorningTo = new DateTime(2018, 3, 26, 11, rnd.Next(26, 35), 0).AddDays(i);
                                    DateTime newANTi = new DateTime(2018, 3, 26, 12, rnd.Next(26, 35), 0).AddDays(i);
                                    DateTime newANTo = new DateTime(2018, 3, 26, 16, rnd.Next(56, 60), 0).AddDays(i);

                                    teDAO.Create(new TimeEntryDO { UserId = id, TimeIn = newMorningTi, TimeOut = newMorningTo });
                                    teDAO.Create(new TimeEntryDO { UserId = id, TimeIn = newANTi, TimeOut = newANTo });
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {

            }
        }

        public void UpdateUser(UserDO userDO)
        {
            throw new NotImplementedException();
        }
    }
}
