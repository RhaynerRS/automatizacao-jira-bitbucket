using System.Linq.Expressions;
using MongoDB.Driver;
using Projeto.JiraAutomationService.Dominio.Jira.Entidades;
using Projeto.JiraAutomationService.Dominio.Jira.Repositorios;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Entidades;
using Projeto.JiraAutomationService.Dominio.WebhookGit.Enumeradores;
using Projeto.JiraAutomationService.Infra.Jira.Repositorios.Bitbucket;

namespace Projeto.JiraAutomationService.Infra.Jira.Repositorios;

public class JiraRepositorio: IJiraRepositorio
{
    private readonly IMongoCollection<Board> database;
    private readonly IHttpClientFactory httpClientFactory;
    public JiraRepositorio(IHttpClientFactory httpClientFactory,IMongoDatabase database)
    {
        this.database = database.GetCollection<Board>("Boards");
        this.httpClientFactory = httpClientFactory;
    }
    
    public async Task InserirAsync(Board board, CancellationToken cancellationToken = default)
    {
        await database.InsertOneAsync(board, cancellationToken:cancellationToken);
    }

    public async Task<Board> RecuperarAsync(Expression<Func<Board, bool>> filtro)
    {
        return await database.Find(filtro).FirstOrDefaultAsync();
    }

    public IJiraEventoRepositorio CriarRepositorioEvento(WebhookRequisicao webhook)
    {
        return (webhook.Plataforma, webhook.Tipo) switch
        {
            (PlataformaEnum.bitbucket, TipoEventoEnum.pullrequest) => new BitbucketPullRequestEventoRepositorio(httpClientFactory),
            _ => throw new NotSupportedException("Combinação não suportada.")
        };
    }
}