using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MovieAppTagHelper.Models;
using MovieAppTagHelper.Options;
using Newtonsoft.Json;

namespace MovieAppTagHelper.Services
{
    public class MovieApiService : IMovieApiService
    {
        private readonly IMemoryCache memoryCache;
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        private HttpClient HttpClient { get; set; }

        public MovieApiService(IHttpClientFactory httpClientFactory, IOptions<MovieApiOptions> options, IMemoryCache memoryCache)
        {
            BaseUrl = options.Value.BaseUrl;
            ApiKey = options.Value.ApiKey;

            HttpClient = httpClientFactory.CreateClient();
            this.memoryCache = memoryCache;
        }

        public async Task<MovieApiResponse> SearchByTitleAsync(string title)
        {
            MovieApiResponse? result;

            if (true)
            {
                var response = await HttpClient.GetAsync($"{BaseUrl}?s={title}&apikey={ApiKey}");
                var json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<MovieApiResponse>(json);

                if (result?.Response == "False")
                    throw new Exception(result.Error);

                var cacheTime = new MemoryCacheEntryOptions();
                cacheTime.SetAbsoluteExpiration(TimeSpan.FromDays(10));

                memoryCache.Set(title.ToLower(), result, cacheTime);
            }

            return result;
        }

        public async Task<Cinema> SearchByIdAsync(string id)
        {
            Cinema? result;

            if (true)
            {
                var response = await HttpClient.GetAsync($"{BaseUrl}?&apikey={ApiKey}&i={id}");
                var json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Cinema>(json);

                if (result?.Response == "False")
                    throw new Exception(result.Error);

                var cacheTime = new MemoryCacheEntryOptions();
                cacheTime.SetAbsoluteExpiration(TimeSpan.FromDays(10));

                memoryCache.Set(id, result, cacheTime);
            }

            return result;
        }
    }
}
