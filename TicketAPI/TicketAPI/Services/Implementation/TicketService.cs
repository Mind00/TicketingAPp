using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketAPI.Models;
using TicketAPI.Resources.Request;
using TicketAPI.Resources.Response;
using TicketAPI.Services.Interface;
using TicketAPI.Shared;

namespace TicketAPI.Services.Implementation
{
    public class TicketService : BaseService, ITicketService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TicketService> _logger;


        public TicketService(IConfiguration configuration, ILogger<TicketService> logger) : base(configuration, logger)
        {
            this._configuration = configuration;
            this._logger = logger;
        }
        public async Task<List<Ticket>> GetTickets()
        {
            try
            {
                //var statements = new IList<Ticket>();
                var statements = await this.GetQueryResultAsync<Ticket>(
                   @"exec sp_TicketTsk @action='select'"
                           , new { }
                    );
                return statements;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetTikcetById(int id)
        {
            try
            {

                string ticketStatus = await this.GetQueryFirstOrDefaultResultAsync<string>
                        (
                           @"exec sp_TicketTsk @action='scan',@Id=@Id"
                           , new { id }
                       );

                return ticketStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ServiceResponse<Ticket>> BuyTicket(Ticket request)
        {
            ServiceResponse<Ticket> service = new ServiceResponse<Ticket>();
            try
            {
                var pDetails = await this.GetQueryFirstOrDefaultResultAsync<Ticket>
                         (
                            @"exec sp_TicketTsk @action='add',@FullName=@FullName,@TicketType=@TicketType,@Price=@Price"
                            , new { request.Fullname, request.TicketType, request.Price }
                        );
                service.IsSuccess = true;
                service.Message = "Ticket Buy Successfully";
                return service;
            }
            catch (Exception ex)
            {
                service.IsSuccess = false;
                service.Message = ex.Message;
                return service;
            }
        }

        public async Task<ServiceResponse<Ticket>> GetTicket(int id)
        {
            ServiceResponse<Ticket> service = new ServiceResponse<Ticket>();
            try
            {
                //var data = new List<Ticket>();
                    Ticket data = await this.GetQueryFirstOrDefaultResultAsync<Ticket>(
                    @"sp_TicketTsk @action='get', @Id = @Id", new { }
                    );
                if (data == null)
                {
                    service.IsSuccess = false;
                    service.Message = "Empty Record";
                    return service;
                }
                else
                {
                    service.IsSuccess = true;
                    service.Data = data;
                    return service;
                }
            }catch(Exception ex)
            {
                service.IsSuccess = false;
                service.Message = "Cannot Fetch Data";
                return service;
            }
        }
    }
}
