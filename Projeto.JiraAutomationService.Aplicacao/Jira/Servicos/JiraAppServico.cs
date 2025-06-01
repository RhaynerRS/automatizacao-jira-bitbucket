using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Projeto.JiraAutomationService.Aplicacao.Jira.Servicos.Interfaces;
using Projeto.JiraAutomationService.DataTransfer.Jira.Request;
using Projeto.JiraAutomationService.Dominio.Jira.Entidades;
using Projeto.JiraAutomationService.Dominio.Jira.Servicos.Interfaces;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Entidades;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Servicos.Interfaces;

namespace Projeto.JiraAutomationService.Aplicacao.Jira.Servicos
{
    public class JiraAppServico: IJiraAppServico
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWebhookGitServico webhookGitServico;
        private readonly IJiraServico jiraServico;
        
        public JiraAppServico(IHttpContextAccessor httpContextAccessor, IJiraServico jiraServico, IWebhookGitServico webhookGitServico)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.jiraServico = jiraServico;
            this.webhookGitServico = webhookGitServico;
        }
        
        public async Task AcaoPullRequestAsync(object payload)
        {
            var headers = httpContextAccessor.HttpContext.Request.Headers;
            
            JsonElement element = (JsonElement)payload;
            
            var doc = JObject.Parse(element.GetRawText());

            WebhookRequisicao webhookRequisicao = webhookGitServico.CriarEntidadeWebhook(headers, doc);

            var response = await jiraServico.ConvertePayload(webhookRequisicao, doc);
            await jiraServico.MoverCardJira(webhookRequisicao,response);
        }

        public async Task<Board> CadastreRepositorio(RepositorioCriarRequest request, CancellationToken cancellationToken = default)
        {
            return await jiraServico.InserirAsync(request.Nome, request.TagJira, request.UrlJira, request.IdCampoReview, request.IdCampoRelease, request.IdCampoAguardandoRelease, request.IdCampoDone,cancellationToken);
        }
    }
}