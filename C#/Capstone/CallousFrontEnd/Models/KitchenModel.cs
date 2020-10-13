﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountService;

namespace CallousFrontEnd.Models
{
    // Author: Peter Szadurski
    public class KitchenModel
    {
        public List<SerializableKitchen> Kitchens { get; set; }
        public List<Storage> Storages { get; set; }
    }
}