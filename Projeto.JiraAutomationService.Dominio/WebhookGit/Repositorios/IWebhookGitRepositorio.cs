using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Entidades;

namespace Projeto.JiraAutomationService.Dominio.WebhookGit.Repositorios;

public interface IWebhookGitRepositorio
{
    WebhookRequisicao CriarEntidade(IHeaderDictionary headers, JObject body);
}