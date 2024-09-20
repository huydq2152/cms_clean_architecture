using System.Reflection;
using CleanArchitecture.Infrastructure.MasstransisRabbitMQ.Configurations;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.MasstransisRabbitMQ.Extensions;

public static class MasstransisRabbitMQServiceExtensions
{
    public static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
    
    public static void AddConfigurationMasstransisRabbitMQ(this IServiceCollection services,
        IConfiguration configuration)
    {
        var massTransitSetting = new MasstransitSetting();
        configuration.GetSection(nameof(MasstransitSetting)).Bind(massTransitSetting);

        services.AddMassTransit(mt =>
        {
            mt.AddConsumers(Assembly.GetExecutingAssembly());
            mt.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(massTransitSetting.Host, massTransitSetting.VHost, h =>
                {
                    h.Username(massTransitSetting.UserName);
                    h.Password(massTransitSetting.Password);
                });
                
                configurator.ConfigureEndpoints(context);
            });
            
        });
    }
}