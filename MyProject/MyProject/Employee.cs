using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject
{
    public class Employee
    {
        public Employee(int id, int uid, int cid, int oid, int isA)
        {
            Employee_ID = id;
            User_ID = uid;
            Card_ID = cid;
            Occupation_ID = oid;
            IsAdmin = isA;
        }
        [Key]
        public int Employee_ID { get; set; }

        [Required]
        public int User_ID { get; set; }

        [Required]
        public int Card_ID{ get; set; }
        [Required]
        public int Occupation_ID { get; set; }
        [Required]
        public int IsAdmin { get; set; }
    }
}
