using System;
using System.Data;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        public IDbConnection Connection { get; }
    }
}

