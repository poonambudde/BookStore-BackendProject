using System;

namespace DatabaseLayer
{
    // Class For User registration Request
    public class UserModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long MobileNumber { get; set; }
    }
}
