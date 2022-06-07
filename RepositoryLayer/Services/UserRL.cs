using DatabaseLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer
{
    public class UserRL : IUserRL
    {
        private SqlConnection sqlConnection;
        public UserRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        // Registers the specified user.
        public UserModel AddUser(UserModel user)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("spUserRegister", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                this.sqlConnection.Open();
                var result = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (result != 0)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)



            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

        public UserLogin LoginUser(string Email, string Password)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand com = new SqlCommand("spUserLogin", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@Email", Email);
                com.Parameters.AddWithValue("@Password", Password);
                this.sqlConnection.Open();
                SqlDataReader rdr = com.ExecuteReader();
                if (rdr.HasRows)
                {
                    int UserId = 0;
                    UserLogin user = new UserLogin();
                    while (rdr.Read())
                    {
                        user.Email = Convert.ToString(rdr["Email"]);
                        user.Password = Convert.ToString(rdr["Password"]);
                        UserId = Convert.ToInt32(rdr["UserId"]);
                    }
                    this.sqlConnection.Close();
                    user.Token = this.GenerateJWTToken(Email, UserId);
                    return user;
                }
                else
                {
                    this.sqlConnection.Close();
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

        // Generate JWT token
        public string GenerateJWTToken(string email, long userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email", email),
                new Claim("Id", userId.ToString()),
            };
            var token = new JwtSecurityToken(
            this.Configuration["Jwt:Issuer"],
            this.Configuration["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string ForgotPassword(string email)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand com = new SqlCommand("spUserForgotPassword", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@Email", email);
                this.sqlConnection.Open();
                SqlDataReader rd = com.ExecuteReader();
                if (rd.HasRows)
                {
                    int userId = 0;
                    while (rd.Read())
                    {
                        email = Convert.ToString(rd["Email"]);
                        userId = Convert.ToInt32(rd["UserId"]);
                    }

                    this.sqlConnection.Close();
                    var token = this.GenerateJWTToken(email, userId);
                    new EmailModel().Sender(token);
                    return token;
                }
                else
                {
                    this.sqlConnection.Close();
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                if (newPassword == confirmPassword)
                {
                    this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                    SqlCommand com = new SqlCommand("spUserResetPassword", this.sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    com.Parameters.AddWithValue("@Email", email);
                    com.Parameters.AddWithValue("@Password", confirmPassword);
                    this.sqlConnection.Open();
                    int i = com.ExecuteNonQuery();
                    this.sqlConnection.Close();
                    if (i >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
    }
}