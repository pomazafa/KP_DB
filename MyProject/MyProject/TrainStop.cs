﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject
{
    class TrainStop
    {
        public TrainStop(int id, DateTime arriv, DateTime depart, Station station)
        {
            TrainStop_ID = id;
            Station = station;
            Arrival_datetime = arriv;
            Departure_datetime = depart;
        }
        [Key]
        public int TrainStop_ID { get; set; }

        [Required]
        public Station Station { get; set; }
        [Required]
        public DateTime Arrival_datetime { get; set; }
        [Required]
        public DateTime Departure_datetime { get; set; }

    }
}