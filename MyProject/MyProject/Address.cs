using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject
{
    public class Address
    {
        public Address()
        {

        }
        public Address(int id, string town, string street, string house)
        {
            ADDRESS_ID = id;
            TOWN = town;
            STREET = street;
            HOUSE = house;
        }
        [Key]
        public int ADDRESS_ID { get; set; }

        [StringLength(30)]
        public string TOWN { get; set; }

        [StringLength(30)]
        public string STREET { get; set; }
        [StringLength(10)]
        public string HOUSE { get; set; }

        public int FLAT{ get; set; }

    }
}
