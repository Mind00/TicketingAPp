using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketAPI.Resources.Request
{
    public class TicketRequest
    {
        public string Fullname { get; set; }
        public string TicketType { get; set; }
        public string Price { get; set; }
        public string Expire { get; set; }
    }
}
