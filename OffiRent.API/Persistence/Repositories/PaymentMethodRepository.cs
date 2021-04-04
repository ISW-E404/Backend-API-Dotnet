using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Persistence.Contexts;
using OffiRent.API.Domain.Repositories;

namespace OffiRent.API.Persistence.Repositories
{
    public class PaymentMethodRepository : BaseRepository, IPaymentMethodRepository
    {
        public PaymentMethodRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PaymentMethod> GetSingleByIdAsync(int id)
        {
            return await _context.PaymentMethods.FindAsync(id);
        }

        public void Remove(PaymentMethod paymentMethod)
        {
            _context.Remove(paymentMethod);
        }

        public async Task<IEnumerable<PaymentMethod>> ListAsync()
        {
            return await _context.PaymentMethods.ToListAsync();
        }
    }
}
