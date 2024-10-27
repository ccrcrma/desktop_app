using System.Text;
using System.Text.Json;

namespace Data_Synchronizer.Http;

public class SynHttpClient : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly string _url;
    public HttpClient Value => _httpClient;
    public SynHttpClient(string baseUrl)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(baseUrl); 
        _url = baseUrl; 
    }

    public async Task<HttpResponseMessage> GetAsync(string route, IDictionary<string, string>? request = null)
    {
        return await _httpClient.GetAsync(route);
    }
    public async Task<HttpResponseMessage> PostAsync(string route, IDictionary<string, string>? request = null)
    {
        return await _httpClient.PostAsync(route, new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
