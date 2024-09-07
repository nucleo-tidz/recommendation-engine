using Microsoft.SemanticKernel;
using nucleotidz.recommendation.engine.schema.manager;
using nucleotidz.recommendation.infrastructure;
using nucleotidz.recommendation.infrastructure.Helpers;
using nucleotidz.recommendation.infrastructure.Interfaces;
using nucleotidz.recommendation.infrastructure.Vectorizer;
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.
   AddTransient(serviceProvider =>
    {
        IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
        _ = kernelBuilder.Services.AddAzureOpenAITextEmbeddingGeneration("vectoriser", builder.Configuration["AzureOpenAI:Endpoint"], builder.Configuration["AzureOpenAI:AuthKey"]);

        Kernel kernel = kernelBuilder.Build();
        return kernel;
    }).
    AddTransient<ITextVectorizer, TextVectorizer>()
              .AddTransient<IVectorizerFactory, VectorizerFactory>()
              .AddTransient<IVectorDatabaseHelper, VectorDatabaseHelper>();
builder.Services.AddTransient<ISchemaManager, SchemaManager>();
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
