using SearchEngine.Data;

namespace SearchEngine.Services;

public class AnimalServiceHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public AnimalServiceHttpClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<IEnumerable<Animal>> GetAnimalsForSearchDb()
    {
        return await _httpClient.GetFromJsonAsync<List<Animal>>(_config["AnimalServiceUrl"] + "/api/animals") 
               ?? Enumerable.Empty<Animal>();
    }
}