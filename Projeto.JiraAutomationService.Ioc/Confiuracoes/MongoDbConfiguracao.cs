using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Projeto.JiraAutomationService.Ioc.Confiuracoes
{
    internal static class MongoDbConfiguracao
    {
        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoClient>(sp =>
                new MongoClient(configuration.GetConnectionString("MongoDb")));

            services.AddScoped(sp =>
                sp.GetRequiredService<IMongoClient>().GetDatabase(configuration.GetSection("MongoDb.Database").Value));
        }
    }
}
