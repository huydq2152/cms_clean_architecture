using System.Reflection;
using CleanArchitecture.Infrastructure.MasstransisRabbitMQ.DependencyInjection.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.MasstransisRabbitMQ.DependencyInjection.Extensions;

public static class MasstransisRabbitMQServiceExtensions
{
    public static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
    
    public static void AddConfigurationMasstransisRabbitMQ(this IServiceCollection services,
        IConfiguration configuration)
    {
        var massTransitConfiguration = new MasstransitConfiguration();
        configuration.GetSection(nameof(MasstransitConfiguration)).Bind(massTransitConfiguration);

        services.AddMassTransit(mt =>
        {
            mt.AddConsumers(Assembly.GetExecutingAssembly());
            mt.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(massTransitConfiguration.Host, massTransitConfiguration.VHost, h =>
                {
                    h.Username(massTransitConfiguration.UserName);
                    h.Password(massTransitConfiguration.Password);
                });
                
                configurator.ConfigureEndpoints(context);
            });
            
        });
    }
}