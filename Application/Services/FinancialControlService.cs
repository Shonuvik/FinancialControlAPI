using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class FinancialControlService : IFinancialControlService
    {
        private readonly IFinancialControlRepository _financialControlRepository;
        private readonly IUserRepository _userRepository;

        public FinancialControlService(IFinancialControlRepository financialControlRepository,
                                       IUserRepository userRepository)
        {
            _financialControlRepository = financialControlRepository;
            _userRepository = userRepository;
        }

        public async Task NewExpense(FinancialExpenseDto request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var id = await _userRepository.GetUserIdByName(request.UserName)
                ?? throw new Exception("Usuario inválido");

            var entity = ParseToEntity(request, id);

            await _financialControlRepository.AddNewExpenseAsync(entity);
        }

        private FinancialExpenseEntity ParseToEntity(FinancialExpenseDto request, long userId)
            => new(
                userId,
                request.CategoryType,
                request.Value,
                request.Description,
                request.TransactionDate);
    }
}

