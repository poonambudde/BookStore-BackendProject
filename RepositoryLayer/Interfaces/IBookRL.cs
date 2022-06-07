﻿using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRL
    {
        public BookModel AddBook(BookModel book);
        public BookModel GetBookByBookId(int BookId);
        public bool DeleteBook(int BookId);
        bool UpdateBook(int BookId, BookModel updateBook);
        public List<BookModel> GetAllBooks();
    }
}
