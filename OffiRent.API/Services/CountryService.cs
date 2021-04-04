using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Repositories;
using OffiRent.API.Domain.Services;
using OffiRent.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ICountryCurrencyRepository _countryCurrencyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CountryService(ICountryRepository countryRepository, ICountryCurrencyRepository countryCurrencyRepository,
            IUnitOfWork unitOfWork)
        {
            _countryRepository = countryRepository;
            _countryCurrencyRepository = countryCurrencyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CountryResponse> DeleteAsync(int id)
        {
            var existingCountry = await _countryRepository.GetSingleByIdAsync(id);

            if (existingCountry == null)
                return new CountryResponse("Country not found");

            try
            {
                _countryRepository.Remove(existingCountry);
                await _unitOfWork.CompleteAsync();

                return new CountryResponse(existingCountry);
            }
            catch (Exception ex)
            {
                return new CountryResponse($"An error ocurred while deleting country: {ex.Message}");
            }
        }

        public async Task<CountryResponse> GetBySingleIdAsync(int id)
        {
            var existingCountry = await _countryRepository.GetSingleByIdAsync(id);
            if (existingCountry == null)
                return new CountryResponse("Country not found");
            return new CountryResponse(existingCountry);
        }

        public async Task<IEnumerable<Country>> ListAsync()
        {
            return await _countryRepository.ListAsync();
        }

        public async Task<CountryResponse> SaveAsync(Country country)
        {
            try
            {
                await _countryRepository.AddAsync(country);
                await _unitOfWork.CompleteAsync();

                return new CountryResponse(country);
            }
            catch (Exception ex)
            {
                return new CountryResponse(
                    $"An error ocurred while saving the Country: {ex.Message}");
            }
        }
    }
}
