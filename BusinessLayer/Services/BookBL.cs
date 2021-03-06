using BusinessLayer.Interfaces;
using DatabaseLayer;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }
        public BookModel AddBook(BookModel book)
        {
            try
            {
                return this.bookRL.AddBook(book);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BookModel GetBookByBookId(int BookId)
        {
            try
            {
                return this.bookRL.GetBookByBookId(BookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteBook(int bookId)
        {
            try
            {
                return this.bookRL.DeleteBook(bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateBook(int BookId, BookModel updateBook)
        {
            try
            {
                return this.bookRL.UpdateBook(BookId, updateBook);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BookModel> GetAllBooks()
        {
            try
            {
                return this.bookRL.GetAllBooks();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
