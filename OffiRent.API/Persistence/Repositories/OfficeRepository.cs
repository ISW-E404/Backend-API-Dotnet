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
    public class OfficeRepository : BaseRepository, IOfficeRepository
    {
        public OfficeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Office office)
        {
            await _context.Offices.AddAsync(office);
        }

        public async Task<Office> FindById(int id)
        {
            return await _context.Offices.FindAsync(id);
        }

        public async Task<IEnumerable<Office>> ListAccountOfficesAsync(int accountId)
        {
            return await _context.Offices.Where(o => o.AccountId == accountId).ToListAsync();
        }

        public async Task<IEnumerable<Office>> ListAsync()
        {
            //return await _context.Offices.Include(p =>p.District).Include(p=>p.Publication).ToListAsync();

            return await _context.Offices
                .OrderByDescending(p => p.Account.IsPremium)
                .Where(p => p.Status == true).Include(o => o.Services)
                .ToListAsync();
        }

        public async Task<IEnumerable<Office>> ListByDistrictIdAsync(int districtId)
        {
            return await _context.Offices

                .OrderByDescending(p => p.Account.IsPremium)
                .Where(p => p.DistrictId == districtId  && p.Status==true )
                .Include(p => p.District)

                .Where(p => p.DistrictId == districtId)

                .ToListAsync();
        }

        public async Task<IEnumerable<Office>> ListByInactiveStatus(int providerId)
        {
            return await _context.Offices
                .Where(p => p.AccountId == providerId && p.Status == false)
                .ToListAsync();
        }



        public async Task<IEnumerable<Office>> ListByPriceEqualOrLowerThanAsync(int price)
        {
            return await _context.Offices
                .OrderByDescending(p => p.Account.IsPremium)
                .Where(p => p.Price <= price && p.Status == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Office>> ListByProviderIdAsync(int providerId)
        {
            return await _context.Offices
                .Where(p => p.AccountId == providerId && p.Status == true)
                .ToListAsync();
        }

        public void Remove(Office office)
        {
            _context.Offices.Remove(office);
        }

        public void Update(Office office)
        {
            _context.Offices.Update(office);
        }
    }
}
