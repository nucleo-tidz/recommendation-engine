using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using nucleotidz.recommendation.infrastructure.Interfaces;
using nucleotidz.recommendation.infrastructure.Respository;
using System.Reflection;

namespace nucleotidz.recommendation.infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddVectorRepoistory(this IServiceCollection services)
        {
            return services.AddTransient<IProductVectorRepository, ProductVectorRepository>();
        }
        public static IServiceCollection AddRepoistory(this IServiceCollection services)
        {
            return services.AddTransient<IProductRepository, ProductRepository>()
                .AddTransient<IOrderRepository, OrderRepository>();
        }
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();

                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(configuration["Queue:Host"], "/", h =>
                    {
                        h.Username(configuration["Queue:Username"]);
                        h.Password(configuration["Queue:Password"]);
                    });
                    configurator.ConfigureEndpoints(context);
                });
            }).AddTransient<IEventPublisher, EventPublisher>();
        }

        public static IServiceCollection AddRabbitMqConsumer(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();
                Assembly entryAssembly = Assembly.GetExecutingAssembly();
                busConfigurator.AddConsumers(entryAssembly);
                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(configuration["Queue:Host"], "/", h =>
                    {
                        h.Username(configuration["Queue:Username"]);
                        h.Password(configuration["Queue:Password"]);
                    });
                    configurator.ConfigureEndpoints(context);
                });
            });
        }
    }
}
