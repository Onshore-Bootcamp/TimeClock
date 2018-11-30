namespace TimeClock_DAL
{
    #region Usings
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using TimeClock_DAL.Mapping;
    using TimeClock_DAL.Models;
    #endregion
    public class TimeEntryDAO
    {
        private readonly string _ConnectionString;
        public TimeEntryDAO(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public void Create(TimeEntryDO timeEntry)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                using (SqlCommand command = new SqlCommand("TimeEntry_CREATE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", timeEntry.UserId);
                    command.Parameters.AddWithValue("@TimeIn", timeEntry.TimeIn);
                    command.Parameters.AddWithValue("@TimeOut", timeEntry.TimeOut == default(DateTime) ? (object)DBNull.Value : timeEntry.TimeOut);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public List<TimeEntryDO> ViewByUserId(Int64 userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (startDate != null)
                if (endDate != null && endDate < startDate)
                    throw new ArgumentException();

            List<TimeEntryDO> usersEntries = new List<TimeEntryDO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                using (SqlCommand command = new SqlCommand("TimeEntry_VIEW_BY_USER_ID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@StartDate", startDate.HasValue ? startDate.Value : startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate.HasValue ? endDate.Value : endDate);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usersEntries.Add(TimeEntryMapper.MapReaderToSingle(reader));
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return usersEntries;
        }

        public TimeEntryDO ViewCurrentEntry(Int64 userId, DateTime date)
        {
            TimeEntryDO currentEntry = new TimeEntryDO();
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                using (SqlCommand command = new SqlCommand("TimeEntry_VIEW_CURRENT_ENTRY", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Date", date);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            currentEntry = TimeEntryMapper.MapReaderToSingle(reader);
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return currentEntry;
        }

        public void Update(TimeEntryDO timeEntry)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                using (SqlCommand command = new SqlCommand("TimeEntry_UPDATE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TimeEntryId", timeEntry.Id);
                    command.Parameters.AddWithValue("@UserId", timeEntry.UserId);
                    command.Parameters.AddWithValue("@TimeIn", timeEntry.TimeIn);
                    command.Parameters.AddWithValue("@TimeOut", timeEntry.TimeOut);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}