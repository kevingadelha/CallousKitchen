using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Classes
{
	public class Kitchen
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual List<Food> Inventory { get; set; }
	}
}