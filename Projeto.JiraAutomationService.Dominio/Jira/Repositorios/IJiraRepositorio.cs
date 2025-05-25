using Projeto.JiraAutomationService.Dominio.Jira.Entidades;

namespace Projeto.JiraAutomationService.Dominio.Jira.Repositorios;

public interface IJiraRepositorio
{
    public Task InserirAsync(Repositorio repositorio, CancellationToken cancellationToken = default);
    public IPullRequestRepositorio FactoryPullRequestRepositorio(string acao);
}