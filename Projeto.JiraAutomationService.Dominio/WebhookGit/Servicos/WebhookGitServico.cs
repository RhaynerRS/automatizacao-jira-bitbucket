using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Entidades;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Repositorios;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Servicos.Interfaces;

namespace Projeto.JiraAutomationService.Dominio.WebhookGit.Servicos;

public class WebhookGitServico : IWebhookGitServico
{
    private readonly IWebhookGitRepositorio _webhookGitRepositorio;
    public WebhookGitServico(IWebhookGitRepositorio webhookGitRepositorio)
    {
        _webhookGitRepositorio = webhookGitRepositorio;
    }

    public WebhookRequisicao CriarEntidadeWebhook(IHeaderDictionary headers, JObject payload)
    {
        return _webhookGitRepositorio.CriarEntidade(headers, payload);
    }
}