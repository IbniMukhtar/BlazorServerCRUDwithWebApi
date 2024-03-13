using Demo.ApiClient.Models;
using Demo.ApiClient.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Demo.ApiClient
{
    public class DemoApiClientService 
    {
        private readonly HttpClient _httpClient;

        public DemoApiClientService(ApiClientOptions apiClientOptions)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(apiClientOptions.ApiBaseAddress);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Product>?> GetProducts()
        {
                return await _httpClient.GetFromJsonAsync<List<Product>?>("/api/Product");
        }

        public async Task SaveProduct(Product product)
        {
            await _httpClient.PostAsJsonAsync("/api/Product", product);
        }

        public async Task UpdateProduct(Product product)
        {
            await _httpClient.PutAsJsonAsync("/api/Product", product);
        }

        public async Task DeleteProduct(int id)
        {
            await _httpClient.DeleteAsync($"/api/Product/{id}");
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}

