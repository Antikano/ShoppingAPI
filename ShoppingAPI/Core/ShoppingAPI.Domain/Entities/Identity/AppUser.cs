﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Domain.Entities.Identity
{
    public class AppUser: IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }


        public ICollection<Basket> Baskets { get; set; }

    }
}
