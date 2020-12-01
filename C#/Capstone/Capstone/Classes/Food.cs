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

		public Food(string name, Storage storage, DateTime? expiryDate, double quantity, string quantityClassifier, int vegan, int vegetarian, List<string> ingredients, List<string> traces, bool favourite)
		{
			Name = name;
			Storage = storage;
			ExpiryDate = expiryDate;
			Quantity = quantity;
			InitialQuantity = quantity;
			QuantityClassifier = quantityClassifier;
			InitialQuantityClassifier = quantityClassifier;
			Vegan = vegan;
			Vegetarian = vegetarian;
			Ingredients = string.Join("|", ingredients);
			Traces = string.Join("|", traces);
			Favourite = favourite;
		}

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public Storage Storage { get; set; }
		public Nullable<DateTime> ExpiryDate { get; set; }
		public Nullable<DateTime> CalculatedExpiryDate { get; set; }
		public double Quantity { get; set; }
		public string QuantityClassifier { get; set; }
		public double InitialQuantity { get; set; }
		public string InitialQuantityClassifier { get; set; }
		public int Vegan { get; set; }
		public int Vegetarian { get; set; }
		public string Ingredients { get; set; }
		public string Traces { get; set; }
		public int Calories { get; set; }
		public bool Favourite { get; set; }
		public bool OnShoppingList { get; set; }
	}

	public enum Storage
	{
		Fridge,
		Freezer,
		Pantry,
		Cupboard,
		Cellar,
		Other
	}

	[DataContract]
	public class SerializableFood
	{
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string Storage { get; set; }
		[DataMember]
		public Nullable<DateTime> ExpiryDate { get; set; }
		[DataMember]
		public double Quantity { get; set; }
		[DataMember]
		public string QuantityClassifier { get; set; }
		[DataMember]
		public int Vegan { get; set; }
		[DataMember]
		public int Vegetarian { get; set; }
		[DataMember]
		public List<string> Ingredients { get; set; }
		[DataMember]
		public List<string> Traces { get; set; }
		[DataMember]
		public bool Favourite { get; set; }
		[DataMember]
		public bool OnShoppingList { get; set; }
		public SerializableFood(Food food)
		{
			Id = food.Id;
			Name = food.Name;
			ExpiryDate = food.ExpiryDate;
			Quantity = food.Quantity;
			QuantityClassifier = food.QuantityClassifier;
			Storage = food.Storage.ToString();
			Vegan = food.Vegan;
			Vegetarian = food.Vegetarian;
			Favourite = food.Favourite;
			OnShoppingList = food.OnShoppingList;
			if (food.Ingredients.Count() > 0)
				Ingredients = food.Ingredients.Split('|').ToList();
			else
				Ingredients = new List<string>();
			if (food.Traces.Count() > 0)
				Traces = food.Traces.Split('|').ToList();
			else
				Traces = new List<string>();
		}
	}
}