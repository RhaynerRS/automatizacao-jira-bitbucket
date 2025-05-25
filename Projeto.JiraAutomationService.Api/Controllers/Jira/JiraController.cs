using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Projeto.JiraAutomationService.Aplicacao.Jira.Servicos.Interfaces;
using Projeto.JiraAutomationService.DataTransfer.Jira.Request;
using Projeto.JiraAutomationService.Dominio.Jira.Repositorios;
using Projeto.JiraAutomationService.Dominio.Jira.Servicos.Interfaces;

namespace Projeto.JiraAutomationService.Api.Controllers.Jira
{
    [ApiController]
    [Route("api/jira")]
    public class JiraController : Controller
    {
        private readonly IJiraAppServico jiraServico;
        public JiraController(IJiraAppServico jiraServico)
        {
            this.jiraServico = jiraServico;
        }

        [HttpPost("pullrequest")]
        public async Task<ActionResult> AcaoPullRequestAsync([FromBody]object issueKey)
        {
            await jiraServico.AcaoPullRequestAsync(issueKey);

            return Ok();
        }
        
        [HttpPost("branch")]
        public async Task<ActionResult> AcaoBranchAsync([FromBody]object issueKey)
        {
            return Ok(issueKey);
        }


        [HttpPost]
        public async Task<ActionResult> CadastraRepositorio([FromBody]RepositorioCriarRequest request)
        {
            return Ok(await jiraServico.CadastreRepositorio(request));
        }
    }
}
