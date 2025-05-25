namespace Projeto.JiraAutomationService.Dominio.Jira.Entidades;

public class PullRequest
{
    public string Repositorio { get; protected set; }
    public string BranchOrigem { get; protected set; }
    public string BranchDestino { get; protected set; }
    public string Autor { get; protected set; }
    public string TituloPr { get; protected set; }
    public string UrlPr { get; protected set; }
    public string Estado { get; protected set; }
    public string HashCommitOrigem { get; protected set; }
    public string HashCommitDestino { get; protected set; }

    public PullRequest(string repositorio, string branchOrigem, string branchDestino,
        string autor, string titulo, string url, string estado, string hashCommitOrigem, string hashCommitDestino)
    {
        SetRepositorio(repositorio);
        SetBranchOrigem(branchOrigem);
        SetBranchDestino(branchDestino);
        SetAutor(autor);
        SetTitulo(titulo);
        SetUrl(url);
        SetEstado(estado);
        SetHashCommitOrigem(hashCommitOrigem);
        SetHashCommitDestino(hashCommitDestino);
    }

    private void SetHashCommitDestino(string hashCommitDestino)
    {
        this.HashCommitDestino = hashCommitDestino;
    }

    private void SetHashCommitOrigem(string hashCommitOrigem)
    {
        this.HashCommitOrigem = hashCommitOrigem;
    }

    private void SetEstado(string estado)
    {
        this.Estado = estado;
    }

    private void SetUrl(string url)
    {
        this.UrlPr = url;
    }

    private void SetTitulo(string titulo)
    {
        this.TituloPr = titulo;
    }

    private void SetAutor(string autor)
    {
        this.Autor = autor;
    }

    private void SetBranchDestino(string branchDestino)
    {
        this.BranchDestino = branchDestino;
    }

    private void SetBranchOrigem(string branchOrigem)
    {
        this.BranchOrigem = branchOrigem;
    }

    private void SetRepositorio(string repositorio)
    {
        this.Repositorio = repositorio;
    }
}