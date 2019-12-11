using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject
{
    public class Occupation
    {
        public Occupation(string name, decimal cost, int hours)
        {
            Name = name;
            CostPerHour = cost;
            HoursPerWeek = hours;
        }
        public Occupation(int id, string name, decimal cost, int hours)
        {
            Occupation_ID = id;
            Name = name;
            CostPerHour = cost;
            HoursPerWeek = hours;
        }
        [Key]
        public int Occupation_ID { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        public decimal CostPerHour { get; set; }
        [Required]
        public int HoursPerWeek { get; set; }
    }
}
