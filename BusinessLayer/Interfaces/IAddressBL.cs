using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IAddressBL
    {
        string AddAddress(int UserId, AddressModel addressModel);
        bool UpdateAddress(int AddressId, AddressModel addressModel);
        bool DeleteAddress(int AddressId);
        List<AddressModel> GetAddressByAddressId(int UserId);
        List<AddressModel> GetAllAddress();
    }
}
