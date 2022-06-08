﻿using BusinessLayer.Interfaces;
using DatabaseLayer;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [HttpPost("AddBooksInCart")]
        public IActionResult AddBookToCart(AddToCart cartBook)
        {
            try
            {
                var result = this.cartBL.AddBookToCart(cartBook);
                if (result.Equals("book added in cart successfully"))
                {
                    return this.Ok(new { success = true, message = $"Book added in Cart Successfully " });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPut("UpdateCart/{CartId}")]
        public IActionResult UpdateCart(int CartId, int BooksQty)
        {
            try
            {
                var result = this.cartBL.UpdateCart(CartId, BooksQty);
                if (result.Equals(true))
                {
                    return this.Ok(new { success = true, message = $"Cart updated Successfully ", response = BooksQty });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpDelete("DeleteCart/{CartId}")]
        public IActionResult DeleteCart(int CartId)
        {
            try
            {
                var result = this.cartBL.DeleteCart(CartId);
                if (result.Equals("Book Deleted in Cart Successfully"))
                {
                    return this.Ok(new { success = true, message = $"Book in Cart deleted Successfully " });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("GetAllBooksinCart/{UserId}")]
        public IActionResult GetAllBooksinCart(int UserId)
        {
            try
            {
                var result = this.cartBL.GetAllBooksinCart(UserId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"All Books Displayed in the cart Successfully ", response = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = $"Books are not there " });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
