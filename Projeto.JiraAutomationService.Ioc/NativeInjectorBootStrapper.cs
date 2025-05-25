using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Projeto.JiraAutomationService.Dominio.Jira.Entidades;
using Projeto.JiraAutomationService.Dominio.Jira.Repositorios;
using Projeto.JiraAutomationService.Dominio.Jira.Servicos;
using Projeto.JiraAutomationService.Dominio.Jira.Servicos.Interfaces;
using Projeto.JiraAutomationService.Infra.Jira.Repositorios;

namespace LocacaoApp.AuthService.IOC
{
    public static class NativeInjectorBoostraper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddHttpContextAccessor();

            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            services.AddServicesFromAssembly(typeof(PullRequest).Assembly);
            services.AddServicesFromAssembly(typeof(IPullRequestRepositorio).Assembly);
            services.AddServicesFromAssembly(typeof(JiraRepositorio).Assembly);
            services.AddScoped<IPullRequestRepositorio, PullRequestCriarRepositorio>();
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
