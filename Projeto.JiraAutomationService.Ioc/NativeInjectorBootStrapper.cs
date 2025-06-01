using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Projeto.JiraAutomationService.Aplicacao.Jira.Servicos;
using Projeto.JiraAutomationService.Dominio.Jira.Repositorios;
using Projeto.JiraAutomationService.Dominio.Jira.Servicos;
using Projeto.JiraAutomationService.Dominio.Jira.Servicos.Interfaces;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Entidades;
using Projeto.JiraAutomationService.Infra.Jira.Repositorios;
using Projeto.JiraAutomationService.Infra.Jira.Repositorios.Bitbucket;
using Projeto.JiraAutomationService.Ioc.Confiuracoes;

namespace Projeto.JiraAutomationService.Ioc
{
    public static class NativeInjectorBoostraper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddHttpContextAccessor();
            services.AddMongoDb(configuration);
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            services.AddServicesFromAssembly(typeof(JiraServico).Assembly);
            services.AddServicesFromAssembly(typeof(IJiraEventoRepositorio).Assembly);
            services.AddServicesFromAssembly(typeof(JiraAppServico).Assembly);
            services.AddServicesFromAssembly(typeof(JiraRepositorio).Assembly);
            services.AddScoped<IJiraEventoRepositorio, BitbucketPullRequestEventoRepositorio>();
            services.AddScoped<IJiraServico, JiraServico>();


            services.AddHttpClient("AuthService", httpClient =>
            {
                httpClient.BaseAddress = new Uri(configuration.GetSection("Jira:BaseUrl").Value);
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic "+Convert.ToBase64String(Encoding.UTF8.GetBytes($"rhayner3301@gmail.com:{configuration.GetSection("Jira:ApiKey").Value}")));
            });
        }
    }
}
