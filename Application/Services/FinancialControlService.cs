using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
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

        public async Task<List<HistoryExpenseDto>> FindHistoryAsync(CategoryType categoryType, string userName)
        {
            var userId = await _userRepository.GetUserIdByNameAsync(userName)
                ?? throw new Exception("Usuário inválido.");

            var historyList = await _financialControlRepository.FindAsync(categoryType, userId);

            var entitylistToDto = Map(historyList);
            return entitylistToDto;
        }

        public async Task NewExpenseAsync(FinancialExpenseDto request, string userName, CategoryType categoryType)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var id = await _userRepository.GetUserIdByNameAsync(userName)
                ?? throw new Exception("Usuário inválido");

            var entity = ParseToEntity(request, id, categoryType);

            await _financialControlRepository.AddNewExpenseAsync(entity);
        }

        private FinancialExpenseEntity ParseToEntity(FinancialExpenseDto request, long userId, CategoryType categoryType)
            => new(
                userId,
                categoryType,
                request.Value,
                request.Description,
                request.TransactionDate);

        private List<HistoryExpenseDto> Map(List<HistoryExpenseEntity> entityList)
        {
            List<HistoryExpenseDto> historyList = new();
            foreach(var item in entityList)
            {
                historyList.Add(new HistoryExpenseDto
                {
                    Category = item.Category,
                    Description = item.Description,
                    TransactionDate = item.TransactionDate,
                    Value = item.Value
                });
            }

            return historyList;
        }
    }
}

