using System;
using System.Threading.Tasks;
using ArQr.Models.Repositories;

namespace ArQr.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationUserRepository Users    { get; }
        IProductRepository         Products { get; }

        Task<int> Complete();
    }
}