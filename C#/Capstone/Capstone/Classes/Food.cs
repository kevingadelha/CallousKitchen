using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Capstone.Classes
{
	public class Food
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Barcode { get; set; }
        public virtual Storage StorageType { get; set; }
        public Nullable<DateTime> ExpiryDate { get; set; }
		public double Quantity { get; set; }
	}

	[DataContract]
	public class SerializableFood
	{
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string Barcode { get; set; }
		[DataMember]
        public virtual Storage StorageType { get; set; }
		[DataMember]
		public Nullable<DateTime> ExpiryDate { get; set; }
		[DataMember]
		public double Quantity { get; set; }
		public SerializableFood(Food food)
		{
			Id = food.Id;
			Name = food.Name;
			Barcode = food.Barcode;
			ExpiryDate = food.ExpiryDate;
			Quantity = food.Quantity;
			StorageType = food.StorageType;

		}
	}
}