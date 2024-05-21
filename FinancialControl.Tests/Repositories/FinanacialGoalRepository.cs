using System;
using Infrastructure.Interfaces;
using Moq;
using System.Data;
using Infrastructure.Repositories;
using Dapper;
using Domain.Entities;
using Domain.Enums;

namespace FinancialControl.Tests.Repositories
{
	public class FinanacialGoalRepository
	{
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IDbConnection> dbConnection;
        private readonly IFinancialGoalRepository _financialGoalRepository;

        public FinanacialGoalRepository()
		{
            dbConnection = new Mock<IDbConnection>();
            _uow = new Mock<IUnitOfWork>();

            _uow.Setup(x => x.Connection).Returns(dbConnection.Object);

            _financialGoalRepository = new FinancialGoalRepository(_uow.Object);
        }

        [Theory]
        [InlineData(CategoryFinancialGoalType.Trip, 1)]
        [InlineData(CategoryFinancialGoalType.Default, 0)]
        public async Task WhenCalled_FindAsync_ShouldReturnOk(CategoryFinancialGoalType category, long id)
        {
            dbConnection.Setup(x => x.QueryAsync<HistoryExpenseEntity>(It.IsAny<string>(),
                It.IsAny<object>(), null, null, null)).ReturnsAsync(new List<HistoryExpenseEntity>
                {
                    new HistoryExpenseEntity
                    {
                        Value = 30
                    }

                });

            var result = await _financialGoalRepository.FindAsync(category, id);

            Assert.True(result.Select(x => x.TargetValue).First() == 30);
        }

        [Fact]
        public void WhenCalled_AddNewGoalAsync_ShouldReturnOk()
        {
            dbConnection.Setup(x => x.QueryAsync<HistoryExpenseEntity>(It.IsAny<string>(),
                It.IsAny<object>(), null, null, null)).ReturnsAsync(new List<HistoryExpenseEntity>
                {
                    new HistoryExpenseEntity
                    {
                        Value = 300
                    }

                });

            Func<Task> act = async () => await _financialGoalRepository.AddNewGoalAsync(It.IsAny<FinancialGoalEntity>());
            var result = act().IsCompleted;

            Assert.True(result);
        }
    }
}

