using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using Moq;
using NUnit.Framework;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Repositories;
using OffiRent.API.Domain.Services;
using OffiRent.API.Services;
using OffiRent.API.Settings;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace OffiRent.API.Test.StepDefinitions
{
    [Binding]
    public class ActivateReservationSteps
    {
        private readonly IReservationService _reservationService;
        private readonly Mock<IReservationRepository> _reservationRepositoryMock = new Mock<IReservationRepository>();
        private readonly Mock<IAccountRepository> _accountRepositoryMock = new Mock<IAccountRepository>();

        private readonly IAccountService _accountService;
        private readonly Mock<IAccountPaymentMethodRepository> _accountPaymentMethodRepositoryMock = new Mock<IAccountPaymentMethodRepository>();
        private readonly Mock<IOptions<AppSettings>> _appSettingsMock = new Mock<IOptions<AppSettings>>();

        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

        Reservation reservation = new Reservation();
        int reservationId = 1;
        int accountId = 2;
        string reservationStatus = "Active";
        Account account = new Account();

        public ActivateReservationSteps()
        {
            _reservationService = new ReservationService(_reservationRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _accountRepositoryMock.Object);

            _accountService = new AccountService(_accountRepositoryMock.Object,
                _accountPaymentMethodRepositoryMock.Object,
                _unitOfWorkMock.Object, _appSettingsMock.Object);

            _accountRepositoryMock.Setup(a => a.GetSingleByIdAsync(accountId)).ReturnsAsync(account);
            _reservationRepositoryMock.Setup(r => r.FindById(reservationId)).ReturnsAsync(reservation);

            reservation.Status = "true";
        }

        [Given(@"offi-user has a Reservation")]
        public void GivenOffi_UserHasReservation()
        {
        }

        [Given(@"offi-user is in the deactivated reservation window")]
        public void GivenOffi_UserIsInTheDeactivatedOfficeWindow()
        {
            reservation.Status = "Pending";
            Assert.AreEqual(_reservationService.GetByIdAsync(reservationId).Result.Resource.Status, "Pending");

        }

        [When(@"offi-user clicks in Activate reservation")]
        public void WhenOffi_UserClicksInActivateProduct()
        {
            var response = _reservationService.SetStatus(accountId, reservationStatus).Result;
        }

        [Then(@"the system change the reservation status to activated")]
        public void ThenTheSystemChangeTheOfficeStatusToActivated()
        {
            reservation.Status = "Active";
            Assert.IsTrue(_reservationService.SetStatus(reservationId, reservationStatus).Result.Success); //lo cambie porque daba error de instancia
        }
    }
}
