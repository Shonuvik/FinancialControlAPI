using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class FinancialGoalService : IFinancialGoalService
    {
        private readonly IFinancialGoalRepository _financialGoalRepository;
        private readonly IUserRepository _userRepository;

        public FinancialGoalService(IFinancialGoalRepository financialGoalRepository,
                                    IUserRepository userRepository)
        {
            _financialGoalRepository = financialGoalRepository;
            _userRepository = userRepository;
        }

        public async Task<List<HistoryGoalDto>> FindProgressAsync(CategoryFinancialGoalType categoryType, string userName)
        {
            var userId = await _userRepository.GetUserIdByNameAsync(userName)
                        ?? throw new Exception("Usuário inválido.");

            var historyList = await _financialGoalRepository.FindAsync(categoryType, userId);

            var entitylistToDto = Map(historyList);
            return entitylistToDto;
        }

        public async Task AddNewGoalAsync(FinancialGoalDto request, string userName, CategoryFinancialGoalType category)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var userId = await _userRepository.GetUserIdByNameAsync(userName)
                ?? throw new ExceptionBusiness("Usuário inválido");

            var entity = ParseToEntity(request, userId, category);

            await _financialGoalRepository.AddNewGoalAsync(entity);
        }

        private FinancialGoalEntity ParseToEntity(FinancialGoalDto request, long userId, CategoryFinancialGoalType category)
        {
            return new FinancialGoalEntity(userId,
                                           category,
                                           request.ActualValue,
                                           request.TargetValue,
                                           request.TargetDate,
                                           request.Description);
        }

        private List<HistoryGoalDto> Map(List<HistoryGoalEntity> entityList)
        {
            string message = string.Empty;
            List<HistoryGoalDto> historyList = new();
            foreach (var item in entityList)
            {
                message = $"Valor Atual: {item.ActualValue} --> Valor Planejado? {item.TargetValue}";
                decimal missingPercentage = (item.ActualValue / item.TargetValue) * 100 - 100;
                historyList.Add(new HistoryGoalDto
                {
                    Category = item.Category,
                    ActualValue = item.ActualValue,
                    TargetValue = item.TargetValue,
                    TargetDate = item.TargetDate,
                    PercentageProgress = item.ActualValue >= item.TargetValue
                    ? $"Você já atingiu o valor planejado {message}"
                    : $"Faltam %{missingPercentage} para atingir a sua meta",
                    Description = item.Description,
                });
            }

            return historyList;
        }
    }
}

