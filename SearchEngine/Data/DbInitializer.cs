using MongoDB.Driver;
using MongoDB.Entities;
using SearchEngine.Services;

namespace SearchEngine.Data;

public class DbInitializer
{
    public static async Task InitDb(WebApplication app)
    {
        await DB.InitAsync("SearchDb", MongoClientSettings
            .FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

        await DB.Index<Animal>()
            .Key(x => x.Type, KeyType.Text)
            .Key(x => x.Breed, KeyType.Text)
            .Key(x => x.Sex, KeyType.Text)
            .CreateAsync();

        var count = await DB.CountAsync<Animal>();

        using var scope = app.Services.CreateScope();

        var httpClient = scope.ServiceProvider.GetRequiredService<AnimalServiceHttpClient>();

        var animals = await httpClient.GetAnimalsForSearchDb();
        var enumerable = animals as Animal[] ?? animals.ToArray();
        
        Console.WriteLine(enumerable.Count() + " returned from the animal service");

        if (enumerable.Any()) await DB.SaveAsync(enumerable);
    }
}