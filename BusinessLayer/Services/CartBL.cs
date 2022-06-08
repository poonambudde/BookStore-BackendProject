using BusinessLayer.Interfaces;
using DatabaseLayer;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        private readonly ICartRL cartRL;

        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public string AddBookToCart(AddToCart cartBook)
        {
            try
            {
                return cartRL.AddBookToCart(cartBook);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
