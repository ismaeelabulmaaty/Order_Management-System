﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Manag.Core.Entites.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string city { get; set; }
        public string Street { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

    }
}
