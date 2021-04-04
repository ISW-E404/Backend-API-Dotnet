using FluentAssertions;
using Moq;
using NUnit.Framework;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Repositories;
using OffiRent.API.Domain.Services;
using OffiRent.API.Domain.Services.Communications;
using OffiRent.API.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace OffiRent.API.Test.StepDefinitions
{
    [Binding]
    public class OfficesLimitSteps
    {
        Office newOffice;
        OfficeResponse response;
        Account nonPremiumAccount;
        Account premiumAccount;

        System.Collections.Generic.List<Office> premiumOffices = new System.Collections.Generic.List<Office>();
        private readonly IOfficeService _officeService;
        private readonly Mock<IOfficeRepository> _officeRepositoryMock = new Mock<IOfficeRepository>();
        private readonly Mock<IAccountRepository> _accountRepositoryMock = new Mock<IAccountRepository>();
        private readonly Mock<IReservationRepository> _reservationRepositoryMock = new Mock<IReservationRepository>();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

        Task addOffice(Office office) { this.premiumOffices.Add(office); return new Task(action: new Action(() => new bool())); ; }

        public OfficesLimitSteps()
        {
            _officeService = new OfficeService(
                _officeRepositoryMock.Object,
                _accountRepositoryMock.Object,
                _reservationRepositoryMock.Object,
                _unitOfWorkMock.Object);

            nonPremiumAccount = new Account
            {
                Id = 100,
                Email = "test@gmail.com",
                Password = "1234",
                Identification = "72901831",
                FirstName = "first",
                LastName = "last",
                PhoneNumber = "920837182",
                IsPremium = false,
            };

            premiumAccount = new Account
            {
                Id = 101,
                Email = "test_chroma@gmail.com",
                Password = "1234",
                Identification = "72901831",
                FirstName = "first_chroma",
                LastName = "last_chroma",
                PhoneNumber = "920837182",
                IsPremium = true,
            };

            _officeRepositoryMock
               .Setup(or => or.ListAccountOfficesAsync(premiumAccount.Id)).ReturnsAsync(premiumOffices);
            //?

            _officeRepositoryMock
                .Setup(or => or.ListAccountOfficesAsync(nonPremiumAccount.Id)).ReturnsAsync(
                new System.Collections.Generic.List<Office> {
                    new Office
                    {
                        Id = 103,
                        Address = "calle Caceres",
                        Floor = 2, Capacity = 3,
                        AllowResource = true,
                        Score = 55,
                        Description = "Oficina espaciosa con proyector",
                        Price = 150,
                        Status = true,
                        AccountId = 100,
                        DistrictId = 81
                    }
                });



            Task task = new Task(action: new Action(() => new bool()));


            _officeRepositoryMock
                .Setup(or => or.AddAsync(newOffice)).Returns(addOffice(newOffice));

            _accountRepositoryMock
                .Setup(ar => ar.GetSingleByIdAsync(premiumAccount.Id)).ReturnsAsync(premiumAccount);

            _accountRepositoryMock
                .Setup(ar => ar.GetSingleByIdAsync(nonPremiumAccount.Id)).ReturnsAsync(nonPremiumAccount);


        }

        [Given(@"the user has specified data for a new office post")]
        public void GivenTheUserHasSpecifiedDataForANewOfficePost()
        {
            newOffice = new Office
            {
                Address = "Random Address",
                Floor = 1,
                Capacity = 20,
                AllowResource = true,
                Score = 9,
                Description = "Beautiful office in the city",
                Price = 1000,
                Comment = "Comment",
                Status = true,
                AccountId = 100,
                DistrictId = 100
            };

        }

        [Given(@"the user already has an active office post")]
        public void GivenTheUserAlreadyHasAnActiveOfficePost()
        {
            Assert.IsTrue(_officeService.AccountHasMoreThanXPosts(0, nonPremiumAccount).Result);
        }

        [When(@"the user sends this resource to the system")]
        public void WhenTheUserSendsThisResourceToTheSystem()
        {
            response = _officeService.SaveAsyncPrev(newOffice).Result;
        }

        [Then(@"the system will return an error message")]
        public void ThenTheSystemWillReturnAnErrorMessage()
        {
            Assert.AreEqual(response.Message, "Your type of account cannot have more than 1 post at the same time");
        }

        [Given(@"the user has specified data for a new office")]
        public void GivenTheUserHasSpecifiedDataForANewOffice()
        {
            newOffice = new Office
            {
                Address = "Random Address",
                Floor = 1,
                Capacity = 20,
                AllowResource = true,
                Score = 9,
                Description = "Beautiful office in the city",
                Price = 1000,
                Comment = "Comment",
                Status = true,
                AccountId = 101,
                DistrictId = 100
            };
        }

        [Given(@"the user has less than (.*) active office posts")]
        public void GivenTheUserHasLessThanActiveOfficePosts(int p0)
        {
            Assert.IsFalse(_officeService.AccountHasMoreThanXPosts(p0, premiumAccount).Result);
        }

        [Then(@"the system will successfuly save the new post")]
        public void ThenTheSystemWillSuccessfulySaveTheNewPost()
        {
            Assert.IsInstanceOf(typeof(Office), response.Resource);
        }

        [Given(@"the user has more than (.*) active office posts")]
        public async void GivenTheUserHasMoreThanActiveOfficePosts(int p0)
        {
            for (int i = 0; i < p0; i++)
            {
                await _officeService.SaveAsyncPrev(newOffice);
                premiumOffices.Add(newOffice);

            }
            Assert.IsTrue(_officeService.AccountHasMoreThanXPosts(p0, premiumAccount).Result);
        }

        [Then(@"the system will return an premium error message")]
        public void ThenTheSystemWillReturnAnPremiumErrorMessage()
        {
            Assert.AreEqual("Your type of account cannot have more than 15 posts at the same time", response.Message);
        }


    }
}
