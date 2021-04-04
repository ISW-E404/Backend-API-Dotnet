using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Repositories;
using OffiRent.API.Domain.Services;
using OffiRent.API.Domain.Services.Communications;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

       
        public OfficeService(IOfficeRepository officeRepository, IAccountRepository accountRepository, IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
        {
            _officeRepository = officeRepository;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<OfficeResponse> ActiveOffice(int providerId, int id)
        {
            var inactiveOffice = await _officeRepository.FindById(id);
            var offiprovider = await _accountRepository.GetSingleByIdAsync(providerId);

            if (offiprovider.IsPremium == false)
                return new OfficeResponse("This Account is not premium");
            else if (inactiveOffice == null)
                return new OfficeResponse("Office not found");
            else if (inactiveOffice.Status == true)
                return new OfficeResponse("Office is already active");

            inactiveOffice.Status = true;

            try
            {
                _officeRepository.Remove(inactiveOffice);
                await _unitOfWork.CompleteAsync();

                return new OfficeResponse(inactiveOffice);
            }
            catch (Exception ex)
            {
                return new OfficeResponse($"An error ocurred while deleting Office: {ex.Message}");
            }
        }

        public async Task<bool> AccountHasMoreThanXPosts(int x, Account account)
        {
            List<Office> accountOffices = new List<Office>(await _officeRepository.ListAccountOfficesAsync(account.Id));
            return (accountOffices.Count > x) ? true : false;

        }

        public async Task<OfficeResponse> DeleteAsync(int id)
        {
            var existingOffice = await _officeRepository.FindById(id);
            var reservationsList = await _reservationRepository.ListOfficeReservationsAsync(id);
            var activeReservation = reservationsList.FirstOrDefault(reservation => reservation.Status=="Active");

            if (existingOffice == null)
                return new OfficeResponse("Office not found");
            else if (activeReservation != null) //queda pendiente
                return new OfficeResponse("Office has active reservations");


            try
            {
                _officeRepository.Remove(existingOffice);
                await _unitOfWork.CompleteAsync();

                return new OfficeResponse(existingOffice);
            }
            catch (Exception ex)
            {
                return new OfficeResponse($"An error ocurred while deleting Office: {ex.Message}");
            }
        }

        public async Task<OfficeResponse> GetByIdAsync(int id)
        {
            var existingOffice = await _officeRepository.FindById(id);
            if (existingOffice == null)
                return new OfficeResponse("Office not found");
            return new OfficeResponse(existingOffice);
        }

        public async Task<IEnumerable<Office>> ListAsync()
        {
            return await _officeRepository.ListAsync();
        }

        public async Task<IEnumerable<Office>> ListByDistrictIdAsync(int districtId)
        {
            return await _officeRepository.ListByDistrictIdAsync(districtId);
        }

        public async Task<IEnumerable<Office>> ListByInactiveStatus(int providerId)
        {
            return await _officeRepository.ListByInactiveStatus(providerId);
        }

        public async Task<IEnumerable<Office>> ListByPriceEqualOrLowerThanAsync(int price)
        {
            return await _officeRepository.ListByPriceEqualOrLowerThanAsync(price);
        }

        public async Task<IEnumerable<Office>> ListByProviderIdAsync(int providerId)
        {
            return await _officeRepository.ListByProviderIdAsync(providerId);
        }

        public async Task<OfficeResponse> SaveStatusAsync(Office Office)
        {
            Office.Status = true;

            try
            {
                await _officeRepository.AddAsync(Office);
                await _unitOfWork.CompleteAsync();

                return new OfficeResponse(Office);
            }
            catch (Exception ex)
            {
                return new OfficeResponse(
                    $"An error ocurred while saving the office: {ex.Message}");
            }
        }
        public async Task<OfficeResponse> SaveAsync(int accountId, Office Office)
        {
            var existingservice = await _officeRepository.FindById(accountId);
            if (existingservice == null)
                return new OfficeResponse("Account Id not found");
            return new OfficeResponse(
                    $"An error ocurred while saving the office");
        }

        public async Task<OfficeResponse> SaveAsyncPrev(Office office)
        {
            if (office!= null)
            {
                office.Status = true;
            }
            try
            {
                int accountId = office.AccountId;
                var account = await _accountRepository.GetSingleByIdAsync(accountId);
                bool isPremium = account.IsPremium;

                if (isPremium)
                {         
                    if (AccountHasMoreThanXPosts(14, account).Result)
                    {
                        return new OfficeResponse("Your type of account cannot have more than 15 posts at the same time");
                    }
                } 
                else
                {
                    if (AccountHasMoreThanXPosts(0, account).Result)
                    {
                        return new OfficeResponse("Your type of account cannot have more than 1 post at the same time");
                    }
                }
                  
                await _officeRepository.AddAsync(office);
                await _unitOfWork.CompleteAsync();
                return new OfficeResponse(office);
            }
            catch (Exception ex)
            {
                return new OfficeResponse(
                    $"An error ocurred while saving the office: {ex.Message}");
            }
        }

        public async Task<OfficeResponse> UpdateAsync(int id, Office office)
        {
            var existingOffice = await _officeRepository.FindById(id);

            if (existingOffice == null)
                return new OfficeResponse("Office not found");
            existingOffice.Title = office.Title;
            existingOffice.Url = office.Url;
            existingOffice.Address = office.Address;
            existingOffice.Floor = office.Floor;
            existingOffice.Capacity = office.Capacity;
            existingOffice.AllowResource = office.AllowResource;
            existingOffice.Score = office.Score;
            existingOffice.Description = office.Description;
            existingOffice.Price = office.Price;
            existingOffice.Comment = office.Comment;
            existingOffice.Status = office.Status;
            existingOffice.DistrictId = office.DistrictId;
            existingOffice.Services = office.Services;

            try
            {
                _officeRepository.Update(existingOffice);
                await _unitOfWork.CompleteAsync();

                return new OfficeResponse(existingOffice);
            }
            catch (Exception ex)
            {
                return new OfficeResponse($"An error ocurred while deleting Office: {ex.Message}");
            }
        }

        public async Task<OfficeResponse> UpdateScoreAsync(int accountId, int officeId, Office office)
        {
            var existingAccount = await _accountRepository.GetSingleByIdAsync(accountId);
            var existingOffice = await _officeRepository.FindById(officeId);

            if (existingAccount == null)
                return new OfficeResponse("Account not found");
            if (existingOffice == null)
                return new OfficeResponse("Office not found");

            existingOffice.Score = office.Score;

            try
            {
                _officeRepository.Update(existingOffice);
                await _unitOfWork.CompleteAsync();

                return new OfficeResponse(existingOffice);
            }
            catch (Exception ex)
            {
                return new OfficeResponse($"An error ocurred while updating the score of the office: {ex.Message}");
            }
        }
    }
}
