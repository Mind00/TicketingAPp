using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketAPI.Models;
using TicketAPI.Resources.Request;
using TicketAPI.Resources.Response;

namespace TicketAPI.Mapping
{
    public class ResourceToModel : Profile
    {
        public ResourceToModel()
        {
            CreateMap<TicketRequest, Ticket>();
        }
    }
}
