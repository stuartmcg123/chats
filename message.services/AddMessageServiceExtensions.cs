using message.models;
using message.services;
using MongoDB.Driver;

namespace Microsoft.Extensions.DependencyInjection;

public static class AddMessageServiceExtensions
{
    public static IServiceCollection AddMessageServices(this IServiceCollection services)
    {
        services.AddSingleton<MongoClient>((sp) =>
        {
            return new MongoClient("");
        });

        services.AddSingleton<IMongoDatabase>((sp) =>
        {
            var client = sp.GetRequiredService<MongoClient>();
            return client.GetDatabase("");
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