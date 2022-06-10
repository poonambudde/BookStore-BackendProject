using DatabaseLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AddressRL : IAddressRL
    {
        private SqlConnection sqlConnection;

        public AddressRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string AddAddress(int UserId, AddressModel addressModel)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("spAddAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Address", addressModel.Address);
                cmd.Parameters.AddWithValue("@City", addressModel.City);
                cmd.Parameters.AddWithValue("@State", addressModel.State);
                cmd.Parameters.AddWithValue("@TypeId", addressModel.TypeId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                this.sqlConnection.Open();
                cmd.ExecuteScalar();
                return " Address Added Successfully";
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

        public bool UpdateAddress(int AddressId, AddressModel addressModel)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("spUpdateAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AddressId", AddressId);
                cmd.Parameters.AddWithValue("@Address", addressModel.Address);
                cmd.Parameters.AddWithValue("@City", addressModel.City);
                cmd.Parameters.AddWithValue("@State", addressModel.State);
                cmd.Parameters.AddWithValue("@TypeId", addressModel.TypeId);
                this.sqlConnection.Open();
                cmd.ExecuteScalar();
                return true;            
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

        public bool DeleteAddress(int addressId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("spDeleteAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AddressId", addressId);
                this.sqlConnection.Open();
                cmd.ExecuteNonQuery();
                return true;
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
        public List<AddressModel> GetAddressByAddressId(int UserId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("spGetAddressByUserId", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserId", UserId);
                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    List<AddressModel> addressModel = new List<AddressModel>();
                    while (reader.Read())
                    {
                        addressModel.Add(new AddressModel
                        {
                            Address = reader["Address"].ToString(),
                            City = reader["City"].ToString(),
                            State = reader["State"].ToString(),
                            TypeId = Convert.ToInt32(reader["TypeId"]),
                            UserId = Convert.ToInt32(reader["UserId"])
                        });
                    }
                    this.sqlConnection.Close();
                    return addressModel;
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

        public List<AddressModel> GetAllAddress()
        {
            try
            {
                List<AddressModel> address = new List<AddressModel>();

                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("spGetAllAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AddressModel model = new AddressModel();
                        model.UserId = Convert.ToInt32(reader["UserId"]);
                        model.AddressId = Convert.ToInt32(reader["AddressId"]);
                        model.Address = reader["Address"].ToString();
                        model.City = reader["City"].ToString();
                        model.State = reader["State"].ToString();
                        model.TypeId = Convert.ToInt32(reader["TypeId"]);
                        address.Add(model);
                    }
                    return address;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

    }
}