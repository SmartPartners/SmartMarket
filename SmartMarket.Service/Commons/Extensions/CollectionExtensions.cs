using Newtonsoft.Json;
using SmartMarket.Domin.Commons;
using SmartMarket.Domin.Configurations;
using SmartMarket.Service.Commons.Exceptions;
using SmartMarket.Service.Commons.Helpers;
using SmartMarket.Service.Helpers;
using System.Net.Http.Headers;

namespace SmartMarket.Service.Commons.Extensions;

public static class CollectionExtensions
{
    private static readonly HttpClient _httpClient;

    static CollectionExtensions()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7208/api/")
        };
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public static void AddAuthorizationHeader()
    {
        if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
        {
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
        }

        if (!string.IsNullOrEmpty(TokenHelper.apiToken))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenHelper.apiToken);
        }
    }

    public static HttpClient GetHttpClient()
    {
        return _httpClient;
    }
}