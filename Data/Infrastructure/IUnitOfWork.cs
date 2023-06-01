using System;
using System.Threading.Tasks;
using ZwajApp.API.Data.Interfaces;

namespace ZwajApp.API.Data.Infrastructure
{
    public interface IUnitOfWork :IDisposable
    {
        IUserRepository UserRepository { get; } 
        Task Commit();
    }
}
