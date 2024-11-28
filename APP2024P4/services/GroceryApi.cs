namespace APP2024P4.services;

using System.Net.Http;
using System.Net.Http.Json;

public class GroceryApi
{
    private readonly HttpClient _httpClient;
    public GroceryApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

}