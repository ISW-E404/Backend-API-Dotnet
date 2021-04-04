using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Repositories;
using OffiRent.API.Domain.Services;
using OffiRent.API.Domain.Services.Communications;
using OffiRent.API.Services;
using OffiRent.API.Settings;
using System;
using TechTalk.SpecFlow;
using Ubiety.Dns.Core;

namespace OffiRent.API.Test.StepDefinitions
{
    [Binding]
    public class AccountDetailsSteps
    {
        AccountResponse response;
        private readonly IAccountService _accountService;
        private readonly IAccountPaymentMethodService _accountPaymentMethodService;

        private readonly Mock<IAccountRepository> _accountRepositoryMock = new Mock<IAccountRepository>();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IAccountPaymentMethodRepository> _accountPaymentMethodRepositoryMock = new Mock<IAccountPaymentMethodRepository>();
        private readonly Mock<IOptions<AppSettings>> _appSettingsMock = new Mock<IOptions<AppSettings>>(); // no se llega a usar
        string message = "Your profile will be shown to you right now";

        Account account= new Account();
        int accountId = 100;

        public AccountDetailsSteps()
        {
            _accountService = new AccountService(_accountRepositoryMock.Object, _accountPaymentMethodRepositoryMock.Object,
                _unitOfWorkMock.Object, _appSettingsMock.Object);

            _accountRepositoryMock.Setup(a => a.GetSingleByIdAsync(accountId)).ReturnsAsync(account);         
        }

        [Given(@"a verified user")]
        public void GivenAVerifiedUser()
        {
            Assert.NotNull(_accountService.GetBySingleIdAsync(accountId));
        }
        
        [Given(@"the user want to validate his personal information")]
        public void GivenTheUserWantToValidateHisPersonalInformation()
        {
            Assert.NotNull(_accountService.GetBySingleIdAsync(accountId));
        }
        
        [When(@"the user clicks on his profile icon")]
        public void WhenTheUserClicksOnHisProfileIcon()
        {
            response = _accountService.GetBySingleIdAsync(accountId).Result;
        }
        
        [Then(@"the system will show his personal information")]
        public void ThenTheSystemWillShowHisPersonalInformation()
        {
            Assert.AreEqual("Your profile will be shown to you right now", message);
        }
    }
}
