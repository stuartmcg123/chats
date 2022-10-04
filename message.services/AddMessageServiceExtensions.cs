using message.models;
using message.services;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Microsoft.Extensions.DependencyInjection;

public static class AddMessageServiceExtensions
{
    public static IServiceCollection AddMessageServices(this IServiceCollection services)
    {
        services.AddSingleton<MongoClient>((sp) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            return new MongoClient(configuration.GetConnectionString("mongoDb"));
        });

        services.AddSingleton<IMongoDatabase>((sp) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var url = MongoUrl.Create(configuration.GetConnectionString("mongoDb"));
            var client = sp.GetRequiredService<MongoClient>();
            return client.GetDatabase(url.DatabaseName);
        });
        services.AddSingleton((sp) =>
        {
            var database = sp.GetRequiredService<IMongoDatabase>();

            return database.GetCollection<Message>(nameof(Message));

        });
        services.AddScoped<IMessageService, MessageService>();

        return services;
    }
}