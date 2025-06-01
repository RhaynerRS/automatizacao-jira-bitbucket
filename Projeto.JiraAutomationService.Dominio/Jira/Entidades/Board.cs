namespace Projeto.JiraAutomationService.Dominio.Jira.Entidades;

public class Board
{
    public int Id { get; protected set; }
    public string Nome { get; protected set; }
    public string TagJira {get ; protected set; }
    public string UrlJira { get; set; }
    public string IdCampoReview { get; protected set; }
    public string IdCampoRelease { get; protected set; }
    public string IdCampoAguardandoRelease { get; protected set; }
    public string IdCampoDone { get; protected set; }
    
    public Board(string nome, string tagJira, string urlJira, string idCampoReview, string idCampoRelease, string idCampoAguardandoRelease, string idCampoDone)
    {
        this.Nome = nome;
        this.IdCampoReview = idCampoReview;
        this.IdCampoRelease = idCampoRelease;
        this.IdCampoAguardandoRelease = idCampoAguardandoRelease;
        this.IdCampoDone = idCampoDone;
        this.UrlJira = urlJira;
        this.TagJira = tagJira;
    }
}