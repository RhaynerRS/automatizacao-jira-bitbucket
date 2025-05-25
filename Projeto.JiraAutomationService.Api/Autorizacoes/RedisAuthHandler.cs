using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Projeto.JiraAutomationService.Api.Autorizacoes
{
    public class RedisAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly HttpClient httpClient;
        public RedisAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IHttpClientFactory httpClientFactory)
            : base(options, logger, encoder, clock)
        {
            httpClient = httpClientFactory.CreateClient("AuthService");
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return AuthenticateResult.NoResult();
            }

            var token = authHeader.Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();

            try
            {
                var jwt = handler.ReadJwtToken(token);
                var userId = jwt.Subject;
                var jti = jwt.Id;

                httpClient.DefaultRequestHeaders.Add("Authorization", authHeader);

                var response = await httpClient.GetAsync("api/usuario/validar");

                if (response.IsSuccessStatusCode)
                {
                    var identity = new ClaimsIdentity(jwt.Claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
                else
                    throw new Exception("");
            }
            catch
            {
                return AuthenticateResult.Fail("Token inválido");
            }
        }
    }
}

public class RedisAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly HttpClient httpClient;

    public RedisAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IHttpClientFactory httpClientFactory)
        : base(options, logger, encoder, clock)
    {
        httpClient = httpClientFactory.CreateClient("AuthService");
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authHeader = Request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            return AuthenticateResult.NoResult();
        }

        var token = authHeader.Replace("Bearer ", "");
        var handler = new JwtSecurityTokenHandler();

        try
        {
            var jwt = handler.ReadJwtToken(token);
            var userId = jwt.Subject;
            var jti = jwt.Id;

            httpClient.DefaultRequestHeaders.Add("Authorization", authHeader);

            var response = await httpClient.GetAsync("api/usuario/validar");

            if (response.IsSuccessStatusCode)
            {
                var identity = new ClaimsIdentity(jwt.Claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            else
                throw new Exception("");
        }
        catch
        {
            return AuthenticateResult.Fail("Token inválido");
        }
    }
}