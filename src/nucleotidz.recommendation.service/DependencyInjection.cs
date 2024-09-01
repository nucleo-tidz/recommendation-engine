using Microsoft.Extensions.DependencyInjection;
using nucleotidz.recommendation.service.Implementation;
using nucleotidz.recommendation.service.Interfaces;

namespace nucleotidz.recommendation.service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddTransient<IProductService, ProductService>()
                .AddTransient<IOrderService, OrderService>();
        }
    }
}
