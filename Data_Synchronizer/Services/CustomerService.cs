using Data_Synchronizer.Http;
using Data_Synchronizer.Models;
using System.Text.Json;

namespace Data_Synchronizer.Services;

public sealed class CustomerService : IDisposable
{
    private readonly SynHttpClient _httpClient;
    public CustomerService()
    {
        _httpClient = new SynHttpClient(Routes.BaseUrl);    
    }

    public void Dispose()
    {
        _httpClient.Dispose();  
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync(Routes.Customers);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        var responseBody = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Customer>>(responseBody) ?? throw new ArgumentNullException(nameof(Customer));
    }
    public async Task<bool> SyncAsync()
    {
        var response = await _httpClient.PostAsync(Routes.Sync);
        return response.IsSuccessStatusCode;    
    }
}
