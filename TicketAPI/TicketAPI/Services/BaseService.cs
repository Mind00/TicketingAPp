using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace TicketAPI.Services
{
    public abstract class BaseService
    {
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        protected BaseService(IConfiguration configuration, ILogger logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }
        protected IDbConnection Connection => new SqlConnection(this.configuration.GetConnectionString("dbConnect"));

        protected async Task<List<T>> GetQueryResultAsync<T>(string sQuery, object parameters = null, IDbTransaction transaction = null)
        {
            this.logger.LogDebug($"QUERY GetQueryResultAsync COMMAND | { sQuery }");
            this.logger.LogDebug($"QUERY GetQueryResultAsync PARAMETERS | { parameters }");

            var command = new CommandDefinition(sQuery, parameters, transaction);

            var result = await this.Connection.QueryAsync<T>(command);

            this.logger.LogDebug($"QUERY GetQueryResultAsync EXECUTED");

            return result.ToList();

        }

        protected async Task<T> GetQueryFirstOrDefaultResultAsync<T>(string sQuery, object parameters, IDbTransaction transaction = null)
        {
            this.logger.LogDebug($"QUERY GetQueryFirstOrDefaultResultAsync COMMAND | { sQuery }");
            this.logger.LogDebug($"QUERY GetQueryFirstOrDefaultResultAsync PARAMETERS | { parameters }");

            var command = new CommandDefinition(sQuery, parameters, transaction);

            var result = await this.Connection.QueryFirstOrDefaultAsync<T>(command);

            this.logger.LogDebug($"QUERY GetQueryFirstOrDefaultResultAsync EXECUTED");

            return result;
        }
        protected async Task<int> ExecuteAsync(string sQuery, object parameters, IDbTransaction transaction = null)
        {
            this.logger.LogDebug($"QUERY ExecuteAsync COMMAND | { sQuery }");
            this.logger.LogDebug($"QUERY ExecuteAsync PARAMETERS | { parameters }");

            var command = new CommandDefinition(sQuery, parameters, transaction);

            var rowsAffected = await this.Connection.ExecuteAsync(command);

            this.logger.LogDebug($"QUERY ExecuteAsync EXECUTED");

            return rowsAffected;
        }

        protected async Task<GridReader> GetFromMultipleQuery(string sQuery, object parameters, IDbTransaction transaction = null)
        {
            this.logger.LogDebug($"QUERY InsertAndGetId COMMAND | { sQuery }");
            this.logger.LogDebug($"QUERY InsertAndGetId PARAMETERS | { parameters }");

            var command = new CommandDefinition(sQuery, parameters, transaction);

            var result = await this.Connection.QueryMultipleAsync(command);

            this.logger.LogDebug($"QUERY GetQueryResultCount EXECUTED");

            return result;
        }
    }
}
