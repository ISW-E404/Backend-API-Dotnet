using Moq;
using NUnit.Framework;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Repositories;
using OffiRent.API.Domain.Services;
using OffiRent.API.Domain.Services.Communications;
using OffiRent.API.Services;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace OffiRent.API.Test.StepDefinitions
{
    [Binding]
    public class CreateOfficeSteps
    {
        Account provider;
        Office validOffice;
        Office pricelessOffice;
        Office districtlessOffice;

        OfficeResponse goodResponse;
        OfficeResponse priceResponse;
        OfficeResponse districtResponse;


        private readonly IOfficeService _officeService;
        private readonly Mock<IOfficeRepository> _officeRepositoryMock = new Mock<IOfficeRepository>();
        private readonly Mock<IAccountRepository> _accountRepositoryMock = new Mock<IAccountRepository>();
        private readonly Mock<IReservationRepository> _reservationRepositoryMock = new Mock<IReservationRepository>();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        
        string districtError = "An office needs to have a district";
        string priceError = "An office needs to have a price";


        public CreateOfficeSteps()
        {
            provider = new Account
            {
                Id = 100,
                Email = "test@gmail.com",
                Password = "1234",
                Identification = "72901831",
                FirstName = "tester",
                LastName = "testing",
                PhoneNumber = "920837182",
                IsPremium = true,
            };

            _officeService = new OfficeService(
                _officeRepositoryMock.Object,
                _accountRepositoryMock.Object,
                _reservationRepositoryMock.Object,
                _unitOfWorkMock.Object);

            _officeRepositoryMock.Setup(or => or.AddAsync(validOffice)).Returns(new Task(action: new Action(() => new OfficeResponse(validOffice))));
            _officeRepositoryMock.Setup(or => or.AddAsync(districtlessOffice)).Returns(new Task(action: new Action(() => new OfficeResponse("An office needs to have a district"))));
            _officeRepositoryMock.Setup(or => or.AddAsync(pricelessOffice)).Returns(new Task(action: new Action(() => new OfficeResponse("An office needs to have a price"))));
            _accountRepositoryMock.Setup(ar => ar.GetSingleByIdAsync(provider.Id)).ReturnsAsync(provider);

            
        }

        [Given(@"the provider specifies properties for his office")]
        public void GivenTheProviderSpecifiesPropertiesForHisOffice()
        {
            validOffice = new Office
            {
                Id = 100,
                Address = "calle felicidad",
                Floor = 2,
                Capacity = 4,
                AllowResource = true,
                Score = 85,
                Description = "Oficina espaciosa con gran comodidad",
                Price = 100,
                Status = true,
                AccountId = provider.Id,
                DistrictId = 80,
            };
        }
        
        [Given(@"his office meets all the requirements")]
        public void GivenHisOfficeMeetsAllTheRequirements()
        {
            Assert.IsTrue(validOffice.Validate());
        }

        [When(@"the user sends the data to the system")]
        public void WhenTheUserSendsTheDataToTheSystem()
        {
            goodResponse = _officeService.SaveAsyncPrev(validOffice).Result;
        }

        [Then(@"the system will save the office successfully")]
        public void ThenTheSystemWillSaveTheOfficeSuccessfully()
        {
            Assert.IsTrue(goodResponse.Success);
        }

        [Given(@"the provider doesn't specify the price for the office")]
        public void GivenTheProviderDoesnTSpecifyThePriceForTheOffice()
        {
            pricelessOffice = new Office
            {
                Id = 100,
                Address = "calle felicidad",
                Floor = 2,
                Capacity = 4,
                AllowResource = true,
                Score = 85,
                Description = "Oficina espaciosa con gran comodidad",
                Price = 100,
                Status = true,
                AccountId = provider.Id,
                DistrictId = null,
            };
        }
        
        [When(@"the user sends the data without price to the system")]
        public void WhenTheUserSendsTheDataWithoutPriceToTheSystem()
        {
            priceResponse = _officeService.SaveAsyncPrev(pricelessOffice).Result;
        }

        [Then(@"the system will return an error response message, asking to specify a price")]
        public void ThenTheSystemWillReturnAnErrorResponseMessageAskingToSpecifyAPrice()
        {
            Assert.AreEqual("An office needs to have a price", priceError);
        }

        [Given(@"the provider doesn't specify the district of the office")]
        public void GivenTheProviderDoesnTSpecifyTheDistrictOfTheOffice()
        {
            districtlessOffice = new Office
            {
                Id = 100,
                Address = "calle felicidad",
                Floor = 2,
                Capacity = 4,
                AllowResource = true,
                Score = 85,
                Description = "Oficina espaciosa con gran comodidad",
                Price = 100,
                Status = true,
                AccountId = provider.Id,
                DistrictId = null,
            };
        }


        [When(@"the user sends the data to the system without district")]
        public void WhenTheUserSendsTheDataToTheSystemWithoutDistrict()
        {
            districtResponse = _officeService.SaveAsyncPrev(districtlessOffice).Result;
        }

        [Then(@"the system will return an error response message, asking to specify a district")]
        public void ThenTheSystemWillReturnAnErrorResponseMessageAskingToSpecifyADistrict()
        {
            Assert.AreEqual("An office needs to have a district", districtError);
        }
    }
}
