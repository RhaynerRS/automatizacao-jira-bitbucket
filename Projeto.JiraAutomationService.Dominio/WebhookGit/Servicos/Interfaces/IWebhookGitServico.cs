using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Entidades;

namespace Projeto.JiraAutomationService.Dominio.WebhookGit.Servicos.Interfaces;

public interface IWebhookGitServico
{
    WebhookRequisicao CriarEntidadeWebhook(IHeaderDictionary headers, JObject payload);
}