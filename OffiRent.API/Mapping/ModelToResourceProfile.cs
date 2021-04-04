using System;
using AutoMapper;
using OffiRent.API.Domain.Models;
using OffiRent.API.Extensions;
using OffiRent.API.Resources;

using OffiRent.API.Resources.Account;
using OffiRent.API.Resources.Office;

namespace OffiRent.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {

            CreateMap <Account, AccountResource>();
          
            CreateMap<Reservation, ReservationResource>();

            CreateMap<Country, CountryResource>();
            CreateMap<Currency, CurrencyResource>();

            CreateMap<Departament, DepartamentResource>();
            CreateMap<District, DistrictResource>();
            CreateMap<Office, OfficeServiceResource>();
            CreateMap<Service, ServiceResource>();

        }
    }
}
