using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Moq;
using NUnit.Framework;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Repositories;
using OffiRent.API.Domain.Services.Communications;
using OffiRent.API.Services;

namespace OffiRent.API.Test
{
    public class RateOfficeTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task UpdateOfficeScoreAsyncWhenExistingAccountIdReturnsDetailsOffice()
        {
            var mockAccountRepository = GetDefaultIAccountRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockOfficeRepository = GetDefaultIOfficeRepositoryInstance();
            var mockReservationRepository = GetDefaultReservationRepositoryInstance();
            var service = new OfficeService(mockOfficeRepository.Object, mockAccountRepository.Object, mockReservationRepository.Object,mockUnitOfWork.Object);

            var accountId = 100;
            var officeId = 100;

            Account account = new Account
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

            List<Office> offices = new List<Office>();

            Office office = new Office
            {
                Id = 100,
                Score = 4,
                AccountId = 100
            };
            

            mockAccountRepository.Setup(a => a.GetSingleByIdAsync(accountId)).ReturnsAsync(account);
            //mockOfficeRepository.Setup(a => a.Update(office)).Callback(()=>Task.FromResult<Office>(office));

            //List<Office> offices1 = (List<Office>)await service.UpdateScoreAsync(accountId, officeId,office.Score);

            //offices1.Should().NotBeEmpty();

        }

        [Test]
        public async Task UpdateOfficeScoreAsyncWhenReciviesNullObjectReturnsException()
        {
            var mockAccountRepository = GetDefaultIAccountRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockOfficeRepository = GetDefaultIOfficeRepositoryInstance();
            var mockReservationRepository = GetDefaultReservationRepositoryInstance();

            var service = new OfficeService(mockOfficeRepository.Object, mockAccountRepository.Object, mockReservationRepository.Object,mockUnitOfWork.Object);

            Account account = new Account
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

            Office nullOffice = new Office
            { 
               
            };

            mockAccountRepository.Setup(a => a.GetSingleByIdAsync(account.Id)).ReturnsAsync(account);
            mockOfficeRepository.Setup(a => a.Update(nullOffice)).Callback(()=>Task.FromResult<Office>(null));

            //var response = service.UpdateScoreAsync(nullOffice).Result;

            //Assert.AreEqual("An error ocurred while saving the score of the office:", response.Message);
        }


        private Mock<IAccountRepository> GetDefaultIAccountRepositoryInstance()
        {
            return new Mock<IAccountRepository>();
        }

        private Mock<IOfficeRepository> GetDefaultIOfficeRepositoryInstance()
        {
            return new Mock<IOfficeRepository>();
        }

        private Mock<IReservationRepository> GetDefaultReservationRepositoryInstance()
        {
            return new Mock<IReservationRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}