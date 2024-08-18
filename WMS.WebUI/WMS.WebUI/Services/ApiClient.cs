using Newtonsoft.Json;
using System.Net.Http.Headers;
using WMS.WebUI.Exceptions;

namespace WMS.WebUI.Services;

public class ApiClient
{
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApiClient(
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor)
    {
        _client = new HttpClient();

        _client.BaseAddress = new Uri("https://w2sc5qx5-7097.inc1.devtunnels.ms/api/");
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<T> GetAsync<T>(string url) where T : class
    {
        AddJwt();

        var response = await _client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<T>(json);

        if (result is null)
        {
            throw new JsonSerializationException($"Unable to deserialize response from {url}.");
        }

        return result;
    }

    public async Task<Stream> GetAsStreamAsync(string url)
    {
        AddJwt();

        var response = await _client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync();

        return stream;
    }

    public async Task<TResult> PostAsync<TResult, TBody>(string url, TBody body)
        where TBody : class
    {
        AddJwt();

        var response = await _client.PostAsJsonAsync(url, body);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<TResult>(json);

        if (result is null)
        {
            throw new JsonSerializationException($"Unable to deserialize response from {url}.");
        }

        return result;
    }

    public async Task PutAsync<TBody>(string url, TBody body)
    {
        AddJwt();

        var response = await _client.PutAsJsonAsync(url, body);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(string url, int id)
    {
        AddJwt();

        var response = await _client.DeleteAsync($"{url}/{id}");
        response.EnsureSuccessStatusCode();
    }

    private void AddJwt()
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["JWT"];

        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedException();
        }

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }
}