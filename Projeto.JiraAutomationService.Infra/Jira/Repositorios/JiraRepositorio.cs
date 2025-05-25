using Projeto.JiraAutomationService.Dominio.Jira.Repositorios;

namespace Projeto.JiraAutomationService.Infra.Jira.Repositorios;

public class JiraRepositorio: IJiraRepositorio
{
    private readonly IHttpClientFactory httpClientFactory;
    public JiraRepositorio(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
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