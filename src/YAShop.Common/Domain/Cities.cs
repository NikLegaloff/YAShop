﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAShop.Common.Domain
{
    public class City : DomainObject
    {
        public string Name { get; set; }
        public string NameWithPrice => Name + ", delivery: " + Price.ToString("$0.00");
        public decimal Price { get; set; }

    }
}
