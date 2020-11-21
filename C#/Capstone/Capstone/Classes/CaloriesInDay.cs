using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Capstone.Classes
{
	public class CaloriesInDay
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public DateTime Day { get; set; }
		public int Calories { get; set; }
	}

	[DataContract]
	public class SerializableCaloriesInDay
	{
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		public DateTime Day { get; set; }
		[DataMember]
		public int Calories { get; set; }
		public SerializableCaloriesInDay(CaloriesInDay caloriesInDay)
		{
			Id = caloriesInDay.Id;
			Day = caloriesInDay.Day;
			Calories = caloriesInDay.Calories;
		}
	}
}