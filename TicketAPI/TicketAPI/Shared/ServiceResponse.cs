using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketAPI.Shared
{
    public class ServiceResponse<T>
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
        public T Data { get; set; }
    }
}
