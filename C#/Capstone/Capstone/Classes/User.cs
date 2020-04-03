using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Classes
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int GuiltLevel { get; set; }
        public virtual List<Kitchen> Kitchens { get; set; }
        public virtual List<string> DietTags { get; set; }
    }
}