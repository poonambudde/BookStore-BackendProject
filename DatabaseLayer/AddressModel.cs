﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer
{
    public class AddressModel
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int TypeId { get; set; }

    }
}
