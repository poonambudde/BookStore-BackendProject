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
    public class BookRL : IBookRL
    {
        private SqlConnection sqlConnection;
        public BookRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        public BookModel AddBook(BookModel book)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:Bookstore"]);
                SqlCommand cmd = new SqlCommand("SPAddBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@BookName", book.BookName);
                cmd.Parameters.AddWithValue("@AuthorName", book.AuthorName);
                cmd.Parameters.AddWithValue("@TotalRating", book.TotalRating);
                cmd.Parameters.AddWithValue("@RatingCount", book.RatingCount);
                cmd.Parameters.AddWithValue("@OriginalPrice", book.OriginalPrice);
                cmd.Parameters.AddWithValue("@discountPrice", book.DiscountPrice);
                cmd.Parameters.AddWithValue("@BookDetails", book.BookDetails);
                cmd.Parameters.AddWithValue("@BookImage", book.BookImage);
                cmd.Parameters.AddWithValue("@BookQuantity", book.BookQuantity);
                this.sqlConnection.Open();
                cmd.ExecuteNonQuery();
                return book;     
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

        public BookModel GetBookByBookId(int BookId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("spGetBookByBookId", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BookId", BookId);
                this.sqlConnection.Open();
                BookModel bookModel = new BookModel();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                bookModel.BookName = reader["BookName"].ToString();
                bookModel.AuthorName = reader["AuthorName"].ToString();
                bookModel.TotalRating = Convert.ToInt32(reader["TotalRating"]);
                bookModel.RatingCount = Convert.ToInt32(reader["RatingCount"]);
                bookModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                bookModel.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                bookModel.BookDetails = reader["BookDetails"].ToString();
                bookModel.BookImage = reader["BookImage"].ToString();
                bookModel.BookQuantity = Convert.ToInt32(reader["BookQuantity"]);
                return bookModel;
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