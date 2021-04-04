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
    public class DepartamentService : IDepartamentService
    {
        private readonly IDepartamentRepository _departamentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DepartamentService(IDepartamentRepository departamentRepository, IUnitOfWork unitOfWork)
        {
            _departamentRepository = departamentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DepartamentResponse> DeleteAsync(int id)
        {
            var existingDepartament = await _departamentRepository.FindById(id);

            if (existingDepartament == null)
                return new DepartamentResponse("Departament not found");

            try
            {
                _departamentRepository.Remove(existingDepartament);
                await _unitOfWork.CompleteAsync();

                return new DepartamentResponse(existingDepartament);
            }
            catch (Exception ex)
            {
                return new DepartamentResponse($"An error ocurred while deleting departament: {ex.Message}");
            }
        }

        public async Task<DepartamentResponse> GetByIdAsync(int id)
        {
            var existingDepartament = await _departamentRepository.FindById(id);
            if (existingDepartament == null)
                return new DepartamentResponse("Departament not found");
            return new DepartamentResponse(existingDepartament);
        }
    

        public async Task<IEnumerable<Departament>> ListAsync()
        {
            return await _departamentRepository.ListAsync();
        }

        public async Task<IEnumerable<Departament>> ListByCountryIdAsync(int countryId)
        {
            return await _departamentRepository.ListByCountryIdAsync(countryId);
        }

        public async Task<DepartamentResponse> SaveAsync(Departament departament)
        {

            try
            {
                await _departamentRepository.AddAsync(departament);
                await _unitOfWork.CompleteAsync();

                return new DepartamentResponse(departament);
            }
                catch (Exception ex)
            {
                return new DepartamentResponse(
                    $"An error ocurred while saving the Departament: {ex.Message}");
            }
         }

        public async Task<DepartamentResponse> UpdateAsync(int id, Departament departament)
        {
            var existingDepartament = await _departamentRepository.FindById(id);

            if (existingDepartament == null)
                return new DepartamentResponse("Departament not found");

            existingDepartament.Name = departament.Name;

            try
            {
                _departamentRepository.Update(existingDepartament);
                await _unitOfWork.CompleteAsync();

                return new DepartamentResponse(existingDepartament);
            }
            catch (Exception ex)
            {
                return new DepartamentResponse($"An error ocurred while updating departament: {ex.Message}");
            }
        }
    }
}
