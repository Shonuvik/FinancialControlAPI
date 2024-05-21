using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Interfaces
{
    public interface IFinancialGoalRepository
    {
        Task<List<HistoryGoalEntity>> FindAsync(CategoryFinancialGoalType? category, long userId);
        Task AddNewGoalAsync(FinancialGoalEntity entity);
    }
}