using Data_Synchronizer.Http;
using Data_Synchronizer.Models;
using System.Text.Json;

namespace Data_Synchronizer.Services;

public sealed class LocationService : IDisposable
{
    private readonly SynHttpClient _httpClient;
    public LocationService()
    {
        _httpClient = new SynHttpClient(Routes.BaseUrl);
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }

    public async Task<List<Location>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync(Routes.Locations);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        var responseBody = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Location>>(responseBody) ?? throw new ArgumentNullException(nameof(Customer));
    }
}
