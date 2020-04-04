using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Capstone.Classes
{
	public class Kitchen
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual List<Food> Inventory { get; set; }
	}
	[DataContract]
	public class SerializableKitchen
	{
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public List<Food> Inventory { get; set; }
		public SerializableKitchen(Kitchen kitchen)
		{
			Id = kitchen.Id;
			Name = kitchen.Name;
			Inventory = kitchen.Inventory;
		}
	}
}