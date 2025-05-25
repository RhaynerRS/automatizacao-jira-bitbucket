using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Projeto.JiraAutomationService.Dominio.Jira.Repositorios;
using Projeto.JiraAutomationService.Dominio.Jira.Entidades; 

namespace Projeto.JiraAutomationService.Infra.Jira.Repositorios;

public class PullRequestCriarRepositorio: IPullRequestRepositorio
{
    private readonly HttpClient _httpClient;
    public PullRequestCriarRepositorio(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("AuthService");
    }
    
    private static string ExtrairIssueDaBranch(string nomeBranch)
    {
        var match = Regex.Match(nomeBranch, @"([A-Z]+-\d+)");
        return match.Success ? match.Value : null;
    }

    public async Task<PullRequest> ConverteReponseBitbucket(JObject json)
    {
        JObject doc = json;

        return new PullRequest(
            doc["repository"]?["full_name"]?.ToString(),
            doc["pullrequest"]?["source"]?["branch"]?["name"]?.ToString(),
            doc["pullrequest"]?["destination"]?["branch"]?["name"]?.ToString(),
            doc["pullrequest"]?["author"]?["display_name"]?.ToString(),
            doc["pullrequest"]?["title"]?.ToString(),
            doc["pullrequest"]?["links"]?["html"]?["href"]?.ToString(),
            "CREATED",
            doc["pullrequest"]?["source"]?["commit"]?["hash"]?.ToString(),
            doc["pullrequest"]?["destination"]?["commit"]?["hash"]?.ToString()
        );
    }


    public async Task MoveCardJira(PullRequest pullRequest)
    {
        var body = new
        {
            transition = new
            {
                id = "21"
            }
        };

        var json = JsonConvert.SerializeObject(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
            
        var response = await _httpClient.PostAsync($"{ExtrairIssueDaBranch(pullRequest.BranchOrigem)}/transitions", content);

        response.EnsureSuccessStatusCode();
    }
}