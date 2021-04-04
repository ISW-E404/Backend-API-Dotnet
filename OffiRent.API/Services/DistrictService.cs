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
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DistrictService(IDistrictRepository districtRepository, IUnitOfWork unitOfWork)
        {
            _districtRepository = districtRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DistrictResponse> DeleteAsync(int id)
        {
            var existingDistrict = await _districtRepository.FindById(id);

            if (existingDistrict == null)
                return new DistrictResponse("District not found");

            try
            {
                _districtRepository.Remove(existingDistrict);
                await _unitOfWork.CompleteAsync();

                return new DistrictResponse(existingDistrict);
            }
            catch (Exception ex)
            {
                return new DistrictResponse($"An error ocurred while deleting District: {ex.Message}");
            }
        }

        public async Task<DistrictResponse> GetByIdAsync(int id)
        {
            var existingDistrict = await _districtRepository.FindById(id);
            if (existingDistrict == null)
                return new DistrictResponse("District not found");
            return new DistrictResponse(existingDistrict);
        }

        public async Task<IEnumerable<District>> ListAsync()
        {
            return await _districtRepository.ListAsync();
        }

        public async Task<IEnumerable<District>> ListByDepartamentIdAsync(int departamentId)
        {
            return await _districtRepository.ListByDepartamentIdAsync(departamentId);
        }

        public async Task<DistrictResponse> SaveAsync(District district)
        {
            try
            {
                await _districtRepository.AddAsync(district);
                await _unitOfWork.CompleteAsync();

                return new DistrictResponse(district);
            }
            catch (Exception ex)
            {
                return new DistrictResponse(
                    $"An error ocurred while saving the District: {ex.Message}");
            }
        }

        public async Task<DistrictResponse> UpdateAsync(int id, District district)
        {
            var existingDistrict = await _districtRepository.FindById(id);

            if (existingDistrict == null)
                return new DistrictResponse("District not found");

            existingDistrict.Name = district.Name;

            try
            {
                _districtRepository.Update(existingDistrict);
                await _unitOfWork.CompleteAsync();

                return new DistrictResponse(existingDistrict);
            }
            catch (Exception ex)
            {
                return new DistrictResponse($"An error ocurred while updating district: {ex.Message}");
            }
        }
    }
}
