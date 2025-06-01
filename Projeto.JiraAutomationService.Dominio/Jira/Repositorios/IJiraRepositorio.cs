using System.Linq.Expressions;
using Projeto.JiraAutomationService.Dominio.Jira.Entidades;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Entidades;

namespace Projeto.JiraAutomationService.Dominio.Jira.Repositorios;

public interface IJiraRepositorio
{
    public Task InserirAsync(Board board, CancellationToken cancellationToken = default);
    public Task<Board> RecuperarAsync(Expression<Func<Board, bool>> filtro);
    public IJiraEventoRepositorio CriarRepositorioEvento(WebhookRequisicao webhook);
}