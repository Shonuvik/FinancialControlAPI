using System;
using System.Data;
using Dapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Moq;

namespace FinancialControl.Tests.Repositories
{
    public class FinancialControlRepositoryTests
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IDbConnection> dbConnection;
        private readonly IFinancialControlRepository _financialControlRepository;

        public FinancialControlRepositoryTests()
        {
            dbConnection = new Mock<IDbConnection>();
            _uow = new Mock<IUnitOfWork>();

            _uow.Setup(x => x.Connection).Returns(dbConnection.Object);

            _financialControlRepository = new FinancialControlRepository(_uow.Object);
        }

        [Theory]
        [InlineData(CategoryType.Monthly, 1)]
        [InlineData(CategoryType.Default, 0)]
        public async Task WhenCalled_FindAsync_ShouldReturnOk(CategoryType category, long id)
        {
            dbConnection.Setup(x => x.QueryAsync<HistoryExpenseEntity>(It.IsAny<string>(),
                It.IsAny<object>(), null, null, null)).ReturnsAsync(new List<HistoryExpenseEntity>
                {
                    new HistoryExpenseEntity
                    {
                        Value = 1000
                    }

                });

            var result = await _financialControlRepository.FindAsync(category, id);

            Assert.True(result.Select(x => x.Value).First() == 1000);
        }

        [Fact]
        public void WhenCalled_AddNewExpenseAsync_ShouldReturnOk()
        {
            dbConnection.Setup(x => x.QueryAsync<HistoryExpenseEntity>(It.IsAny<string>(),
                It.IsAny<object>(), null, null, null)).ReturnsAsync(new List<HistoryExpenseEntity>
                {
                    new HistoryExpenseEntity
                    {
                        Value = 1000
                    }

                });

            Func<Task> act = async () => await _financialControlRepository.AddNewExpenseAsync(It.IsAny<FinancialExpenseEntity>());
            var result = act().IsCompleted;

            Assert.True(result);
        }
    }
}

