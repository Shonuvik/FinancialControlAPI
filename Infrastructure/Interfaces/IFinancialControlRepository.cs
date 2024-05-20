using System;
using Domain.Entities;

namespace Infrastructure.Interfaces
{
	public interface IFinancialControlRepository
	{
        Task<FinancialExpenseEntity> AddNewExpenseAsync(FinancialExpenseEntity entity);
    }
}

