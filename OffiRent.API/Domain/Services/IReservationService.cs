using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> ListAsync();
        Task<IEnumerable<Reservation>> ListByAccountIdAsync(int accountId, [Optional] string status);
        Task<IEnumerable<Reservation>> ListByOfficeIdAsync(int officeId, [Optional] string status);
        Task<ReservationResponse> GetByIdAsync(int id);
        Task<ReservationResponse> SaveAsync(int accountId, Reservation reservation);
        Task<ReservationResponse> UpdateAsync(int id, Reservation reservation);
        Task<ReservationResponse> SetStatus(int id, [Optional] string status);
        Task<ReservationResponse> DeleteAsync(int id);
        //Task<ReservationResponse> ActiveReservation(int accountId, int id);
        Task<bool> AccountHasReservation(int x, Account account);
    }
}
