using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PetStore.Abstraction.DAL;
using PetStore.BLL.Toys.Queries;
using PetStore.DAL.Configuration;
using PetStore.DAL.Repositories;
using PetStore.Server.Helpers;
using System.Reflection;

using static PetStore.Server.Configuration.PolicyConstants;

namespace PetStore.Server.Configuration;
internal static class ServiceCollectionExtensions
{
    public static IServiceCollection BootstrapApplication(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.RegisterAuthentication(configuration);
        services.RegisterMediatr();
        services.AddAutoMapper(new[] { typeof(Mappers.ToyProfile), typeof(BLL.Mappers.ToyProfile) });
        services.AddControllersWithViews();
        services.AddRazorPages();
        services.RegisterSwagger();

        return services;
    }

    public static void RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerAdapter, NLogAdapter>();
        services.RegisterDB();
    }

    #region << Bootstrap Application >>

    public static void RegisterAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
            {
                c.Authority = $"https://{configuration["Auth0:Domain"]}";
                c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidAudience = configuration["Auth0:Audience"],
                    ValidIssuer = $"https://{configuration["Auth0:Domain"]}"
                };
            });

        services
            .AddAuthorization(options =>
            {
                options.AddPolicy(ADMIN, policy => policy.RequireClaim("permissions", "CreateToy"));
                options.AddPolicy(USER, policy => policy.RequireClaim("permissions", "CreateOrder"));
            });
    }

    public static IServiceCollection RegisterMediatr(this IServiceCollection services)
    {
        Assembly[] assemblies = new Assembly[] { typeof(SearchToysQuery).Assembly };
        services.AddMediatR(assemblies);
        AssemblyScanner.FindValidatorsInAssemblies(assemblies).ForEach(delegate (AssemblyScanner.AssemblyScanResult item)
        {
            services.AddScoped(item.InterfaceType, item.ValidatorType);
        });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }

    public static IServiceCollection RegisterSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Pet Store Definition",
                Version = "v1",
                Description = File.ReadAllText("swagger-info.md"),
                Contact = new OpenApiContact()
                {
                    Name = "Nino Corovic",
                    Url = new Uri("https://github.com/ncorovic1")
                }
            });

            // Add Authentication Button
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Authorization with bearer scheme.",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "PetStore.Server.xml"));
        });

        return services;
    }

    #endregion

    #region << Register Application Services >>

    public static void RegisterDB(this IServiceCollection services)
    {
        services.AddScoped<DbContext, PetStoreDbContext>();
        services.AddDbContextPool<PetStoreDbContext>(options =>
        {
            options.UseInMemoryDatabase(databaseName: "PetStoreContext");
        });

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
        services.AddScoped<IToyRepository, ToyRepository>();
        services.AddScoped<IToyCategoryRepository, ToyCategoryRepository>();
        services.AddScoped<IToyTypeRepository, ToyTypeRepository>();
    }

    #endregion
}

