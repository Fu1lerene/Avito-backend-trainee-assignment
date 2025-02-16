using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Avito.MerchStore.Domain.Models;
using Avito.MerchStore.Domain.Repositories.Models;

namespace Avito.MerchStore.Tests.E2E.HttpClients;

public class E2EClient
{
    private readonly HttpClient _httpClient;

    private string _token;
    public E2EClient()
    {
        _httpClient = new HttpClient(new HttpClientHandler{ UseProxy = false });
        _httpClient.BaseAddress = new Uri("http://localhost:8080");
    }
    
    public async Task Authenticate(string username)
    {
        var request = new AuthRequest
        {
            Username = username,
            Password = "12345"
        };
        var response = await _httpClient.PostAsJsonAsync("/api/auth", request);
        var responseString = await response.Content.ReadAsStringAsync();
        
        _token = JsonSerializer.Deserialize<AuthResponse>(responseString)!.Token;
        
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
    }

    public async Task<User?> GetUserByName(string username)
    {
        var response = await _httpClient.GetAsync($"/api/e2e/user/{username}");
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<User>(responseString);
    }
    
    public async Task<MerchItem?> GetItemByName(string itemname)
    {
        var response = await _httpClient.GetAsync($"/api/e2e/merch/{itemname}");
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<MerchItem>(responseString);
    }

    public async Task BuyItem(string item, int quantity)
    {
        var response = await _httpClient.GetAsync($"/api/buy/{item}?quantity={quantity}");
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    public async Task SendCoin(SendCoinRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/sendCoin", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    public async Task<InfoResponse> GetInfo()
    {
        var response = await _httpClient.GetAsync("/api/info"); 
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var responseString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<InfoResponse>(responseString);
    }


}