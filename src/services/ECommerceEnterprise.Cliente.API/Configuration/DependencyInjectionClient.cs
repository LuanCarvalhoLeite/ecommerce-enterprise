using ECommerceEnterprise.Cliente.API.Application.Commands;
using ECommerceEnterprise.Cliente.API.Application.Events;
using ECommerceEnterprise.Cliente.API.Data;
using ECommerceEnterprise.Cliente.API.Data.Repository;
using ECommerceEnterprise.Cliente.API.Models;
using ECommerceEnterprise.Cliente.API.Services;
using ECommerceEnterprise.Core.Commands;
using ECommerceEnterprise.Core.Mediator;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceEnterprise.Cliente.API.Configuration;

public static class DependencyInjectionClient
{
    public static void AddRegisterService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<ClientesContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddControllers();

        services.AddCors(options =>
        {
            options.AddPolicy("Total",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
        });

        services.AddMessageBusConfiguration(configuration);

        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<RegistrarClienteCommand, ValidationResult>, ClienteCommandHandler>();

        services.AddScoped<INotificationHandler<ClienteRegistradoEvent>, ClienteEventHandler>();

        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<ClientesContext>();
    }
}
