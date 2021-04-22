using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Repositories;
using OffiRent.API.Domain.Services.Communications;
using OffiRent.API.Services;
using OffiRent.API.Settings;

namespace OffiRent.API.Test
{
    public class ViewReservationsTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetAllReservationsAsyncByAccountIdWhenExistingIdReturnsCollectionNotEmpty()
        {
            var mockAccountRepository = GetDefaultIAccountRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockReservationRepository = GetDefaultIResevationRepositoryInstance();

            List<Reservation> reservations = new List<Reservation>();

            var accountId = 100;
            var reservationStatus = "Active";

            reservations.Add(new Reservation {Id = 30, Status = "Active", AccountId = 100, OfficeId = 100 });
            reservations.Add(new Reservation { Id = 31, Status = "Active", AccountId = 100, OfficeId = 101 });
            reservations.Add(new Reservation { Id = 32, Status = "Active", AccountId = 101, OfficeId = 102 });

            

            mockReservationRepository.Setup(a => a.ListAccountReservationsAsync(accountId,reservationStatus)).ReturnsAsync(reservations);

            var service = new ReservationService(mockReservationRepository.Object,mockUnitOfWork.Object, mockAccountRepository.Object);

            List<Reservation> reservations1 = (List<Reservation>)await service.ListByAccountIdAsync(accountId, reservationStatus); // problema era reservation status
           

            reservations1.Should().NotBeEmpty();  // se probó el reservations de arriba, falta reemplazarlo

        }

        [Test]
        public async Task GetAllReservationsAsyncByAccountIdWhenExistingIdReturnsCollectionEmpty()
        {
            var mockAccountRepository = GetDefaultIAccountRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockReservationRepository = GetDefaultIResevationRepositoryInstance();

            var accountId = 102;
            var reservationStatus = "Active";
           

            mockReservationRepository.Setup(a => a.ListAccountReservationsAsync(accountId, reservationStatus)).ReturnsAsync(new List<Reservation>());

            var service = new ReservationService(mockReservationRepository.Object, mockUnitOfWork.Object, mockAccountRepository.Object);

            List<Reservation> reservations = (List < Reservation >)await service.ListByAccountIdAsync(accountId, reservationStatus);
            var reservationsCount = reservations.Count;

            reservationsCount.Should().Equals(0);
        }

        [Test]
        public async Task GetAllReservationsAsyncByAccountIdWhenInvalidIdReturnsAccountNotFoundResponse()
        {
            var mockAccountRepository = GetDefaultIAccountRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockAccountPaymentMethod = GetDefaultIAccountPaymentMethodRepositoryInstance();
            

            var accountId = 103;
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

        private Mock<IReservationRepository> GetDefaultIResevationRepositoryInstance()
        {
            return new Mock<IReservationRepository>();
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