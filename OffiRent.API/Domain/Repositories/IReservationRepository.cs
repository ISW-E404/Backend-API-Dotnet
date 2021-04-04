using OffiRent.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> ListAsync();
        Task<IEnumerable<Reservation>> ListAccountReservationsAsync(int accountId, [Optional] string status);
        Task<IEnumerable<Reservation>> ListOfficeReservationsAsync(int accountId, [Optional] string status);
        Task AddAsync(Reservation reservation);
        Task<Reservation> FindById(int Id);
        void Update(Reservation reservation);
        void Remove(Reservation reservation);
    }
}
