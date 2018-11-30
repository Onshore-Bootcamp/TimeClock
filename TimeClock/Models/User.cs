namespace TimeClock.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public Int64 Id { get; set; }

        public Int64 LMSId { get; set; }

        public Int64 GroupId { get; set; }

        public Int64 CourseId { get; set; }

        [Required]
        [StringLength(50, MinimumLength =5, ErrorMessage = "Usernames must be atleast 5 long but no longer than 50.")]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}