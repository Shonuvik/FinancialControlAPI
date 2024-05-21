using System;
using Application.DTOs;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface IFinancialControlService
    {
        Task<List<HistoryExpenseDto>> FindHistoryAsync(CategoryType categoryType, string userName);
        Task NewExpenseAsync(FinancialExpenseDto request, string userName, CategoryType categoryType);
    }
}

