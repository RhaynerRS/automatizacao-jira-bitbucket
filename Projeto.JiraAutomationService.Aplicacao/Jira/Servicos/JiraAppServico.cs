using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Projeto.JiraAutomationService.Aplicacao.Jira.Servicos.Interfaces;
using Projeto.JiraAutomationService.DataTransfer.Jira.Request;
using Projeto.JiraAutomationService.Dominio.Jira.Entidades;
using Projeto.JiraAutomationService.Dominio.Jira.Servicos.Interfaces;

namespace Projeto.JiraAutomationService.Aplicacao.Jira.Servicos
{
    public class JiraAppServico: IJiraAppServico
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IJiraServico jiraServico;
        
        public JiraAppServico(IHttpContextAccessor httpContextAccessor, IJiraServico jiraServico)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.jiraServico = jiraServico;
        }
        
        public async Task AcaoPullRequestAsync(object payload)
        {
            var eventKey = httpContextAccessor.HttpContext.Request.Headers["X-Event-Key"].FirstOrDefault();

            if (string.IsNullOrEmpty(eventKey))
            {
                throw new Exception("Não foi possivel consultar o evento");
            }
            
            JsonElement element = (JsonElement)payload;
            
            var doc = JObject.Parse(element.GetRawText());

            var response = await jiraServico.ConverteReponseBitbucket(eventKey, doc);
            await jiraServico.PullRequestMoverCardJira(eventKey,response);
        }

        public async Task<Repositorio> CadastreRepositorio(RepositorioCriarRequest request, CancellationToken cancellationToken = default)
        {
            return await jiraServico.InserirAsync(request.Nome, request.IdCampoReview, request.IdCampoRelease, request.IdCampoDone,cancellationToken);
        }
    }
}