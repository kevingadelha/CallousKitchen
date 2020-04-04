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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int GuiltLevel { get; set; }
        public virtual List<Kitchen> Kitchens { get; set; }
        public virtual List<string> DietTags { get; set; }
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
        public List<Kitchen> Kitchens { get; set; }
        [DataMember]
        public List<string> DietTags { get; set; }
        public SerializableUser(User user)
		{
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
            Password = user.Password;
            GuiltLevel = user.GuiltLevel;
            Kitchens = user.Kitchens;
            DietTags = user.DietTags;
		}
	}
}