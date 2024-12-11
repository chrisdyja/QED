using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QedFrontend.Services
{
    public interface IQedApiService
    {
        Task<T> GetAsync<T>(double _numberA, double _numberB);
    }
    public class QedApiService : IQedApiService
    {
        private readonly HttpClient _httpClient;

        public QedApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(double _numberA, double _numberB)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7245/add/{_numberA.ToString()}/{_numberB.ToString()}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
