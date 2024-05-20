﻿using System;
namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<long?> GetUserIdByName(string userName);
    }
}

