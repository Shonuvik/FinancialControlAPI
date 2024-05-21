using Application.DTOs;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface IFinancialGoalService
    {
        Task<List<HistoryGoalDto>> FindProgressAsync(CategoryFinancialGoalType categoryType, string userName);
        Task AddNewGoalAsync(FinancialGoalDto request, string userName, CategoryFinancialGoalType category);
    }
}

