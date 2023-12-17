using System.Net;
using MassTransit;
using Polly;
using Polly.Extensions.Http;
using SearchEngine.Consumers;
using SearchEngine.Data;
using SearchEngine.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

// Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Http service
builder.Services.AddHttpClient<AnimalServiceHttpClient>().AddPolicyHandler(GetPolicy());
// RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<AnimalCreatedConsumer>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
        {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure DB connection
app.Lifetime.ApplicationStarted.Register(async () =>
{
    try
    {
        await DbInitializer.InitDb(app);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
});

// Add Async Policy
static IAsyncPolicy<HttpResponseMessage> GetPolicy()
    => HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
        .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
