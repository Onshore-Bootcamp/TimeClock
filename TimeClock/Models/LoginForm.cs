namespace TimeClock.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginForm
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is not optional")]
        public string Password { get; set; }
    }
}