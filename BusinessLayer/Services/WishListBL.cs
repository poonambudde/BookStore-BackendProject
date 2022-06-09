using BusinessLayer.Interfaces;
using DatabaseLayer;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBL : IWishListBL
    {
        private readonly IWishListRL wishListRL;
        public WishListBL(IWishListRL wishListRL)
        {
            this.wishListRL = wishListRL;
        }

        public string AddBookinWishList(AddToWishList wishListModel)
        {
            try
            {
                return wishListRL.AddBookinWishList(wishListModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteBookinWishList(int WishListId)
        {
            try
            {
                return wishListRL.DeleteBookinWishList(WishListId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WishListModel> GetAllBooksinWishList(int UserId)
        {
            try
            {
                return wishListRL.GetAllBooksinWishList(UserId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
