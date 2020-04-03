using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Classes
{
	public class Food
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Barcode { get; set; }
		public DateTime ExpiryDate { get; set; }
		public double Quantity { get; set; }
	}
}