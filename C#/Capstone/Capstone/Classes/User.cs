using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Capstone.Classes
{
    public class User
    {
		public User()
		{
		}

		public User(string username, string email, string password, int guiltLevel, bool vegetarian, bool vegan, bool calorieTracker, List<Kitchen> kitchens, List<string> allergies)
		{
			Username = username;
			Email = email;
			Password = password;
			GuiltLevel = guiltLevel;
			Vegetarian = vegetarian;
			Vegan = vegan;
			CalorieTracker = calorieTracker;
			Kitchens = kitchens;
			Allergies = string.Join("|", allergies);
		}

		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int GuiltLevel { get; set; }
        public bool Vegetarian { get; set; }
        public bool Vegan { get; set; }
        public bool CalorieTracker { get; set; }
        public virtual List<Kitchen> Kitchens { get; set; }
        //While it's not pretty, the suggested course of action I found online was to use a delimited string to save a list of strings
        //This is because entityframework is to dumb to generate a table for a list of strings
        public string Allergies { get; set; }
    }
	[DataContract]
	public class SerializableUser
	{
		[DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int GuiltLevel { get; set; }
        [DataMember]
        public bool Vegetarian { get; set; }
        [DataMember]
        public bool Vegan { get; set; }
        [DataMember]
        public List<SerializableKitchen> Kitchens { get; set; }
        [DataMember]
        public List<string> Allergies { get; set; }
        public SerializableUser(User user)
		{
            if (user != null)
            {
                Id = user.Id;
                Username = user.Username;
                Email = user.Email;
                //Hopefully not serializing the password won't cause problems and will protect security
                //Password = user.Password;
                GuiltLevel = user.GuiltLevel;
                Vegetarian = user.Vegetarian;
                Vegan = user.Vegan;
                Kitchens = user.Kitchens.Select(o => new SerializableKitchen(o)).ToList();
                Allergies = user.Allergies.Split('|').ToList();
            }
            else
                Id = -1;
		}

		public SerializableUser(int id)
		{
			Id = id;
		}
	}
}