namespace Projeto.JiraAutomationService.Dominio.Jira.Entidades;

public class Repositorio
{
    public int Id { get; protected set; }
    public string Nome { get; protected set; }
    public string IdCampoReview { get; protected set; }
    public string IdCampoRelease { get; protected set; }
    public string IdCampoDone { get; protected set; }
    
    public Repositorio(string nome, string idCampoReview, string idCampoRelease, string idCampoDone)
    {
        this.Nome = nome;
        this.IdCampoReview = idCampoReview;
        this.IdCampoRelease = idCampoRelease;
        this.IdCampoDone = idCampoDone;
    }
}