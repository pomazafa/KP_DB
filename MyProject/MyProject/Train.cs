using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject
{
    class Train
    {
        public Train (int id, string number, int count, float cost)
        {
            TRAIN_ID = id;
            TRAINNUMBER = number;
            COUNTSEATS = count;
            COSTPERSTATION = cost;
        }

        [Key]
        public int TRAIN_ID { get; set; }

        [Required]
        [StringLength(30)]
        public string TRAINNUMBER { get; set; }

        [Required]
        public int COUNTSEATS { get; set; }

        [Required]
        public float COSTPERSTATION { get; set; }

    }
}
