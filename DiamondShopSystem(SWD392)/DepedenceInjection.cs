using Application.Interfaces;
using Application;
using Infrastructures;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using DiamondShopSystem_SWD392_.Middlewares;
using DiamondShopSystem_SWD392_.Services;
using FluentValidation.AspNetCore;
using DiamondShopSystem_SWD392_.Validations.AccountValidations;
using FluentValidation;
using Application.ViewModels.AccountDTOs;

namespace DiamondShopSystem_SWD392_
{
        public static class DependencyInjection
        {
            public static IServiceCollection AddWebAPIService(this IServiceCollection services)
            {
                services.AddControllers();
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen();
                services.AddHealthChecks();
              //services.AddSingleton<GlobalExceptionMiddleware>();
                services.AddSingleton<PerformanceMiddleware>();
                services.AddSingleton<Stopwatch>();
                services.AddScoped<IClaimsService, ClaimsService>();
                services.AddHttpContextAccessor();
                services.AddMemoryCache();
                services.AddScoped<IUnitOfWork, UnitOfWork>();
                //Fluent Validator
                services.AddFluentValidationAutoValidation();
                services.AddFluentValidationClientsideAdapters();
                //Fluent Validator / Account
                services.AddTransient<IValidator<CreateAccountDTO>, CreateAccountViewModelValidation>();
                services.AddTransient<IValidator<RegisterAccountDTO>, RegisterAccountViewModelValidation>();
                services.AddTransient<IValidator<UpdateAccountDTO>, UpdateAccountViewModelValidation>();

            return services;
            }
        }
    }

