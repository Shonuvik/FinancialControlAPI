using System.Text;
using Dapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class FinancialGoalRepository : IFinancialGoalRepository
    {
        private readonly IUnitOfWork _uow;

        public FinancialGoalRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<HistoryGoalEntity>> FindAsync(CategoryFinancialGoalType? category, long userId)
        {
            StringBuilder query = new();
            DynamicParameters parameters = new();

            query.Append($" SELECT                                                               ");
            query.Append($"     Category        AS {nameof(HistoryExpenseEntity.Category)},      ");
            query.Append($"     Description     AS {nameof(HistoryExpenseEntity.Description)},   ");
            query.Append($"     Value           AS {nameof(HistoryExpenseEntity.Value)},         ");
            query.Append($"     TransactionDate AS {nameof(HistoryExpenseEntity.TransactionDate)}");
            query.Append($" FROM [FinancialGoal]                                                 ");
            query.Append($" WHERE 1=1                                                            ");

            if (userId != 0)
            {
                query.Append($" AND UserId = @UserId  ");
                parameters.Add("@UserId", userId);
            }

            if (category != CategoryFinancialGoalType.Default)
            {
                query.Append($" AND Category = @Category  ");
                parameters.Add("@Category", category);
            }

            using var connection = _uow.Connection;
            return (await connection.QueryAsync<HistoryGoalEntity>(
                query.ToString(),
                parameters)).ToList();
        }

        public async Task AddNewGoalAsync(FinancialGoalEntity entity)
        {
            StringBuilder query = new();

            query.Append($" INSERT INTO[FinancialGoal]      ");
            query.Append($" (                               ");
            query.Append($"     UserId,                     ");
            query.Append($"     Category,                   ");
            query.Append($"     ActualValue,                ");
            query.Append($"     TargetValue,                ");
            query.Append($"     TargetDate,                 ");
            query.Append($"     Description,                ");
            query.Append($"     CreatedAt                   ");
            query.Append($" )                               ");
            query.Append($" VALUES(                         ");
            query.Append($"     @UserId,                  ");
            query.Append($"     @Category,                ");
            query.Append($"     @ActualValue,             ");
            query.Append($"     @TargetValue,             ");
            query.Append($"     @TargetDate,              ");
            query.Append($"     @Description,              ");
            query.Append($"     @CreatedAt                ");
            query.Append($" );                              ");

            DynamicParameters parameters = new();
            parameters.Add("@UserId", entity.UserId);
            parameters.Add("@Category", entity.Category);
            parameters.Add("@ActualValue", entity.ActualValue);
            parameters.Add("@TargetValue", entity.TargetValue);
            parameters.Add("@TargetDate", entity.TargetDate);
            parameters.Add("@Description", entity.Description);
            parameters.Add("@CreatedAt", entity.CreatedAt);

            using var connection = _uow.Connection;
            await connection.ExecuteAsync(query.ToString(), parameters);
        }
    }
}

