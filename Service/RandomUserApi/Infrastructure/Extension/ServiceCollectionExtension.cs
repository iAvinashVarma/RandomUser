using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RandomUser.Business.Concrete.LoggerService;
using RandomUser.Business.Concrete.Repository;
using RandomUser.Business.Contract.LoggerService;
using RandomUser.Business.Contract.Repository;
using RandomUser.Business.Entity;
using RandomUserApi.Infrastructure.Options;
using Swashbuckle.Swagger;
using System;

namespace RandomUserApi.Infrastructure.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureRepositoryContext(this IServiceCollection services)
        {
            services.AddDbContext<RepositoryContext>(opt => opt.UseInMemoryDatabase("RandomUser"), ServiceLifetime.Scoped, ServiceLifetime.Scoped);
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureMapper(this IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Random User API", Version = "v1" });
            });
        }
    }
}
