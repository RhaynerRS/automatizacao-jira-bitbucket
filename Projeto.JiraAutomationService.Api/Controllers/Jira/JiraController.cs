using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Projeto.JiraAutomationService.Dominio.Jira.Repositorios;
using Projeto.JiraAutomationService.Dominio.Jira.Servicos.Interfaces;

namespace Projeto.JiraAutomationService.Api.Controllers.Jira
{
    [ApiController]
    [Route("api/jira")]
    public class JiraController : Controller
    {
        private readonly IJiraServico _repository;
        public JiraController(IJiraServico repository)
        {
            _repository = repository;
        }

        [HttpPost("pullrequest")]
        public async Task<ActionResult> AcaoPullRequestAsync([FromBody]object issueKey)
        {
            var eventKey = HttpContext.Request.Headers["X-Event-Key"].FirstOrDefault();

            if (string.IsNullOrEmpty(eventKey))
            {
                return BadRequest("Missing X-Event-Key header");
            }
            
            JsonElement element = (JsonElement)issueKey;
            
            var doc = JObject.Parse(element.GetRawText());

            var response = await _repository.ConverteReponseBitbucket(eventKey, doc);
            await this._repository.PullRequestMoverCardJira(eventKey,response);

            return Ok(issueKey);
        }
        
        [HttpPost("branch")]
        public async Task<ActionResult> AcaoBranchAsync([FromBody]object issueKey)
        {
            var eventKey = HttpContext.Request.Headers["X-Event-Key"].FirstOrDefault();

            if (string.IsNullOrEmpty(eventKey))
            {
                return BadRequest("Missing X-Event-Key header");
            }
            
            JsonElement element = (JsonElement)issueKey;
            
            var doc = JObject.Parse(element.GetRawText());

            var response = await _repository.ConverteReponseBitbucket(eventKey, doc);
            await this._repository.PullRequestMoverCardJira(eventKey,response);

            return Ok(issueKey);
        }


        [HttpPost]
        public async Task<ActionResult> CadastraRepositorio([FromQuery]string issueKey)
        {
            return Ok();
        }
    }
}
