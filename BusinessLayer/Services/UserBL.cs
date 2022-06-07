using BusinessLayer.Interfaces;
using DatabaseLayer;
using RepositoryLayer.Interfaces;
using System;

namespace BusinessLayer
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public UserModel AddUser(UserModel user)
        {
            try
            {
                return this.userRL.AddUser(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserLogin LoginUser(string Email, string Password)
        {
            try
            {
                return this.userRL.LoginUser(Email, Password);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ForgotPassword(string email)
        {
            try
            {
                return this.userRL.ForgotPassword(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                return this.userRL.ResetPassword(email, newPassword, confirmPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}