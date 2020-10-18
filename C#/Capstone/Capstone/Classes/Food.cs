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
		public Food()
		{
		}

		public Food(string name, DateTime? expiryDate, double quantity, int vegan, int vegetarian, List<string> ingredients, List<string> traces, int calories)
		{
			Name = name;
			ExpiryDate = expiryDate;
			Quantity = quantity;
			Vegan = vegan;
			Vegetarian = vegetarian;
			Ingredients = string.Join("|",ingredients);
			Traces = string.Join("|", traces);
			Calories = calories;
		}

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Barcode { get; set; }
		public int StorageId {get; set ;}
        public virtual Storage Storage { get; set; }
        public Nullable<DateTime> ExpiryDate { get; set; }
		public double Quantity { get; set; }
		public int Vegan { get; set; }
		public int Vegetarian { get; set; }
		public string Ingredients { get; set; }
		public string Traces { get; set; }
		public int Calories { get; set; }
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
		public int StorageId { get; set; }
		[DataMember]
        public SerializableStorage Storage { get; set; }
		[DataMember]
		public Nullable<DateTime> ExpiryDate { get; set; }
		[DataMember]
		public double Quantity { get; set; }
		[DataMember]
		public int Vegan { get; set; }
		[DataMember]
		public int Vegetarian { get; set; }
		[DataMember]
		public List<string> Ingredients { get; set; }
		[DataMember]
		public List<string> Traces { get; set; }
		[DataMember]
		public int Calories { get; set; }
		public SerializableFood(Food food)
		{
			Id = food.Id;
			Name = food.Name;
			Barcode = food.Barcode;
			ExpiryDate = food.ExpiryDate;
			Quantity = food.Quantity;
			StorageId = food.StorageId;
			Storage = new SerializableStorage(food.Storage);
			Vegan = food.Vegan;
			Vegetarian = food.Vegetarian;
			Calories = food.Calories;
			//I hope I don't have to make a deep copy of this
			Ingredients = food.Ingredients.Split('|').ToList();
			Traces = food.Traces.Split('|').ToList();
		}
	}
}