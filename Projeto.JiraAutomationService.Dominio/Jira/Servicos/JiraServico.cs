using Newtonsoft.Json.Linq;
using Projeto.JiraAutomationService.Dominio.Jira.Entidades;
using Projeto.JiraAutomationService.Dominio.Jira.Repositorios;
using Projeto.JiraAutomationService.Dominio.Jira.Servicos.Interfaces;

namespace Projeto.JiraAutomationService.Dominio.Jira.Servicos;

public class JiraServico:IJiraServico
{
    private readonly IJiraRepositorio jiraRepositorio;
    public JiraServico(IJiraRepositorio jiraRepositorio)
    {
        this.jiraRepositorio = jiraRepositorio;
    }

    public async Task PullRequestMoverCardJira(string acao, PullRequest pullRequest)
    {
        IPullRequestRepositorio repositorio = jiraRepositorio.FactoryPullRequestRepositorio(acao);
        
        await repositorio.MoveCardJira(pullRequest);
    }
    
    public async Task<PullRequest> ConverteReponseBitbucket(string acao, JObject json)
    {
        IPullRequestRepositorio repositorio = jiraRepositorio.FactoryPullRequestRepositorio(acao);
        
        return await repositorio.ConverteReponseBitbucket(json);
    }
}