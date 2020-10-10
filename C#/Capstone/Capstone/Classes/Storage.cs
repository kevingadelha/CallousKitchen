using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Capstone.Classes
{
    public class Storage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [DataContract]
    public class SerializableStorage { 
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }

        public SerializableStorage(Storage s)
        {
            Id = s.Id;
            Name = s.Name;
        }
    }
}