using MassTransit;
using Microsoft.Extensions.Hosting;
using nucleotidz.recommendation.infrastructure;
using nucleotidz.recommendation.queue.consumer;
using System.Reflection;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices((config, services) =>
    {
        services.AddArtificialIntelligence(config.Configuration);
        services.AddVectorRepoistory().AddRepoistory();
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            var entryAssembly = Assembly.GetExecutingAssembly();
            busConfigurator.AddConsumer<OrderCreatedConsumer>();
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(config.Configuration["Queue:Host"], "/", h =>
                {
                    h.Username(config.Configuration["Queue:Username"]);
                    h.Password(config.Configuration["Queue:Password"]);
                });
                configurator.ConcurrentMessageLimit = 1;
                
                configurator.ConfigureEndpoints(context);
            });
        });
        //  services.AddRabbitMqConsumer(config.Configuration);
    }).Build()
    .Run();