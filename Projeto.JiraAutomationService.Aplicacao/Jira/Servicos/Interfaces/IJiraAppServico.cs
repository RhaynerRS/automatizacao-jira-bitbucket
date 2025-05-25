using Projeto.JiraAutomationService.DataTransfer.Jira.Request;
using Projeto.JiraAutomationService.Dominio.Jira.Entidades;

namespace Projeto.JiraAutomationService.Aplicacao.Jira.Servicos.Interfaces;

public interface IJiraAppServico
{
    public Task AcaoPullRequestAsync(object payload);

    public Task<Repositorio> CadastreRepositorio(RepositorioCriarRequest request,
        CancellationToken cancellationToken = default);
}