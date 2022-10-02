using Microsoft.Extensions.DependencyInjection;
using message.models;
using MongoDB.Driver;

namespace message.services;

public static class AddMessageServiceExtensions
{
    public static IServiceCollection Add(this IServiceCollection services)
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