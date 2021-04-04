using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Repositories;
using OffiRent.API.Domain.Services;
using OffiRent.API.Domain.Services.Communications;

namespace OffiRent.API.Services
{
    public class AccountPaymentMethodService : IAccountPaymentMethodService
    {
        private readonly IAccountPaymentMethodRepository _accountPaymentMethodRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AccountPaymentMethodService(IAccountPaymentMethodRepository accountPaymentMethodRepository, IUnitOfWork unitOfWork)
        {
            _accountPaymentMethodRepository = accountPaymentMethodRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AccountPaymentMethodResponse> AssignPaymentMethodAsync(int accountId, int paymentMethodId)
        {
            try
            {
                await _accountPaymentMethodRepository.AssignAccountPaymentMethod(accountId, paymentMethodId);
                await _unitOfWork.CompleteAsync();
                AccountPaymentMethod accountPaymentMethod = await _accountPaymentMethodRepository.FindByAccountIdAndPaymentMethodId(accountId, paymentMethodId);
                return new AccountPaymentMethodResponse(accountPaymentMethod);
            }
            catch (Exception ex)
            {
                return new AccountPaymentMethodResponse($"An error ocurred while assigning Account and Payment Method: {ex.Message}");
            }
        }

        public Task<IEnumerable<PaymentMethod>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PaymentMethod>> ListByProductIdAsync(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PaymentMethod>> ListByTagIdAsync(int paymentMethodId)
        {
            throw new NotImplementedException();
        }

        public Task<AccountPaymentMethodResponse> UnassignPaymentMethodAsync(int accountId, int paymentMethodId)
        {
            throw new NotImplementedException();
        }
    }
}
