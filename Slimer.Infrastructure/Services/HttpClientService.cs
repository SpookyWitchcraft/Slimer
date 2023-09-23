﻿using Newtonsoft.Json;
using Slimer.Infrastructure.Services.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Text;

namespace Slimer.Infrastructure.Services
{
    [ExcludeFromCodeCoverage]
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);

            var results = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(results)!;
        }

        public async Task<T> SendAsync<T>(HttpRequestMessage content)
        {
            var response = await _httpClient.SendAsync(content);

            var results = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(results)!;
        }

        public StringContent CreateStringContent(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);

            return new StringContent(serialized, Encoding.UTF8, "application/json");
        }

        public HttpRequestMessage CreateBearerRequest(HttpContent content, HttpMethod method, string token, string url, IEnumerable<(string key, string value)> headers = null)
        {
            var request = new HttpRequestMessage
            {
                Method = method,
                Content = content,
                RequestUri = new Uri(url),
            };

            if(headers != null)
                foreach (var (key, value) in headers)
                    request.Headers.Add(key, value);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return request;
        }
    }
}
