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
    public class ModelToResource : Profile
    {
        public ModelToResource()
        {
            CreateMap<Ticket, TicketResponse>().ForMember(a=>a.ExpiryDate, b=>b.MapFrom(c=>c.EntryDate))
                .ForMember(d=>d.CreatedDate, e=>e.MapFrom(f=>f.CreateDate));
        }
    }
}
