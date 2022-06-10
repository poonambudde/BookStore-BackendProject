using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IAddressRL
    {
        string AddAddress(int UserId, AddressModel addressModel);
        bool UpdateAddress(int AddressId, AddressModel addressModel);
        bool DeleteAddress(int AddressId);
        List<AddressModel> GetAddressByAddressId(int UserId);
        List<AddressModel> GetAllAddress();
    }
}
