using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICartBL
    {
        string AddBookToCart(AddToCart cartBook);
        bool UpdateCart(int CartId, int BooksQty);
        string DeleteCart(int CartId);
        List<CartModel> GetAllBooksinCart(int UserId);
    }
}
