using Newtonsoft.Json.Linq;
using Projeto.JiraAutomationService.Dominio.Jira.Entidades;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Entidades;

namespace Projeto.JiraAutomationService.Dominio.Jira.Servicos.Interfaces;

public interface IJiraServico
{
    public Task<Board> InserirAsync(string nome, string tagJira, string urlJira, 
        string idCampoReview, string idCampoRelease, string idCampoAguardandoRelease, 
        string idCampoDone, CancellationToken cancellationToken = default);
    public Task MoverCardJira(WebhookRequisicao webhook, EventoNormalizado eventoNormalizado);
    public Task<EventoNormalizado> ConvertePayload(WebhookRequisicao webhook, JObject json);
}