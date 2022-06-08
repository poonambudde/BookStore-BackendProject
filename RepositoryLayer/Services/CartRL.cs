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
    public class CartRL : ICartRL
    {
        private SqlConnection sqlConnection;

        public CartRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string AddBookToCart(AddToCart cartBook)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:Bookstore"]);
                SqlCommand cmd = new SqlCommand("spAddCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserId", cartBook.UserId);
                cmd.Parameters.AddWithValue("@BookId", cartBook.BookId);
                cmd.Parameters.AddWithValue("@BooksQty", cartBook.BooksQty);

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                return "book added in cart successfully";
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

    }
}
