using System;
using System.Threading.Tasks;
using OffiRent.API.Domain.Persistence.Contexts;
using OffiRent.API.Domain.Repositories;

namespace OffiRent.API.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
