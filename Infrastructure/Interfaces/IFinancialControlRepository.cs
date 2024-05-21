using System;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Interfaces
{
	public interface IFinancialControlRepository
	{
        Task<List<HistoryExpenseEntity>> FindAsync(CategoryType category, long userId);

        Task<FinancialExpenseEntity> AddNewExpenseAsync(FinancialExpenseEntity entity);
    }
}

