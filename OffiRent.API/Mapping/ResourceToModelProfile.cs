using AutoMapper;
using OffiRent.API.Domain.Models;
using OffiRent.API.Resources;

using OffiRent.API.Resources.Account;
using OffiRent.API.Resources.Office;

namespace OffiRent.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveAccountResource, Account>();
            CreateMap<SaveCountryResource, Country>();
            CreateMap<SaveCurrencyResource, Currency>();
            CreateMap<SaveDepartamentResource, Departament>();
            CreateMap<SaveDistrictResource, District>();
            CreateMap<SaveOfficeServiceResource, Office>();
            CreateMap<SaveReservationResource, Reservation>();
            CreateMap<SaveServiceResource, Service>();
        }
    }
}
