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
    public class DistrictRepository : BaseRepository, IDistrictRepository
    {
        public DistrictRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(District district)
        {
            await _context.Districts.AddAsync(district);
        }

        public async Task<District> FindById(int id)
        {
            return await _context.Districts.FindAsync(id);
        }

        public async Task<IEnumerable<District>> ListAsync()
        {
            return await _context.Districts.Include(d=>d.Offices).ToListAsync();
        }

       /* public async Task<IEnumerable<District>> ListByCountryIdAsync(int countryId)
        {
            var departaments= await _context.Departaments
                .Where(p => p.CountryId == countryId)
                .Include(p => p.Country)
                .ToListAsync();

            var departamentsId = departaments.Select(d=>d.Id);

            var districts = await _context.Districts
                
            //throw new NotImplementedException();
        }*/

        public async Task<IEnumerable<District>> ListByDepartamentIdAsync(int departamentId)
        {
            return await _context.Districts
                .Where(p => p.DepartamentId == departamentId)
                .Include(p => p.Departament)
                .ToListAsync();
        }

        public void Remove(District district)
        {
            _context.Districts.Remove(district);
        }

        public void Update(District district)
        {
            _context.Districts.Update(district);
        }
    }
}
