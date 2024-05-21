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
    public class FinancialServiceTests
    {
        private readonly Mock<IFinancialControlRepository> _financialControlRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IFinancialControlService _financialControlService;

        public FinancialServiceTests()
        {
            _financialControlRepositoryMock = new Mock<IFinancialControlRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();

            _financialControlService = new FinancialControlService(_financialControlRepositoryMock.Object,
                                                                   _userRepositoryMock.Object);
        }

        [Fact]
        public void WhenCalled_NewExpense_ShouldReturnOk()
        {
            _userRepositoryMock.Setup(x => x.GetUserIdByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(12);

            _financialControlRepositoryMock.Setup(x => x.AddNewExpenseAsync(It.IsAny<FinancialExpenseEntity>()));

            Func<Task> act = async () => await _financialControlService.NewExpenseAsync(new FinancialExpenseDto(),
                                                    It.IsAny<string>(),
                                                    It.IsAny<CategoryType>());
            var result = act().IsCompleted;

            Assert.True(result);
        }

        [Fact]
        public void WhenCalled_NewExpenseWithRequestEmpty_ShouldThrowException()
        {
            _userRepositoryMock.Setup(x => x.GetUserIdByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(12);

            _financialControlRepositoryMock.Setup(x => x.AddNewExpenseAsync(It.IsAny<FinancialExpenseEntity>()));

            Assert.ThrowsAsync<Exception>(async () => await _financialControlService.NewExpenseAsync(It.IsAny<FinancialExpenseDto>(),
                                                    It.IsAny<string>(),
                                                    It.IsAny<CategoryType>()));
        }
    }
}

