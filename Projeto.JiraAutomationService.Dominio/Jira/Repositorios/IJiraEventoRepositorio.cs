using Newtonsoft.Json.Linq;
using Projeto.JiraAutomationService.Dominio.Jira.Entidades;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Entidades;

namespace Projeto.JiraAutomationService.Dominio.Jira.Repositorios;

public interface IJiraEventoRepositorio
{
    public Task<EventoNormalizado> ConvertePayload(JObject json, WebhookRequisicao webhookRequisicao);
    public Task MoveCardJira(EventoNormalizado eventoNormalizado, Board board);
}