using BusinessLayer.Interfaces;
using DatabaseLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("register")]
        public IActionResult Register(UserModel user)
        {
            try
            {
                UserModel userData = this.userBL.Register(user);
                if (userData != null)
                {
                    return this.Ok(new { Success = true, message = "User Added Sucessfully", Response = userData });
                }
                return this.Ok(new { Success = true, message = "User Already Exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login(string Email, string Password)
        {
            try
            {
                var result = this.userBL.Login(Email, Password);
                if (result != null)
                    return this.Ok(new { success = true, message = "Login Successful", data = result });
                else
                    return this.BadRequest(new { success = false, message = "Login Failed", data = result });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var forgotPasswordToken = this.userBL.ForgotPassword(email);
                if (forgotPasswordToken != null)
                {
                    return this.Ok(new { Success = true, message = " Mail Sent Successful", Response = forgotPasswordToken });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid Email" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            try
            {
                var email = User.FindFirst("Email").Value.ToString();
                if (this.userBL.ResetPassword(email, newPassword, confirmPassword))
                {
                    return this.Ok(new { Success = true, message = " Password Changed Successfully " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = " Password Change Unsuccessfully " });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}