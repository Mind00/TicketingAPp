using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketAPI.Resources.Response
{
    public class TicketResponse
    {
        public string Id { get; set; } 
        public string Fullname { get; set; }
        public string TicketType { get; set; }
        public string Price { get; set; }
        public string ExpiryDate { get; set; }
        public string CreatedDate { get; set; }
    }
}
