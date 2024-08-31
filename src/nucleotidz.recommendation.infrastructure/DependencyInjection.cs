using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using nucleotidz.recommendation.infrastructure.Interfaces;

namespace nucleotidz.recommendation.infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddArtificialIntelligence(this IServiceCollection services, IConfiguration configuration = default)
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
            }).AddTransient<IVectorizer, Vectorizer>();
            return services;
        }
    }
}
