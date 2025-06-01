using Newtonsoft.Json.Linq;
using Projeto.JiraAutomationService.Dominio.Jira.Entidades;
using Projeto.JiraAutomationService.Dominio.Jira.Repositorios;
using Projeto.JiraAutomationService.Dominio.Jira.Servicos.Interfaces;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Entidades;

namespace Projeto.JiraAutomationService.Dominio.Jira.Servicos;

public class JiraServico : IJiraServico 
{
    private readonly IJiraRepositorio jiraRepositorio;
    public JiraServico(IJiraRepositorio jiraRepositorio)
    {
        this.jiraRepositorio = jiraRepositorio;
    }

    public async Task<Board> InserirAsync(string nome, string tagJira, string urlJira, string idCampoReview, string idCampoRelease, string idCampoAguardandoRelease, string idCampoDone, CancellationToken cancellationToken = default)
    {
        Board board = new(nome,tagJira, urlJira,idCampoReview,idCampoRelease, idCampoAguardandoRelease,idCampoDone);
        
        await jiraRepositorio.InserirAsync(board, cancellationToken);
        
        return board;
    }

    public async Task MoverCardJira(WebhookRequisicao webhook, EventoNormalizado eventoNormalizado)
    {
        IJiraEventoRepositorio repositorio = jiraRepositorio.CriarRepositorioEvento(webhook);

        Board board = await jiraRepositorio.RecuperarAsync(x => x.TagJira == eventoNormalizado.TagJira );
        
        await repositorio.MoveCardJira(eventoNormalizado, board);
    }
    
    public async Task<EventoNormalizado> ConvertePayload(WebhookRequisicao webhook, JObject json)
    {
        IJiraEventoRepositorio repositorio = jiraRepositorio.CriarRepositorioEvento(webhook);
        
        return await repositorio.ConvertePayload(json,webhook);
    }
}