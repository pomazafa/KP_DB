using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace MyProject
{
    public class Station :ICloneable
    {
        public Station()
        {

        }
        public Station(int id, string name, Address adr)
        {
            Station_ID = id;
            Name = name;
            Address = adr;
        }
        [Key]
        public int Station_ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        public Address Address { get; set; }

        public object Clone()
        {
            return new Station(Station_ID, Name, Address);
        }
    }
}
