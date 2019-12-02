using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace MyProject
{
    public class Trip
    {
        public Trip(int id, Station st1, Station st2, Train t)
        {
            Trip_ID = id;
            Station1 = st1;
            Station2 = st2;
            Train = t;
        }
        public Trip(Station st1, Station st2, Train t)
        {
            Station1 = st1;
            Station2 = st2;
            Train = t;
        }
        [Key]
        public int Trip_ID { get; set; }

        [Required]
        public Station Station1 { get; set; }

        [Required]
        public Station Station2 { get; set; }

        [Required]
        public Train Train { get; set; }
    }
}
