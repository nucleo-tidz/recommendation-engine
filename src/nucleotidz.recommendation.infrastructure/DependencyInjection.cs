using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.Connectors.Milvus;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Memory;
using Milvus.Client;
using nucleotidz.recommendation.infrastructure.Interfaces;
using nucleotidz.recommendation.infrastructure.Respository;
using System.Reflection;

namespace nucleotidz.recommendation.infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSemanticMemory(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddTransient(serviceProvider =>
               {
                   MemoryBuilder memoryBuilder = new MemoryBuilder();
#pragma warning disable SKEXP0020 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
                   memoryBuilder.WithAzureOpenAITextEmbeddingGeneration("vectoriser", "https://nucleo-tidz.openai.azure.com/", configuration["AzureOpenAI:AuthKey"])
                          .WithMemoryStore(new MilvusMemoryStore("standalone", metricType: SimilarityMetricType.Ip)).Build();
#pragma warning restore SKEXP0020 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
                   return memoryBuilder.Build();
               });


        }
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
