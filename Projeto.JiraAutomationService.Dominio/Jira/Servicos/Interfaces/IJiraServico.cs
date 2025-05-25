using Newtonsoft.Json.Linq;
using Projeto.JiraAutomationService.Dominio.Jira.Entidades;

namespace Projeto.JiraAutomationService.Dominio.Jira.Servicos.Interfaces;

public interface IJiraServico
{
    public Task PullRequestMoverCardJira(string acao, PullRequest pullRequest);
    public Task<PullRequest> ConverteReponseBitbucket(string acao, JObject json);
}