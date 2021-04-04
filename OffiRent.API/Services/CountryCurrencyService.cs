using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Repositories;
using OffiRent.API.Domain.Services;
using OffiRent.API.Domain.Services.Communications;
using OffiRent.API.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Services
{
    public class CountryCurrencyService : ICountryCurrencyService
    {
        private readonly ICountryCurrencyRepository _countryCurrencyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CountryCurrencyService(ICountryCurrencyRepository countryCurrencyRepository, IUnitOfWork unitOfWork)
        {
            _countryCurrencyRepository = countryCurrencyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CountryCurrencyResponse> AssignCountryCurrencyAsync(int countryId, int currencyId)
        {
            try
            {
                await _countryCurrencyRepository.AssignCountryCurrency(countryId, currencyId);
                await _unitOfWork.CompleteAsync();
                CountryCurrency countryCurrency = await _countryCurrencyRepository
                    .FindByCountryIdAndCurrencyId(countryId, currencyId);
                return new CountryCurrencyResponse(countryCurrency);
            }
            catch (Exception ex)
            {
                return new CountryCurrencyResponse($"An error ocurred while assigning Country and Currency: {ex.Message}");
            }
         }

        public async Task<IEnumerable<CountryCurrency>> ListAsync()
        {
            return await _countryCurrencyRepository.ListAsync();
        }

        public async Task<IEnumerable<CountryCurrency>> ListByCountryIdAsync(int countryId)
        {
            return await _countryCurrencyRepository.ListByCountryIdAsync(countryId);
        }

        public async Task<IEnumerable<CountryCurrency>> ListByCurrencyIdAsync(int currencyId)
        {
            return await _countryCurrencyRepository.ListByCurrencyIdAsync(currencyId);
        }

       public async Task<CountryCurrencyResponse> UnassignCountryCurrencyAsync(int countryId, int currencyId)
        {
            try
            {
                CountryCurrency countryCurrency = await _countryCurrencyRepository.FindByCountryIdAndCurrencyId(countryId, currencyId);
                _countryCurrencyRepository.Remove(countryCurrency);
                await _unitOfWork.CompleteAsync();
                return new CountryCurrencyResponse(countryCurrency);
            }
            catch (Exception ex)
            {
                return new CountryCurrencyResponse($"An error ocurred while assigning Currency to Country: {ex.Message}");
            }
        }

    }
}
