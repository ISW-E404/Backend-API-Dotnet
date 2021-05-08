using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OffiRent.API.Domain.Persistence.Contexts;
using OffiRent.API.Domain.Repositories;
using OffiRent.API.Domain.Services;
using OffiRent.API.Extensions;
using Microsoft.Extensions.Logging;
using OffiRent.API.Persistence.Repositories;
using OffiRent.API.Services;
using OffiRent.API.Settings;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace OffiRent.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //CORS Support
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyHeader());
            });

            services.AddControllers();

            //AppSettings Section Reference
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // Json Web Tpken Authentication Configuration
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            //Authentication Service Configuration
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });



            services.AddDbContext<AppDbContext>(options =>
            {
                //options.UseInMemoryDatabase("offirent-api-in-memory");
                options.UseMySQL(Configuration.GetConnectionString("MySQLConnection"));
            });

            services.AddScoped<IDepartamentRepository, DepartamentRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IOfficeRepository, OfficeRepository>();
            // Repositories
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddScoped<IAccountPaymentMethodRepository, AccountPaymentMethodRepository>();



            services.AddScoped<IDepartamentRepository, DepartamentRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IOfficeRepository, OfficeRepository>();
            // Repositories
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddScoped<IAccountPaymentMethodRepository, AccountPaymentMethodRepository>();


            // Repositories
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<ICountryCurrencyRepository, CountryCurrencyRepository>();



            //options.UseMySQL(Configuration.GetConnectionString("MySQLConnection"));
            //options.UseNpgsql("server=localhost;port=5432;database=suparmarket;uid=postgres;password=postgres");



            // Unit Of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountPaymentMethodService, AccountPaymentMethodService>();


            // Services
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICountryCurrencyService, CountryCurrencyService>();
            services.AddScoped<IOfficeService, OfficeService>();


            services.AddRouting(options => options.LowercaseUrls = true);

            // Repositories
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<ICountryCurrencyRepository, CountryCurrencyRepository>();


            services.AddAutoMapper(typeof(Startup));

            // Unit Of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountPaymentMethodService, AccountPaymentMethodService>();

            // Services

            services.AddScoped<IDepartamentService, DepartamentService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<IOfficeService, OfficeService>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountPaymentMethodService, AccountPaymentMethodService>();



            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IReservationService, ReservationService>();
            // Services
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICountryCurrencyService, CountryCurrencyService>();


            services.AddAutoMapper(typeof(Startup));
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddCustomSwagger();

            

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();


            app.UseRouting();

            //app.UseCors();
            app.UseCors("CorsPolicy");

            // Authentication Support
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCustomSwagger();

        }
    }
}
