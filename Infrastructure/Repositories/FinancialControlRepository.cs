using System.Collections.Generic;
using System.Text;
using Dapper;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class FinancialControlRepository : IFinancialControlRepository
    {
        private readonly IUnitOfWork _uow;

        public FinancialControlRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<FinancialExpenseEntity> AddNewExpenseAsync(FinancialExpenseEntity entity)
        {
            StringBuilder query = new();

            query.Append($" INSERT INTO[FinancialExpense]   ");
            query.Append($" (                               ");
            query.Append($"     UserId,                     ");
            query.Append($"     Category,                   ");
            query.Append($"     Description,                ");
            query.Append($"     Value,                      ");
            query.Append($"     TransactionDate,            ");
            query.Append($"     CreatedAt                   ");
            query.Append($" )                               ");
            query.Append($" VALUES(                         ");
            query.Append($"     '@UserId',                  ");
            query.Append($"     '@Category',                ");
            query.Append($"     '@Description',             ");
            query.Append($"     '@Value',                   ");
            query.Append($"     '@TransactionDate',         ");
            query.Append($"     '@CreatedAt'                ");            
            query.Append($" );                              ");

            DynamicParameters parameters = new();
            parameters.Add("@UserId", entity.UserId);
            parameters.Add("@Category", entity.Category);
            parameters.Add("@Description", entity.Description);
            parameters.Add("@Value", entity.Value);
            parameters.Add("@TransactionDate", entity.TransactionDate);
            parameters.Add("@CreatedAt", entity.CreatedAt);

            using var connection = _uow.Connection;

            await connection.ExecuteAsync(query.ToString(), parameters);
            return entity;
        }
    }
}

