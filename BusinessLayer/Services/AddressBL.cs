﻿using BusinessLayer.Interfaces;
using DatabaseLayer;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBL : IAddressBL
    {
        IAddressRL addressRL;

        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }

        public string AddAddress(int UserId, AddressModel addressModel)
        {
            try
            {
                return addressRL.AddAddress(UserId, addressModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateAddress(int AddressId, AddressModel addressModel)
        {
            try
            {
                return addressRL.UpdateAddress(AddressId, addressModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteAddress(int AddressId)
        {
            try
            {
                return addressRL.DeleteAddress(AddressId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AddressModel> GetAddressByAddressId(int UserId)
        {
            try
            {
                return addressRL.GetAddressByAddressId(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AddressModel> GetAllAddress()
        {
            try
            {
                return addressRL.GetAllAddress();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
