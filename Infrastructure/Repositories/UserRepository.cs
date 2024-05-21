using System;
using Dapper;
using System.Text;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _uow;

        public UserRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<long?> GetUserIdByNameAsync(string userName)
        {
            StringBuilder query = new();

            query.Append($" SELECT            ");
            query.Append($"        Id      ");
            query.Append($" FROM [User]       ");
            query.Append($" WHERE Name = @Name");

            using var connection = _uow.Connection;
            var credentials = await connection.QueryFirstOrDefaultAsync<long>(query.ToString(),
                new
                {
                    Name = userName
                });

            return credentials;
        }
    }
}

