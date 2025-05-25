namespace Projeto.JiraAutomationService.DataTransfer.Jira.Request;

public class RepositorioCriarRequest
{
    public string Nome { get; set; }
    public string IdCampoReview { get; set; }
    public string IdCampoRelease { get; set; }
    public string IdCampoDone { get; set; }
}