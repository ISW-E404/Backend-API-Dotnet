using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OffiRent.API.Domain.Models;
using OffiRent.API.Extensions;

namespace OffiRent.API.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {

        public DbSet<Account> Accounts { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<AccountPaymentMethod> AccountPaymentMethods { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Departament> Departaments { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CountryCurrency> CountryCurrencies { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Service> Services { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            

            //builder.Entity<Reservation>().HasData(
            //    new Reservation
            //    {
            //        Id = 100,
            //        InitialDate = 10,
            //        EndDate = 15,
            //        InitialHour = 8,
            //        EndHour = 16,
            //        Status = true
            //    },
            //    new Reservation
            //    {
            //        Id = 101,
            //        InitialDate = 8,
            //        EndDate = 22,
            //        InitialHour = 8,
            //        EndHour = 16,
            //        Status = false
            //    }
            //    );

            //        InitialDate = 2021-03-15T12:49:23,
            //        FinishDate = 2021 - 23 - 15T12:49:23,
            //        Status = true,
            //        AccountId = 100,
            //        OfficeId = 100
            //    }
            //    );


            // Account Entity
            builder.Entity<Account>().ToTable("Accounts");
            builder.Entity<Account>().HasKey(a => a.Id);
            builder.Entity<Account>().Property(a => a.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Account>().Property(a => a.Email)
                .IsRequired().HasMaxLength(50);
            builder.Entity<Account>().Property(a => a.Password)
                .IsRequired().HasMaxLength(50);
            builder.Entity<Account>().Property(a => a.Identification)
                .HasMaxLength(50);
            builder.Entity<Account>().Property(a => a.FirstName)
                .IsRequired().HasMaxLength(50);
            builder.Entity<Account>().Property(a => a.LastName)
                .IsRequired().HasMaxLength(50);
            builder.Entity<Account>().Property(a => a.PhoneNumber)
                .HasMaxLength(50);
            builder.Entity<Account>().Property(p => p.IsPremium)
               .IsRequired();

            builder.Entity<Account>().HasData(
                new Account
                {
                    Id = 100,
                    Email = "juan@gmail.com",
                    Password = "1234",
                    Identification = "72901831",
                    FirstName = "Pepe",
                    LastName = "Cadena",
                    PhoneNumber = "920837182",
                    IsPremium = true,
                },
                new Account {Id=200, Email="alberto@gmail.com", Password="1234",Identification="87654321",FirstName="Alberto",LastName="Diaz",PhoneNumber="998761977",IsPremium=true },
                new Account { Id=300, Email="carlos@gmail.com", Password="1234",Identification="01234567",FirstName="Carlos",LastName="Gonzales",PhoneNumber="987654321",IsPremium=true}
                );



            // PaymentMethod Entity
            builder.Entity<PaymentMethod>().ToTable("PaymentMethods");
            builder.Entity<PaymentMethod>().HasKey(p => p.Id);
            builder.Entity<PaymentMethod>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<PaymentMethod>().Property(p => p.CardNumber)
                .IsRequired().HasMaxLength(30);
            builder.Entity<PaymentMethod>().Property(p => p.OwnerName)
                .IsRequired().HasMaxLength(30);
            builder.Entity<PaymentMethod>().Property(p => p.DueDate)
                .IsRequired().HasMaxLength(30);
            builder.Entity<PaymentMethod>().Property(p => p.CV)
                .IsRequired().HasMaxLength(30);

            // AccountPaymentMethod Entity
            builder.Entity<AccountPaymentMethod>().ToTable("AccountPaymentMethods");
            builder.Entity<AccountPaymentMethod>().HasKey(ap => new { ap.AccountId, ap.PaymentMethodId });

            builder.Entity<AccountPaymentMethod>()
                .HasOne(ap => ap.Account)
                .WithMany(a => a.AccountPaymentMethods)
                .HasForeignKey(ap => ap.AccountId);

            builder.Entity<AccountPaymentMethod>()
                .HasOne(ap => ap.PaymentMethod)
                .WithMany(t => t.AccountPaymentMethods)
                .HasForeignKey(ap => ap.PaymentMethodId);

            // Office Entity
            builder.Entity<Office>().ToTable("Offices");
            builder.Entity<Office>().HasKey(p => p.Id);
            builder.Entity<Office>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Office>().Property(p => p.Title)
                .IsRequired();
            builder.Entity<Office>().Property(p => p.Url)
                .IsRequired();
            builder.Entity<Office>().Property(p => p.Address)
                .IsRequired();
            builder.Entity<Office>().Property(p => p.Floor)
                .IsRequired().HasMaxLength(30);
            builder.Entity<Office>().Property(p => p.Capacity)
                .IsRequired().HasMaxLength(30);
            builder.Entity<Office>().Property(p => p.AllowResource)
                .IsRequired();
            builder.Entity<Office>().Property(p => p.Score)
                .IsRequired();
            builder.Entity<Office>().Property(p => p.Description)
                .IsRequired();
            builder.Entity<Office>().Property(p => p.Price)
                .IsRequired();
            builder.Entity<Office>().Property(p => p.Comment);
            builder.Entity<Office>().Property(p => p.Status)
                .IsRequired();
            builder.Entity<Office>().Property(o => o.DistrictId)
                .IsRequired();
            builder.Entity<Office>()
                .HasOne(p => p.Account)
                .WithMany(p => p.Offices)
                .HasForeignKey(p => p.AccountId);
            builder.Entity<Office>()
                .HasMany(o => o.Services)
                .WithOne(s => s.Office)
                .HasForeignKey(s => s.OfficeId);
            builder.Entity<Office>()
                .HasMany(o => o.Reservations)
                .WithOne(o => o.Office)
                .HasForeignKey(o => o.OfficeId);

            builder.Entity<Office>().HasData(
                new Office
                {
                    Id = 100,
                    Title ="Benevolente",
                    Url= "http://www.mateca.net/wp-content/uploads/2016/04/BBVA8.jpg",
                    Address = "calle Jerusalen",
                    Floor = 2,
                    Capacity = 4,
                    AllowResource = true,
                    Score = 85,
                    Description = "Ya sea que tu empresa esté recién comenzando o buscas un espacio para crecer, este dinámico espacio de coworking de WeWork en Magdalena del Mar ofrece algo para todos. Con nueve pisos de salas acogedoras, oficinas privadas y salas de juntas equipadas con alta tecnología, esta ubicación, a la cual también puedes traer tu perro si gustas, se diseñó cuidadosamente para alojar equipos de todos los tamaños. Disfruta la brisa marina de la playa Costa Verde mientras comes en la hermosa terraza exterior, el espacio perfecto para que te refresques sin tener que desconectarte de tu equipo. Además, es muy fácil llegar aquí: cerca de esta ubicación hay estaciones de transporte público y el edificio cuenta con estacionamiento y espacio para guardar bicicletas. ¿Quieres llevar tu día de trabajo a otro nivel? Programa una visita hoy mismo.",
                    Price = 100,
                    Status = true,
                    AccountId = 200,
                    DistrictId = 80,
                },
                new Office { Id = 101,Title="La guarida",Url= "https://stay-concierge.img-ikyu.com/concierge/wp-content/uploads/2019/11/00000620_201911_lounge_ec.jpg?auto=compress,format&lossless=0&fit=clamp&w=750", Address = "calle Jazmines", Floor = 1, Capacity = 3, AllowResource = true, Score = 99,
                    Description = "San Isidro, un centro de negocios moderno en la capital histórica de Perú, es una plataforma de lanzamiento para el éxito, y nuestro espacio de coworking en la Avenida Jorge Basadre ayuda a impulsar una mayor innovación. Con ocho pisos de espacios de trabajo creativos, ofrece a los equipos de todos los tamaños un lugar donde prosperar: conoce a un nuevo cliente en nuestras salas de estar llenas de arte, comienza un nuevo proyecto en una sala de reuniones moderna y luego ponte en contacto con tu equipo en una Oficina Privada elegante. Viajar al trabajo todos los días es sencillo con el metro en la Avenida Camino Real y la Avenida Javier Prado que se encuentran a poca distancia a pie; la cercanía a los hogares y escuelas de Lince se suma a la comodidad. Rodeado de sedes bancarias, embajadas internacionales y lugares de interés cultural, estarás en buena compañía en nuestro espacio de oficina en Av Jorge Basadre 349. Programa una visita hoy mismo.", Price = 80, Status = true, AccountId = 200, DistrictId = 81 },
                new Office { Id = 102,Title="La resistencia",Url= "https://i.pinimg.com/originals/8e/af/05/8eaf056ac29f0b7008bd1dacb48f255c.jpg", Address = "calle Girasol", Floor = 1, Capacity = 5, AllowResource = true, Score = 12, 
                    Description = "Nuestras oficinas están diseñadas intencionadamente para que hagas tu mejor trabajo, al ofrecer diferentes tipos de espacio que mejoran la productividad y la colaboración.", Price = 120, Status = true, AccountId = 100, DistrictId = 82 },
                new Office { Id = 103,Title="El espacio ideal" ,Url = "https://workplace.okamura.co.jp/works/case/151001/img/twinbird_06_executive08.jpg", Address = "calle Caceres", Floor = 2, Capacity = 3, AllowResource = true, Score = 55, 
                    Description = "Es esencial trabajar en un espacio que priorice tu salud y seguridad. Por eso, hemos rediseñado el espacio de trabajo con limpieza mejorada, diseños modificados, sistemas de HVAC actualizados y señalización útil.", Price = 150, Status = true, AccountId = 300, DistrictId = 83 }); ; ;

            //builder.Entity<Office>()
            //  .HasOne(p => p.Publication);   //en duda




            //District Entity
            builder.Entity<District>().ToTable("Districts");
            builder.Entity<District>().HasKey(p => p.Id);
            builder.Entity<District>().Property(p => p.Id)
                    .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<District>().Property(p => p.Name)
                    .IsRequired().HasMaxLength(30);

            builder.Entity<District>()
                .HasMany(p => p.Offices)
                .WithOne(p => p.District)
                .HasForeignKey(p => p.DistrictId);


            builder.Entity<District>().HasData(
                new District { Id = 80, Name = "San Isidro", DepartamentId = 90 },
                new District { Id = 81, Name = "Miraflores", DepartamentId = 90 },
                new District { Id = 82, Name = "Junin", DepartamentId = 92 },
                new District { Id = 83, Name = "Mercedes", DepartamentId = 92 });


            builder.Entity<District>()
                    .HasOne(p => p.Departament)
                    .WithMany(p => p.Districts);


            // Departament Entity
            builder.Entity<Departament>().ToTable("Departaments");
            builder.Entity<Departament>().HasKey(p => p.Id);
            builder.Entity<Departament>().Property(p => p.Id)
                    .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Departament>().Property(p => p.Name)
                    .IsRequired().HasMaxLength(30);

            builder.Entity<Departament>()
                    .HasOne(p => p.Country)
                    .WithMany(p => p.Departaments);

            builder.Entity<Departament>()
                .HasMany(p => p.Districts)
                .WithOne(p => p.Departament);

            builder.Entity<Departament>().HasData(
                new Departament { Id = 100, Name = "Lima", CountryId = 100 },
                new Departament { Id = 101, Name = "Arequipa", CountryId = 100 },

                new Departament { Id = 102, Name = "Buenos Aires", CountryId = 101 },
                new Departament { Id = 103, Name = "Córdoba", CountryId = 101 }
                );


            // OffiUser Entity



            builder.Entity<Account>()
                .HasMany(r => r.Reservations)
                .WithOne(r => r.Account)
                .HasForeignKey(r => r.Id);

            // Country Entity

            builder.Entity<Country>().ToTable("Countries");
            builder.Entity<Country>().HasKey(p => p.Id);
            builder.Entity<Country>().Property(p => p.Id)
                    .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Country>().Property(p => p.Name)
                     .IsRequired().HasMaxLength(30);

            builder.Entity<Country>().HasData(
                    new Country
                    {
                        Id = 100,
                        Name = "Peru"
                    },
                    new Country
                    {
                        Id = 101,
                        Name = "Argentina"
                    }
                    );



            // Currency Entity

            builder.Entity<Currency>().ToTable("Currencies");
            builder.Entity<Currency>().HasKey(p => p.Id);
            builder.Entity<Currency>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Currency>().Property(p => p.Name)
                 .IsRequired().HasMaxLength(30);
            builder.Entity<Currency>().Property(p => p.Symbol) //simbolo de la moneda
                 .IsRequired().HasMaxLength(1);

            builder.Entity<Currency>().HasData(
               new Currency
               {
                   Id = 100,
                   Name = "Nuevo Sol",
                   Symbol = 'S'
               },
               new Currency
               {
                   Id = 101,
                   Name = "Dolar",
                   Symbol = '$'
               }
               );


            // CountryCurrency Entity

            builder.Entity<CountryCurrency>().ToTable("CountryCurrencies");
            builder.Entity<CountryCurrency>()
                .HasKey(p => new { p.CountryId, p.CurrencyId });


            builder.Entity<CountryCurrency>()
                 .HasOne(cc => cc.Country)
                 .WithMany(c => c.CountryCurrencies)
                 .HasForeignKey(cc => cc.CountryId);

            builder.Entity<CountryCurrency>()
                 .HasOne(p => p.Currency)
                 .WithMany(d => d.CountryCurrencies)
                 .HasForeignKey(p => p.CurrencyId);

            // Service Entity
            builder.Entity<Service>().ToTable("Services");
            builder.Entity<Service>().HasKey(a => a.Id);
            builder.Entity<Service>().Property(a => a.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Service>().Property(a => a.Image);
            builder.Entity<Service>().Property(a => a.Name)
                .IsRequired().HasMaxLength(50);
            builder.Entity<Service>().Property(s => s.OfficeId)
                .IsRequired();
            builder.Entity<Service>()
            .HasOne(p => p.Office)
                 .WithMany(d => d.Services)
                 .HasForeignKey(p => p.OfficeId);


            builder.Entity<Service>().HasData(
                new Service
                {
                    Id = 100,
                    Image = "https://wallpapershome.com/images/pages/pic_v/14178.jpg",
                    Name = "Wifi",
                    OfficeId = 100
                },
                new Service
                {
                    Id = 101,
                    Image = "https://wallpapershome.com/images/pages/pic_v/14178.jpg",
                    Name = "Luz",
                    OfficeId = 100
                },
                new Service
                {
                    Id = 102,
                    Image = "https://wallpapershome.com/images/pages/pic_v/14178.jpg",
                    Name = "Agua",
                    OfficeId = 100
                },
                new Service
                {
                    Id = 103,
                    Image = "https://wallpapershome.com/images/pages/pic_v/14178.jpg",
                    Name = "Oficina",
                    OfficeId = 100
                },
                new Service
                {
                    Id = 104,
                    Image = "https://wallpapershome.com/images/pages/pic_v/14178.jpg",
                    Name = "Cable",
                    OfficeId = 100
                },
                new Service
                {
                    Id = 105,
                    Image = "https://wallpapershome.com/images/pages/pic_v/14178.jpg",
                    Name = "Limpieza",
                    OfficeId = 100
                },
                new Service
                {
                    Id = 106,
                    Image = "https://wallpapershome.com/images/pages/pic_v/14178.jpg",
                    Name = "Mantenimiento",
                    OfficeId = 100
                }
                ); ;


            //Reservation
            builder.Entity<Reservation>().ToTable("reservation");
            builder.Entity<Reservation>().HasKey(P => P.Id);
            builder.Entity<Reservation>().Property(P => P.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Reservation>().Property(p => p.InitialDate)
                .IsRequired();
            //builder.Entity<Reservation>().Property(p => p.EndDate)
            //    .IsRequired();
            //builder.Entity<Reservation>().Property(p => p.InitialHour)
            //    .IsRequired();
            //builder.Entity<Reservation>().Property(p => p.EndHour)
            //    .IsRequired();
            builder.Entity<Reservation>().Property(p => p.Status);
            //.HasDefaultValue(false);
            builder.Entity<Reservation>().Property(p => p.FinishDate)
                .IsRequired();
            builder.Entity<Reservation>().Property(p => p.AccountId)
                .IsRequired();
            builder.Entity<Reservation>().Property(p => p.OfficeId)
                .IsRequired();


            builder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 100,
                    InitialDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-03-15 12:49:23",
                    FinishDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-23-15 12:49:23",
                    Status = "Active",
                    AccountId = 200,
                    OfficeId = 102
                },
                new Reservation
                {
                    Id = 101,
                    InitialDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-03-15 12:49:23",
                    FinishDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-23-15 12:49:23",
                    Status = "Pending",
                    AccountId = 200,
                    OfficeId = 103
                },
                new Reservation
                {
                    Id = 102,
                    InitialDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-03-15 12:49:23",
                    FinishDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-23-15 12:49:23",
                    Status = "Pending",
                    AccountId = 100,
                    OfficeId = 100
                },
                new Reservation
                {
                    Id = 103,
                    InitialDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-03-15 12:49:23",
                    FinishDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-23-15 12:49:23",
                    Status = "Pending",
                    AccountId = 100,
                    OfficeId = 101
                },
                new Reservation
                {
                    Id = 104,
                    InitialDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-03-15 12:49:23",
                    FinishDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-23-15 12:49:23",
                    Status = "Active",
                    AccountId = 300,
                    OfficeId = 100
                },
                new Reservation
                {
                    Id = 105,
                    InitialDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-03-15 12:49:23",
                    FinishDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-23-15 12:49:23",
                    Status = "Pending",
                    AccountId = 300,
                    OfficeId = 101
                },
                new Reservation
                {
                    Id = 106,
                    InitialDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-03-15 12:49:23",
                    FinishDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-23-15 12:49:23",
                    Status = "Canceled",
                    AccountId = 300,
                    OfficeId = 102
                },
                new Reservation
                {
                    Id = 107,
                    InitialDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-03-15 12:49:23",
                    FinishDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-23-15 12:49:23",
                    Status = "Finished",
                    AccountId = 200,
                    OfficeId = 103
                }
                );

            builder.Entity<Reservation>()
                   .HasOne(p => p.Account)
                   .WithMany(p => p.Reservations)
                   .HasForeignKey(p => p.AccountId);

            builder.Entity<Reservation>()
                   .HasOne(p => p.Office)
                   .WithMany(p => p.Reservations)
                   .HasForeignKey(p => p.OfficeId);

            // Naming convention Policy
            builder.ApplySnakeCaseNamingConvention();
        }
    }
}
