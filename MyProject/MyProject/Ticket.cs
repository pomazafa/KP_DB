using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject
{
    class Ticket
    {
        public Ticket(int id, DateTime dt, Trip tr, Client cl)
        {
            TicketID = id;
            DateOfTrip = dt;
            Trip = tr;
            Client = cl;
        }
        [Key]
        public int TicketID { get; set; }

        public DateTime DateOfPurchase { get; set; }

        [Required]
        public DateTime DateOfTrip { get; set; }

        [Required]
        public Trip Trip { get; set; }

        [Required]
        public Client Client { get; set; }
    }
}
