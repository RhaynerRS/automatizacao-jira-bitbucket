using Newtonsoft.Json.Linq;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Enumeradores;

namespace Projeto.JiraAutomationService.Dominio.WebhookGit.Entidades;

public class WebhookRequisicao
{
    public virtual TipoEventoEnum Tipo { get; protected set; }
    public virtual AcaoEventoEnum Acao { get; protected set; }
    public virtual PlataformaEnum Plataforma { get; protected set; }
    
    public WebhookRequisicao(TipoEventoEnum tipo, AcaoEventoEnum acao, PlataformaEnum plataforma)
    {
        this.Tipo = tipo;
        this.Acao = acao;
        this.Plataforma = plataforma;
    }
}