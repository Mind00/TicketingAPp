using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketAPI.Models
{
    public class Ticket
    {
        public string Id { get; set; }
       public string Fullname{ get; set; }
       public string TicketType { get; set; }
       public string Price { get; set; }
       public string EntryDate { get; set; }
        public string CreateDate { get; set; }
    }
}
