using System;
using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer
{
    // Class For User registration Request
    public class UserModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Full Name required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        public long MobileNumber { get; set; }
    }
}
