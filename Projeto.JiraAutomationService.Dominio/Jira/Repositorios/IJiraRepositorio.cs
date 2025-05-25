namespace Projeto.JiraAutomationService.Dominio.Jira.Repositorios;

public interface IJiraRepositorio
{
    public IPullRequestRepositorio FactoryPullRequestRepositorio(string acao);
}