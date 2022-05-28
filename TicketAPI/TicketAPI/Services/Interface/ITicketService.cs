using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketAPI.Models;
using TicketAPI.Resources.Request;
using TicketAPI.Resources.Response;
using TicketAPI.Shared;

namespace TicketAPI.Services.Interface
{
    public interface ITicketService
    {
        Task<List<Ticket>> GetTickets();
        Task<string> GetTikcetById(int id);
        Task<ServiceResponse<Ticket>> BuyTicket( Ticket request);
        Task<ServiceResponse<Ticket>> GetTicket(int id);
    }
}
