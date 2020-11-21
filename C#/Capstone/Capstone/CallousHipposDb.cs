using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Capstone.Classes;

namespace Capstone
{
	public class CallousHipposDb : DbContext
	{
		public CallousHipposDb() : base("name=CallousHipposDb")
		{
		}
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<CaloriesInDay> CaloriesInDays { get; set; }
		public virtual DbSet<Kitchen> Kitchens { get; set; }
		public virtual DbSet<Food> Foods { get; set; }
	}
}