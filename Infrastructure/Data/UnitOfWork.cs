using System.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbConnection _connection;

        public UnitOfWork(IDbConnection connection)
        {
            _connection = connection;
        }

        public IDbConnection Connection
        {
            get
            {
                _connection.Open();
                return _connection;
            }
        }
    }
}

