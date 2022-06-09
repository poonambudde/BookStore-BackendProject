using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IWishListRL
    {
        string AddBookinWishList(AddToWishList wishListModel);
        bool DeleteBookinWishList(int WishListId);
        List<WishListModel> GetAllBooksinWishList(int UserId);
    }
}
