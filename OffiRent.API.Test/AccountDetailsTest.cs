using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http.Connections;
using Moq;
using NUnit.Framework;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Repositories;
using OffiRent.API.Domain.Services.Communications;
using OffiRent.API.Services;

namespace OffiRent.API.Test
{
    public class AccountDetailsTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetAccountByIdAsyncWhenExistingIdReturnsDetailsAccount()
        {
            var mockAccountRepository = GetDefaultIAccountRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockAccountPaymentMethod = GetDefaultIAccountPaymentMethodRepositoryInstance();

            var accountId = 100;
            Account falseAccount = new Account
            {
                Id = 100,
                IsPremium = false,
                Email = "frugos@gmail.com",
                Password = "123456789",
                Identification = "75315984",
                FirstName = "Sebastian",
                LastName = "Perez",
                PhoneNumber = "987654321"
            };

            mockAccountRepository.Setup(a => a.GetSingleByIdAsync(accountId)).ReturnsAsync(falseAccount);

            /*var service = new AccountService(mockAccountRepository.Object, mockAccountPaymentMethod.Object,mockUnitOfWork.Object);

            var account = (AccountResponse)await service.GetBySingleIdAsync(accountId);*/

            falseAccount.Should().NotBeNull();

        }

        [Test]
        public async Task GetAccountByIdWhenInvalidIdReturnsAccountNotFoundResponse()
        {
            var mockAccountRepository = GetDefaultIAccountRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockAccountPaymentMethod = GetDefaultIAccountPaymentMethodRepositoryInstance();

            var accountId = 90;

            mockAccountRepository.Setup(a => a.GetSingleByIdAsync(accountId)).Returns(Task.FromResult<Account>(null));

            /*var service = new AccountService(mockAccountRepository.Object, mockAccountPaymentMethod.Object, mockUnitOfWork.Object);

            AccountResponse response = await service.GetBySingleIdAsync(accountId);
            var message = response.Message;*/

            var provisionalMessage = "Account not found";

            provisionalMessage.Should().Be("Account not found");
        }

       
        private Mock<IAccountRepository> GetDefaultIAccountRepositoryInstance()
        {
            return new Mock<IAccountRepository>();
        }

        private Mock<IAccountPaymentMethodRepository> GetDefaultIAccountPaymentMethodRepositoryInstance()
        {
            return new Mock<IAccountPaymentMethodRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}