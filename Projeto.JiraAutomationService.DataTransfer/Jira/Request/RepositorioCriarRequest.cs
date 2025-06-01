namespace Projeto.JiraAutomationService.DataTransfer.Jira.Request;

public class RepositorioCriarRequest
{
    public string Nome { get;  set; }
    public string TagJira {get ;set; }
    public string UrlJira { get; set; }
    public string IdCampoReview { get;  set; }
    public string IdCampoRelease { get;  set; }
    public string IdCampoAguardandoRelease { get;  set; }
    public string IdCampoDone { get;  set; }
}