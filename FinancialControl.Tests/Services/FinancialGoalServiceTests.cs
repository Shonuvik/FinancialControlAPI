using System;
using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Interfaces;
using Moq;

namespace FinancialControl.Tests.Services
{
    public class FinancialGoalServiceTests
    {
        private readonly IFinancialGoalService _financialGoalService;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IFinancialGoalRepository> _financialGoalRepositoryMock;

        public FinancialGoalServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _financialGoalRepositoryMock = new Mock<IFinancialGoalRepository>();

            _financialGoalService = new FinancialGoalService(_financialGoalRepositoryMock.Object,
                                                             _userRepositoryMock.Object);
        }

        [Fact]
        public void WhenCalled_NewExpense_ShouldReturnOk()
        {
            _userRepositoryMock.Setup(x => x.GetUserIdByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(12);

            _financialGoalRepositoryMock.Setup(x => x.AddNewGoalAsync(It.IsAny<FinancialGoalEntity>()));

            Func<Task> act = async () => await _financialGoalService.AddNewGoalAsync(new FinancialGoalDto(),
                                                    It.IsAny<string>(),
                                                    It.IsAny<CategoryFinancialGoalType>());
            var result = act().IsCompleted;

            Assert.True(result);
        }

        [Theory]
        [InlineData(0, 1, 0)]
        [InlineData(100, 0, 0)]
        [InlineData(100, 100, 0)]
        public void WhenCalled_NewExpenseWithValueLessThan_ShouldThrowException(decimal value, decimal targetValue, long userId)
        {
            var entity = new FinancialGoalEntity();
            entity.TargetValue = targetValue;
            entity.UserId = userId;
            entity.ActualValue = value;

            _userRepositoryMock.Setup(x => x.GetUserIdByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(12);

            _financialGoalRepositoryMock.Setup(x => x.AddNewGoalAsync(entity));

            Assert.ThrowsAsync<Exception>(async () => await _financialGoalService.AddNewGoalAsync(new FinancialGoalDto(),
                                                    It.IsAny<string>(),
                                                    It.IsAny<CategoryFinancialGoalType>()));
        }

        [Fact]
        public void WhenCalled_NewExpenseWithRequestEmpty_ShouldThrowException()
        {
            _userRepositoryMock.Setup(x => x.GetUserIdByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(12);

            _financialGoalRepositoryMock.Setup(x => x.AddNewGoalAsync(It.IsAny<FinancialGoalEntity>()));

            Assert.ThrowsAsync<Exception>(async () => await _financialGoalService.AddNewGoalAsync(It.IsAny<FinancialGoalDto>(),
                                                    It.IsAny<string>(),
                                                    It.IsAny<CategoryFinancialGoalType>()));
        }
    }
}

