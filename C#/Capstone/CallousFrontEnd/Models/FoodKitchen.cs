using AccountService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallousFrontEnd.Models
{
    public class FoodKitchen
    {
        public int KitchenId { get; set; }
        public Food Food { get; set; }
    }
}
