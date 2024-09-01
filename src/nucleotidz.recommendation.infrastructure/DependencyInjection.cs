using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using nucleotidz.recommendation.infrastructure.Interfaces;
using nucleotidz.recommendation.infrastructure;
using MassTransit;
using System.Reflection;
using nucleotidz.recommendation.infrastructure.Respository;
using nucleotidz.recommendation.infrastructure.Helpers;

namespace nucleotidz.recommendation.infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddArtificialIntelligence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<Kernel>(serviceProvider =>
            {
                IKernelBuilder kernelBuilder = Kernel.CreateBuilder();

#pragma warning disable SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
                kernelBuilder.Services.AddAzureOpenAITextEmbeddingGeneration("vectoriser", configuration["AzureOpenAI:Endpoint"], configuration["AzureOpenAI:AuthKey"]);
#pragma warning restore SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
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
            return services.AddTransient<IProductRepository, ProductRepository>();
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
                var entryAssembly = Assembly.GetExecutingAssembly();
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
