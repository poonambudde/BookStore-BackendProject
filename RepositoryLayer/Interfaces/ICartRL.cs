using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ICartRL
    {
        string AddBookToCart(AddToCart cartBook);
        bool UpdateCart(int CartId, int BooksQty);
        string DeleteCart(int CartId);
        List<CartModel> GetAllBooksinCart(int UserId);
    }
}
