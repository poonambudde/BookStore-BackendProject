using BusinessLayer.Interfaces;
using DatabaseLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }
        [HttpPost("AddBook")]
        public  IActionResult AddBook(BookModel book)
        {
            try
            {
                var bookDetail = this.bookBL.AddBook(book);
                if (bookDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Book Added Successfully", Response = bookDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Book Added Unsuccessfully" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [HttpGet("GetBook/{BookId}")]
        public IActionResult GetBookByBookId(int BookId)
        {
            try
            {
                var book = this.bookBL.GetBookByBookId(BookId);
                if (book != null)
                {
                    return this.Ok(new { Success = true, message = "Book Detail Fetched Sucessfully", Response = book });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeleteBook/{BookId}")]
        public IActionResult DeleteBook(int BookId)
        {
            try
            {
                if (this.bookBL.DeleteBook(BookId))
                {
                    return this.Ok(new { Success = true, message = "Book Deleted Sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [HttpGet("GetAllBook")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var updatedBookDetail = this.bookBL.GetAllBooks();
                if (updatedBookDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Book Detail Fetched Sucessfully", Response = updatedBookDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

    }
}
