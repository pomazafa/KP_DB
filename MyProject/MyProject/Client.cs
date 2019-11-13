using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace MyProject
{
    class Client
    {
        public Client()
        {
            //VISIT = new HashSet<VISIT>();
        }

        public Client(int id, string Login, string last, string first, DateTime? bd, string telephone, string patr)
        {
            //VISIT = new HashSet<VISIT>();

            Client_ID = id;
            LOGIN = Login.Trim();
            LASTNAME = last.Trim();
            FIRSTNAME = first.Trim();
            BDAY = bd;
            TELEPHONE = telephone.Trim();
            PATRONIMIC = patr.Trim();
        }


        [Key]
        public int Client_ID { get; set; }

        [Required]
        [StringLength(30)]
        public string LOGIN { get; set; }

        [Required]
        [StringLength(30)]
        public string LASTNAME { get; set; }

        [Required]
        [StringLength(30)]
        public string FIRSTNAME { get; set; }

        [StringLength(30)]
        public string PATRONIMIC { get; set; }

        [Column(TypeName = "date")]
        [Required]
        public DateTime? BDAY { get; set; }

        [StringLength(15)]
        public string TELEPHONE { get; set; }

        //public int? ADDRESS_ID { get; set; }

        //public virtual ADDRESS ADDRESS { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<VISIT> VISIT { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<RECIPE> RECIPE { get; set; }

        public override string ToString()
        {
            return Client_ID + " - id\n" + LASTNAME + " - Lastname\n" + FIRSTNAME + " - firstname\n" + BDAY.ToString() + " - bday\n";

        }
    }
}