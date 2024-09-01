using MassTransit;
using Microsoft.Extensions.Hosting;
using nucleotidz.recommendation.infrastructure;
using nucleotidz.recommendation.queue.consumer;
using System.Reflection;

IHostBuilder builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices((config, services) =>
    {
        _ = services.AddArtificialIntelligence(config.Configuration);
        _ = services.AddVectorRepoistory().AddRepoistory();
        _ = services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            Assembly entryAssembly = Assembly.GetExecutingAssembly();
            _ = busConfigurator.AddConsumer<OrderCreatedConsumer>();
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