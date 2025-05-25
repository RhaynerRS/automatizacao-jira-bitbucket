using MongoDB.Driver;
using Projeto.JiraAutomationService.Dominio.Jira.Entidades;
using Projeto.JiraAutomationService.Dominio.Jira.Repositorios;

namespace Projeto.JiraAutomationService.Infra.Jira.Repositorios;

public class JiraRepositorio: IJiraRepositorio
{
    private readonly IMongoCollection<Repositorio> database;
    private readonly IHttpClientFactory httpClientFactory;
    public JiraRepositorio(IHttpClientFactory httpClientFactory,IMongoDatabase database)
    {
        this.database = database.GetCollection<Repositorio>("Usuario");
        this.httpClientFactory = httpClientFactory;
    }
    
    public async Task InserirAsync(Repositorio repositorio, CancellationToken cancellationToken = default)
    {
        await database.InsertOneAsync(repositorio, cancellationToken:cancellationToken);
    }

    public IPullRequestRepositorio FactoryPullRequestRepositorio(string acao)
    {
        IPullRequestRepositorio factory = acao switch
        {
            "pullrequest:created"=> new PullRequestCriarRepositorio(httpClientFactory),
            "pullrequest:fulfilled"=> new PullRequestFinalizarRepositorio(httpClientFactory),
        };
        
        return factory;
    }
}