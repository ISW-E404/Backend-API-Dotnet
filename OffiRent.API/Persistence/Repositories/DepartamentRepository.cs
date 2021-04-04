using Microsoft.EntityFrameworkCore;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Persistence.Contexts;
using OffiRent.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Persistence.Repositories
{
    public class DepartamentRepository : BaseRepository, IDepartamentRepository
    {
        public DepartamentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Departament departament)
        {
            await _context.Departaments.AddAsync(departament);
        }

        public async Task<Departament> FindById(int id)
        {
            return await _context.Departaments.FindAsync(id);
        }

        public async Task<IEnumerable<Departament>> ListAsync()
        {
            return await _context.Departaments.Include(p => p.Country).ToListAsync();
        }

        public async Task<IEnumerable<Departament>> ListByCountryIdAsync(int countryId)
        {
            return await _context.Departaments
                .Where(p => p.CountryId == countryId)
                .Include(p => p.Country)
                .ToListAsync();
        }

        public void Remove(Departament departament)
        {
            _context.Departaments.Remove(departament);
        }

        public void Update(Departament departament)
        {
            _context.Departaments.Update(departament);
        }
    }
}
