using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IWishListBL
    {
        string AddBookinWishList(AddToWishList wishListModel);
        bool DeleteBookinWishList(int WishListId);
        List<WishListModel> GetAllBooksinWishList(int UserId);

    }
}
