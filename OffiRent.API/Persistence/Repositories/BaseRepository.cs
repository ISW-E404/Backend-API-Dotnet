using System;
using OffiRent.API.Domain.Persistence.Contexts;

namespace OffiRent.API.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly AppDbContext _context;

        protected BaseRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
