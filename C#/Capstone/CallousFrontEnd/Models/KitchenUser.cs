using Capstone.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallousFrontEnd.Models
{
    public class KitchenUser
    {
        public int UserId { get; set; }
        public Kitchen kitchen { get; set; }
    }
}
