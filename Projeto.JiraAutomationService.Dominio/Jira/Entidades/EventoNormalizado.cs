using Projeto.JiraAutomationService.Dominio.WebhookGit.Enumeradores;

namespace Projeto.JiraAutomationService.Dominio.Jira.Entidades;

public class EventoNormalizado
{
    public virtual TipoEventoEnum Tipo { get; protected set; }
    public virtual AcaoEventoEnum Acao { get; protected set; }
    public virtual PlataformaEnum Plataforma { get; protected set; }
    public virtual string TagJira { get; protected set; }

    public virtual string IdEntidade { get; protected set; }
    public virtual string Autor { get; protected set; }
    public virtual DateTime Timestamp { get; protected set; }
    public virtual string UrlRecurso { get; protected  set; }
    public virtual IDictionary<string, string> DadosExtras { get; protected set; }

    public EventoNormalizado(
        TipoEventoEnum tipo,
        AcaoEventoEnum acao,
        PlataformaEnum plataforma,
        string tagJira,
        string idEntidade,
        string autor,
        DateTime timestamp,
        string urlRecurso,
        IDictionary<string, string>? dadosExtras = null)
    {
        SetTipo(tipo);
        SetAcao(acao);
        SetPlataforma(plataforma);
        SetIdEntidade(idEntidade);
        SetAutor(autor);
        this.TagJira = tagJira;
        SetTimestamp(timestamp);
        SetUrlRecurso(urlRecurso);
        SetDadosExtras(dadosExtras ?? new Dictionary<string, string>());
    }

    protected internal void SetTipo(TipoEventoEnum tipo)
    {
        if (!Enum.IsDefined(typeof(TipoEventoEnum), tipo))
            throw new ArgumentException("Tipo inválido.", nameof(tipo));
        Tipo = tipo;
    }

    protected internal void SetAcao(AcaoEventoEnum acao)
    {
        if (!Enum.IsDefined(typeof(AcaoEventoEnum), acao))
            throw new ArgumentException("Ação inválida.", nameof(acao));
        Acao = acao;
    }

    protected internal void SetPlataforma(PlataformaEnum plataforma)
    {
        if (!Enum.IsDefined(typeof(PlataformaEnum), plataforma))
            throw new ArgumentException("Plataforma inválida.", nameof(plataforma));
        Plataforma = plataforma;
    }

    protected internal void SetIdEntidade(string idEntidade)
    {
        if (string.IsNullOrWhiteSpace(idEntidade))
            throw new ArgumentNullException(nameof(idEntidade), "IdEntidade não pode ser nulo ou vazio.");
        IdEntidade = idEntidade;
    }

    protected internal void SetAutor(string autor)
    {
        if (string.IsNullOrWhiteSpace(autor))
            throw new ArgumentNullException(nameof(autor), "Autor não pode ser nulo ou vazio.");
        Autor = autor;
    }

    protected internal void SetTimestamp(DateTime timestamp)
    {
        if (timestamp == default)
            throw new ArgumentException("Timestamp inválido.", nameof(timestamp));
        Timestamp = timestamp;
    }

    protected internal void SetUrlRecurso(string urlRecurso)
    {
        if (string.IsNullOrWhiteSpace(urlRecurso))
            throw new ArgumentNullException(nameof(urlRecurso), "UrlRecurso não pode ser nulo ou vazio.");
        UrlRecurso = urlRecurso;
    }

    protected internal void SetDadosExtras(IDictionary<string, string> dadosExtras)
    {
        DadosExtras = dadosExtras ?? throw new ArgumentNullException(nameof(dadosExtras));
    }
}
