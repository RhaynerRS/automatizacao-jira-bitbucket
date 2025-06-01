using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Entidades;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Enumeradores;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Repositorios;

namespace Projeto.JiraAutomationService.Infra.WebhookGit.Repositorios;

public class WebhookGitRepositorio : IWebhookGitRepositorio
{
    public WebhookGitRepositorio(){}

    public WebhookRequisicao CriarEntidade(IHeaderDictionary headers, JObject body)
    {
        string header = headers["X-Event-Key"].FirstOrDefault();
        if (header!=null)
        {
            string[] partes = header.Split(':');
            TipoEventoEnum tipo = MapearTipoBitbucket(partes[0]);
            AcaoEventoEnum acao = (AcaoEventoEnum) Enum.Parse(typeof(AcaoEventoEnum), partes[1]);


            return new WebhookRequisicao(tipo, acao, PlataformaEnum.bitbucket);
        } else
        {
            var eventType = body.Value<string>("eventType");
            TipoEventoEnum tipo = MapearTipoAzure(eventType, body);
            AcaoEventoEnum acao = MapearAcaoAzure(eventType, body);

            return new WebhookRequisicao(tipo,acao,PlataformaEnum.azure);
        }
        
        throw new InvalidOperationException("Plataforma desconhecida ou payload inválido.");
    }
    
    private TipoEventoEnum MapearTipoBitbucket(string tipo)
    {
        return tipo.ToLower() switch
        {
            "pullrequest" => TipoEventoEnum.pullrequest,
            "repo:refs_changed" => TipoEventoEnum.branch,
            "push" => TipoEventoEnum.branch,
            "branch" => TipoEventoEnum.branch,
            _ => throw new NotSupportedException($"Tipo de evento desconhecido: {tipo}")
        };
    }
    
    private TipoEventoEnum MapearTipoAzure(string eventType, dynamic json)
    {
        return eventType switch
        {
            "git.pullrequest.created" or
                "git.pullrequest.updated" or
                "git.pullrequest.merged" => TipoEventoEnum.pullrequest,

            "git.push" => AnalisarPushAzureTipo(json),

            _ => throw new NotSupportedException($"Tipo de evento Azure desconhecido: {eventType}")
        };
    }

    private TipoEventoEnum AnalisarPushAzureTipo(dynamic json)
    {
        var refUpdates = json?.resource?.refUpdates;
        if (refUpdates != null)
        {
            foreach (var refUpdate in refUpdates)
            {
                string name = refUpdate?.name;
                if (name != null && name.StartsWith("refs/heads/"))
                    return TipoEventoEnum.branch;
            }
        }

        throw new InvalidOperationException("Evento git.push sem branch detectada.");
    }

    private AcaoEventoEnum MapearAcaoAzure(string eventType, dynamic json)
    {
        if (eventType.StartsWith("git.pullrequest."))
            return (AcaoEventoEnum) Enum.Parse(typeof(AcaoEventoEnum), eventType.Split('.').Last());

        if (eventType == "git.push")
        {
            foreach (var refUpdate in json?.resource?.refUpdates)
            {
                string newObj = refUpdate?.newObjectId;
                string oldObj = refUpdate?.oldObjectId;

                if (oldObj == "0000000000000000000000000000000000000000")
                    return AcaoEventoEnum.created;

                if (newObj == "0000000000000000000000000000000000000000")
                    return AcaoEventoEnum.deleted;
            }
            return AcaoEventoEnum.updated;
        }

        throw new InvalidOperationException("Ação não mapeada");
    }
}