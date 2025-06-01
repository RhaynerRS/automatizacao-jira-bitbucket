using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Projeto.JiraAutomationService.Dominio.Jira.Repositorios;
using Projeto.JiraAutomationService.Dominio.Jira.Entidades;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Entidades;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Enumeradores;

namespace Projeto.JiraAutomationService.Infra.Jira.Repositorios.Bitbucket;

public class BitbucketPullRequestEventoRepositorio : IJiraEventoRepositorio
{
    private readonly HttpClient _httpClient;
    public BitbucketPullRequestEventoRepositorio(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("AuthService");
    }
    
    private static string ExtrairIssueDaBranch(string nomeBranch)
    {
        var match = Regex.Match(nomeBranch, @"([A-Z]+)-\d+", RegexOptions.IgnoreCase);
        return match.Success ? match.Value : null;
    }

    public async Task<EventoNormalizado> ConvertePayload(JObject json, WebhookRequisicao webhookRequisicao)
    {
        if (json == null) throw new ArgumentNullException(nameof(json));

        var tipo = webhookRequisicao.Tipo;
        var acao = webhookRequisicao.Acao;
        var plataforma = webhookRequisicao.Plataforma;
        
        string idEntidade = json["pullrequest"]?["id"]?.ToString() ?? Guid.NewGuid().ToString();
        string autor = json["pullrequest"]?["author"]?["display_name"]?.ToString() ?? "Desconhecido";
        DateTime timestamp = DateTime.UtcNow;
        string urlRecurso = json["pullrequest"]?["links"]?["html"]?["href"]?.ToString() ?? "";
        
        var dadosExtras = new Dictionary<string, string>
        {
            ["repositoryFullName"] = json["repository"]?["full_name"]?.ToString(),
            ["sourceBranch"] = json["pullrequest"]?["source"]?["branch"]?["name"]?.ToString(),
            ["destinationBranch"] = json["pullrequest"]?["destination"]?["branch"]?["name"]?.ToString(),
            ["title"] = json["pullrequest"]?["title"]?.ToString(),
            ["sourceCommitHash"] = json["pullrequest"]?["source"]?["commit"]?["hash"]?.ToString(),
            ["destinationCommitHash"] = json["pullrequest"]?["destination"]?["commit"]?["hash"]?.ToString()
        };

        string tagJira = ExtrairTagDoBoardJira(json["pullrequest"]?["source"]?["branch"]?["name"]?.ToString());

        return new EventoNormalizado(
            tipo,
            acao,
            plataforma,
            tagJira,
            idEntidade,
            autor,
            timestamp,
            urlRecurso,
            dadosExtras
        );
    }
    
    private static string ExtrairTagDoBoardJira(string nomeBranch)
    {
        if (string.IsNullOrWhiteSpace(nomeBranch))
            return null;

        // Regex para capturar algo como "ABC-123", "abc-456", etc.
        var match = Regex.Match(nomeBranch, @"([A-Z]+)-\d+", RegexOptions.IgnoreCase);

        if (!match.Success)
            return null;

        // Extrai só a parte da tag (as letras antes do hífen)
        var identificador = match.Groups[1].Value.ToUpperInvariant();
        return identificador;
    }

    
    private static bool IndoParaRelease(string branchName)
    {
        return branchName?.StartsWith("release/", StringComparison.OrdinalIgnoreCase) == true;
    }

    public async Task MoveCardJira(EventoNormalizado eventoNormalizado, Board board)
    {
        eventoNormalizado.DadosExtras.TryGetValue("destinationBranch", out var branchDestino);
        string idCampo = eventoNormalizado.Acao switch
        {
            AcaoEventoEnum.created => board.IdCampoReview,
            AcaoEventoEnum.merged => IndoParaRelease(branchDestino) ? board.IdCampoRelease : board.IdCampoDone,
            _ => throw new ArgumentOutOfRangeException("Ação não mapeada para esse evento")
        };
        
        var body = new
        {
            transition = new
            {
                id = idCampo
            }
        };
        var json = JsonConvert.SerializeObject(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        eventoNormalizado.DadosExtras.TryGetValue("sourceBranch", out var branch);
        var response = await _httpClient.PostAsync($"{ExtrairIssueDaBranch(branch)}/transitions", content);

        response.EnsureSuccessStatusCode();
    }
}