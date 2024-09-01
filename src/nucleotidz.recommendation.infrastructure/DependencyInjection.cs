using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using nucleotidz.recommendation.infrastructure.Helpers;
using nucleotidz.recommendation.infrastructure.Interfaces;
using nucleotidz.recommendation.infrastructure.Respository;
using nucleotidz.recommendation.infrastructure.Vectorizer;
using System.Reflection;

namespace nucleotidz.recommendation.infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddArtificialIntelligence(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddTransient(serviceProvider =>
            {
                IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
                _ = kernelBuilder.Services.AddAzureOpenAITextEmbeddingGeneration("vectoriser", configuration["AzureOpenAI:Endpoint"], configuration["AzureOpenAI:AuthKey"]);
                #region Azure OpenAI
                /*
                kernelBuilder.Services.AddAzureOpenAIChatCompletion("gpt-4o",
                   configuration["AzureOpenAI:Endpoint"],
                   configuration["AzureOpenAI:AuthKey"],
                   "gpt-4-turbo",
                   "gpt-4-turbo");
                */
                #endregion
                Kernel kernel = kernelBuilder.Build();
                return kernel;
            }).AddTransient<ITextVectorizer, TextVectorizer>()
              .AddTransient<IVectorizerFactory, VectorizerFactory>()
              .AddTransient<IVectorDatabaseHelper, VectorDatabaseHelper>();
            return services;
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
