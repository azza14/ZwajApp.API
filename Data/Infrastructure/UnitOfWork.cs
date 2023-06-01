using System;
using System.Threading.Tasks;
using ZwajApp.API.Data.DAL;
using ZwajApp.API.Data.Interfaces;
using ZwajApp.API.Data.Repositories;

namespace ZwajApp.API.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {

        public IUserRepository UserRepository { get; }

        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);  
        }
        protected virtual void Dispose(bool  disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

    }
}
