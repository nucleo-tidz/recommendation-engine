using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using nucleotidz.recommendation.infrastructure;
using nucleotidz.recommendation.infrastructure.Interfaces;
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((config, services) =>
    {
        services.AddArtificialIntelligence(config.Configuration);
        services.AddTransient<IVectorTest, VectorTest>();
    }).Build();


public interface IVectorTest
{
    Task Start();
}

public class VectorTest(IVectorizer vectorizer) : IVectorTest
{
    public async Task Start()
    {
        await vectorizer.GenerateEmbeddingsAsync(new List<string>() { "Hello World" }.ToArray());
    }
}