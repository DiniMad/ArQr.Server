using System;
using System.Threading.Tasks;

namespace Data.Repository.Base
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IUserRepository UserRepository { get; }

        public Task CompleteAsync();
    }
}