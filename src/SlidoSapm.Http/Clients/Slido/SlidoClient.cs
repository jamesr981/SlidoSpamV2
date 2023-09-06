using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using SlidoSpam.Api.V1;

namespace SlidoSapm.Http.Clients.Slido;

public class SlidoClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public SlidoClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private const string AuthPayload =
        """{"initialAppViewer":"browser--other","granted_consents":["StoreEssentialCookies"]}""";

    public async Task<AuthenticationResponse> Authenticate(string eventGuid)
    {
        var httpClient = _httpClientFactory.CreateClient("SlidoHttpClient");

        var authenticationResponse =
            await httpClient.PostAsync($"{eventGuid}/auth",
                new StringContent(AuthPayload, Encoding.UTF8, "application/json"));

        if (!authenticationResponse.IsSuccessStatusCode)
            throw new Exception($"Failed to authenticate: {authenticationResponse.Content.ReadAsStringAsync().Result}");

        var auth = await authenticationResponse.Content.ReadFromJsonAsync<AuthenticationResponse>();
        if (auth is not null)
        {
            return auth;
        }

        throw new Exception("Unexpected authentication response");
    }

    public async Task<EventResponse> GetEvent(AuthenticationResponse authenticationResponse, string eventGuid,
        CancellationToken? cancellationToken = null)
    {
        var httpClient = _httpClientFactory.CreateClient("SlidoHttpClient");
        using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, eventGuid);
        httpRequestMessage.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", authenticationResponse.AccessToken);

        var eventResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        if (!eventResponseMessage.IsSuccessStatusCode) throw new Exception($"Unable to get event with Id {eventGuid}");

        var eventResponse = await eventResponseMessage.Content.ReadFromJsonAsync<EventResponse>();
        if (eventResponse is not null) return eventResponse;

        throw new Exception($"Unexpected Event Response for EventId: {eventGuid}");
    }

    public async Task CreateQuestion(AuthenticationResponse authenticationResponse, string eventGuid,
        QuestionPayload question, CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient("SlidoHttpClient" );
        using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"{eventGuid}/Questions");
        httpRequestMessage.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", authenticationResponse.AccessToken);
        
        var body = JsonSerializer.Serialize(question);
        httpRequestMessage.Content = new StringContent(body, Encoding.UTF8, "application/json");
        await httpClient.SendAsync(httpRequestMessage, cancellationToken);
    }

    public async Task UpdateUsername(AuthenticationResponse authenticationResponse, string eventGuid, UserPayload user,
        CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient("SlidoHttpClient");
        using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, $"{eventGuid}/User");
        httpRequestMessage.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", authenticationResponse.AccessToken);

        var body = JsonSerializer.Serialize(user);
        httpRequestMessage.Content = new StringContent(body, Encoding.UTF8, "application/json");
        await httpClient.SendAsync(httpRequestMessage, cancellationToken);
    }
}