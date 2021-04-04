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
    public class ActivateOfficesSteps
    {

        private readonly IOfficeService _officeService;
        private readonly Mock<IOfficeRepository> _officeRepositoryMock = new Mock<IOfficeRepository>();
        private readonly Mock<IAccountRepository> _accountRepositoryMock = new Mock<IAccountRepository>();
        private readonly Mock<IReservationRepository> _reservationRepository = new Mock<IReservationRepository>();

        private readonly IAccountService _accountService;
        private readonly Mock<IAccountPaymentMethodRepository> _accountPaymentMethodRepositoryMock = new Mock<IAccountPaymentMethodRepository>();
        private readonly Mock<IOptions<AppSettings>> _appSettingsMock = new Mock<IOptions<AppSettings>>(); // no se llega a usar

        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

        Office office = new Office();
        int officeId = 1;
        int providerId = 2;
        Account account = new Account();
        
        public ActivateOfficesSteps()
        {
            _officeService = new OfficeService(_officeRepositoryMock.Object,
                _accountRepositoryMock.Object, _reservationRepository.Object,
                _unitOfWorkMock.Object);


            _accountService = new AccountService(_accountRepositoryMock.Object,
                _accountPaymentMethodRepositoryMock.Object,
                _unitOfWorkMock.Object, _appSettingsMock.Object);



            _accountRepositoryMock.Setup(a => a.GetSingleByIdAsync(providerId)).ReturnsAsync(account);
            _officeRepositoryMock.Setup(o => o.FindById(officeId)).ReturnsAsync(office);
        }

        [Given(@"offi-provider has Premium Account")]
        public void GivenOffi_ProviderHasPremiumAccount()
        {
            account.IsPremium = true;
            Assert.AreEqual(_accountService.GetBySingleIdAsync(providerId).Result.Resource.IsPremium, true);
            
        }
        
        [Given(@"offi-provider is in the deactivated office window")]
        public void GivenOffi_ProviderIsInTheDeactivatedOfficeWindow()
        {
            office.Status = false;
            Assert.AreEqual(_officeService.GetByIdAsync(officeId).Result.Resource.Status, false);

        }
        
        [Given(@"offi-provider has not a Premium Account")]
        public void GivenOffi_ProviderHasNotAPremiumAccount()
        {
            account.IsPremium = false;
            Assert.AreEqual(_accountService.GetBySingleIdAsync(providerId).Result.Resource.IsPremium, false);

        }
        
        [When(@"offi-provider clicks in Activate product")]
        public void WhenOffi_ProviderClicksInActivateProduct()
        {
            var response = _officeService.ActiveOffice(providerId, officeId).Result;
        }
        
        [Then(@"the system change the office status to activated")]
        public void ThenTheSystemChangeTheOfficeStatusToActivated()
        {
            office.Status = false;
            account.IsPremium = true;
            Assert.IsTrue(_officeService.ActiveOffice(providerId, officeId).Result.Success);
        }
        
        [Then(@"the system shows the message This Account is not premium")]
        public void ThenTheSystemShowsTheMessageThisAccountIsNotPremium()
        {
            office.Status = false;
            account.IsPremium = false;
            Assert.AreEqual("This Account is not premium", _officeService.ActiveOffice(providerId, officeId).Result.Message);
        }
    }
}
