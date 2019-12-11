using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject
{
    public class Card
    {
        public Card(int id, string numb, string espir, string owner)
        {
            Card_ID = id;
            Number = numb;
            Espiration_date = espir;
            Owner = owner;
        }
        public Card (string numb, string espir, string owner)
        {
            Number = numb;
            Espiration_date = espir;
            Owner = owner;
        }
        [Key]
        public int Card_ID { get; set; }

        [Required]
        [MinLength(16)]
        [MaxLength(16)]
        public string Number { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        [RegularExpression("^0[1-9]|1[0-2]/[0-9]{2}$")]
        public string Espiration_date { get; set; }

        [MaxLength(30)]
        public string Owner { get; set; }
    }
}
