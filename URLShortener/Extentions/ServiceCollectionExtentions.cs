using URLShortener.DAL.Entities;
using MediatR;
using URLShortener.DAL.Repositories.Interfaces.Base;
using URLShortener.DAL.Repositories.Realizations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FluentResults;
using URLShortener.BLL.DTO;
using URLShortener.BLL.MediatR.Url.GetAll;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using URLShortener.BLL.Services.Interfaces;
using URLShortener.BLL.Services.Realizations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace URLShortener.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddSingleton<IShortenUrlService, ShortenUrlService>();
            services.AddTransient<IRequestHandler<GetAllUrlsQuery, Result<IEnumerable<UrlDTO>>>, GetAllUrlsHandler>();

            var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.AddAutoMapper(currentAssemblies);
            services.AddMediatR(currentAssemblies);
        }

        public static void AddApplicationServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options => {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = configuration["Jwt:Issuer"],
                       ValidAudience = configuration["Jwt:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                   };

               });
            services.AddMvc();

            services.AddHttpContextAccessor();

            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.SetPreflightMaxAge(TimeSpan.FromDays(1));
                });
            });

            services.AddControllers();
        }
    }
}
