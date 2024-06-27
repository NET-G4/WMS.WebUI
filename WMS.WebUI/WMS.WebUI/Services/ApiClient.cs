using Newtonsoft.Json;
using System.Text;
using WMS.WebUI.Exceptions;

namespace WMS.WebUI.Services;

public class ApiClient
{
    private readonly HttpClient _client;

    public ApiClient()
    {
        _client = new()
        {
            BaseAddress = new Uri("https://localhost:7108/api/")
        };
    }

    public async Task<T> GetAsync<T>(string resource)
    {
        var response = await _client.GetAsync(resource);
        
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<T>(json)
            ?? throw new ApiException($"Could not deserialize get response for resource {resource}.");

        return result;
    }

    public async Task<T> PostAsync<T,TBody>(string resource, TBody body)
    {
        var content = GetStringContent(body);
        var response = await _client.PostAsync(resource, content);
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<T>(responseJson)
            ?? throw new ApiException($"Could not deserialize post response for resource {resource}.");

        return result;
    }

    public async Task PutAsync<TBody>(string resource, TBody body)
    {
        var content = GetStringContent(body);
        var response = await _client.PutAsync(resource, content);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(string resource, int id)
    {
        var response = await _client.DeleteAsync(resource + "/" + id);
        response.EnsureSuccessStatusCode();
    }

    private static StringContent GetStringContent<T>(T value)
    {
        var bodyJson = JsonConvert.SerializeObject(value);
        var content = new StringContent(bodyJson, Encoding.UTF8, "application/json");

        return content;
    }
}
