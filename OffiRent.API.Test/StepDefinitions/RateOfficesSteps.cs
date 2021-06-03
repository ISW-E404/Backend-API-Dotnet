using Microsoft.Extensions.Options;
using Moq;
using NuGet.Frameworks;
using NUnit.Framework;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Repositories;
using OffiRent.API.Domain.Services;
using OffiRent.API.Domain.Services.Communications;
using OffiRent.API.Services;
using OffiRent.API.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Ubiety.Dns.Core;

namespace OffiRent.API.Test.StepDefinitions
{
    [Binding]
    public class RateOfficeSteps
    {
        private readonly IOfficeService _officeService;
        private readonly IAccountService _accountService;
        private readonly IReservationService _reservationService;

        private readonly Mock<IOfficeRepository> _officeRepositoryMock = new Mock<IOfficeRepository>();
        private readonly Mock<IAccountRepository> _accountRepositoryMock = new Mock<IAccountRepository>();
        private readonly Mock<IReservationRepository> _reservationRepositoryMock = new Mock<IReservationRepository>();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IAccountPaymentMethodRepository> _accountPaymentMethodRepositoryMock = new Mock<IAccountPaymentMethodRepository>();
        private readonly Mock<IOptions<AppSettings>> _appSettingsMock = new Mock<IOptions<AppSettings>>();

        AccountResponse accountResponse;
        ReservationResponse reservationResponse;
        OfficeResponse officeResponse;

        Office office = new Office();
        Account account = new Account();
        Reservation reservationNull = null;
        Reservation reservation = new Reservation();
        int officeId = 100;
        int accountId = 101;
        int reservationId = 102;
        int reservationIdNull = 1;
        float score;
        string status = "active";

        Task<IEnumerable<Reservation>> reservations;
        Task<IEnumerable<Reservation>> reservations2;

        public RateOfficeSteps()
        {
            _officeService = new OfficeService(_officeRepositoryMock.Object, _accountRepositoryMock.Object, _reservationRepositoryMock.Object,_unitOfWorkMock.Object);
            _accountService = new AccountService(_accountRepositoryMock.Object,_accountPaymentMethodRepositoryMock.Object,
                _unitOfWorkMock.Object, _appSettingsMock.Object);
            _reservationService = new ReservationService(_reservationRepositoryMock.Object,_unitOfWorkMock.Object, _accountRepositoryMock.Object);

            _reservationRepositoryMock.Setup(a => a.ListAsync()).ReturnsAsync(new List<Reservation>());
            _reservationRepositoryMock.Setup(a => a.ListAccountReservationsAsync(accountId, status)).ReturnsAsync(new List<Reservation>());

            _reservationRepositoryMock.Setup(a => a.FindById(reservationIdNull)).ReturnsAsync(reservationNull);

            _reservationRepositoryMock.Setup(a => a.FindById(reservationId)).ReturnsAsync(reservation);
            _accountRepositoryMock.Setup(a => a.GetSingleByIdAsync(accountId)).ReturnsAsync(account);
            _officeRepositoryMock.Setup(o => o.FindById(officeId)).ReturnsAsync(office);
        }

        [Given(@"the user wants to rate an office")]
        public void GivenTheUserWantsToRateAnOffice()
        {
             Assert.NotNull(_accountService.GetBySingleIdAsync(accountId));
        }
        
        [Given(@"the user wants to change the rating of an office")]
        public void GivenTheUserWantsToChangeTheRatingOfAnOffice()
        {
            Assert.NotNull(_accountService.GetBySingleIdAsync(accountId));
        }
        
        [Given(@"the user wants to change the rating of an office for the second time")]
        public void GivenTheUserWantsToChangeTheRatingOfAnOfficeForTheSecondTime()
        {
            Assert.NotNull(_accountService.GetBySingleIdAsync(accountId));
        }
        
        [When(@"the user clicks on My Reservations page")]
        public void WhenTheUserClicksOnPage()
        {
            reservations = _reservationService.ListAsync();
        }
        
        [When(@"it goes to the past reservations section")]
        public void WhenItGoesToThePastReservationsSection()
        {
            reservations2 = _reservationService.ListByAccountIdAsync(accountId);
        }
        
        [When(@"select the reservation in which the user want to rate the office")]
        public void WhenSelectTheReservationInWhichTheUserWantToRateTheOffice()
        {
            Assert.NotNull(_officeService.GetByIdAsync(officeId).Result);
        }
        
        [When(@"if it's the first time that the user enters to this reservation")]
        public void WhenIfItSTheFirstTimeThatTheUserEntersToThisReservation()
        {
            reservationResponse = new ReservationResponse(reservation);   
            Assert.IsInstanceOf(typeof(Reservation), reservationResponse.Resource);
        }
        
        [When(@"the user clicks on the My Reservations page")]
        public void WhenTheUserClicksOnThePage()
        {
            reservations = _reservationService.ListAsync();
        }
        
        [When(@"it goes to the past reservation section")]
        public void WhenItGoesToThePastReservationSection()
        {
            reservations2 = _reservationService.ListByAccountIdAsync(accountId);
        }
        
        [When(@"select the reservation in which the user want to change the rating of the office")]
        public void WhenSelectTheReservationInWhichTheUserWantToChangeTheRatingOfTheOffice()
        {
            Assert.NotNull(_reservationService.GetByIdAsync(reservationId).Result);
        }
        
        [When(@"it goes to the score section to change the rating")]
        public void WhenItGoesToTheScoreSectionToChangeTheRating()
        {
            reservationResponse = _reservationService.UpdateAsync(reservationId, reservation).Result;
        }
        
        [Then(@"the system will show a screen where the user can rate the office in scales between (.*) to (.*)")]
        public void ThenTheSystemWillShowAScreenWhereTheUserCanRateTheOfficeInScalesBetweenTo(int p0, int p1)
        {
            //Assert.AreEqual(_officeService.GetByIdAsync(officeId).Result.Resource.Score, score);
        }
        
        [Then(@"the system will show a screen where the details of your past reservation appear")]
        public void ThenTheSystemWillShowAScreenWhereTheDetailsOfYourPastReservationAppear()
        {
            reservationResponse = _reservationService.GetByIdAsync(reservationId).Result;
        }
        
        [Then(@"in the score section the user can change the rating of the office")]
        public void ThenInTheScoreSectionTheUserCanChangeTheRatingOfTheOffice()
        {
            Assert.AreEqual(_officeService.GetByIdAsync(officeId).Result.Resource.Score,score);
        }
        
        [Then(@"the system will show an error message")]
        public void ThenTheSystemWillShowAnErrorMessage()
        {
            reservationResponse = _reservationService.UpdateAsync(reservationIdNull, reservationNull).Result;
            Assert.AreEqual("Reservation not found", reservationResponse.Message);

        }
    }
}
