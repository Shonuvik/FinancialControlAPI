using System;
namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<long?> GetUserIdByNameAsync(string userName);
    }
}

