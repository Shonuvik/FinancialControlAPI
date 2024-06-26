﻿using System.Text;
using Dapper;
using Domain.Entities;
using Domain.Enums;
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

        public async Task<List<HistoryExpenseEntity>> FindAsync(CategoryType category, long userId)
        {
            StringBuilder query = new();
            DynamicParameters parameters = new();

            query.Append($" SELECT                                                               ");
            query.Append($"     Category        AS {nameof(HistoryExpenseEntity.Category)},      ");
            query.Append($"     Description     AS {nameof(HistoryExpenseEntity.Description)},   ");
            query.Append($"     Value           AS {nameof(HistoryExpenseEntity.Value)},         ");
            query.Append($"     TransactionDate AS {nameof(HistoryExpenseEntity.TransactionDate)}");
            query.Append($" FROM [FinancialExpense]     ");
            query.Append($" WHERE 1=1                   ");

            if(userId != 0)
            {
                query.Append($" AND UserId = @UserId  ");
                parameters.Add("@UserId", userId);
            }

            if (category != CategoryType.Default)
            {
                query.Append($" AND Category = @Category  ");
                parameters.Add("@Category", category);
            }

            using var connection = _uow.Connection;
            return (await connection.QueryAsync<HistoryExpenseEntity>(
                query.ToString(),
                parameters)).ToList();
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
            query.Append($"     @UserId,                  ");
            query.Append($"     @Category,                ");
            query.Append($"     @Description,             ");
            query.Append($"     @Value,                   ");
            query.Append($"     @TransactionDate,         ");
            query.Append($"     @CreatedAt                ");
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

