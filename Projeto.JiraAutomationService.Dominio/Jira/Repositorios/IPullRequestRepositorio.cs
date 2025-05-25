using Newtonsoft.Json.Linq;
using Projeto.JiraAutomationService.Dominio.Jira.Entidades;

namespace Projeto.JiraAutomationService.Dominio.Jira.Repositorios;

public interface IPullRequestRepositorio
{
    public Task<PullRequest> ConverteReponseBitbucket(JObject json);
    public Task MoveCardJira(PullRequest pullRequest);
}