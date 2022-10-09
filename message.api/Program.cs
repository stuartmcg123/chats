using MediatR;
using message.handlers;
using message.profile;
using message.services;
using message.utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMessageServices();
builder.Services.AddCors(p => p.AddDefaultPolicy(dp => dp
.WithOrigins("http://localhost:4200")
.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials()
));
//builder.Services.MapHub();
builder.Services.AddSignalR();
builder.Services.AddMediatR(typeof(GetMessagesRequestHandler).Assembly);
builder.Services.AddTransient<IUserIdentityProvider, UserIdentityProvider>();
builder.Services.AddAutoMapper(typeof(MessageProfile).Assembly);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://identity.stuartmcgillivray.com";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseHttpsRedirection();

app.Use((context, next) =>
{
    var token = context.Request.Query["access_token"].ToString();

    if (context.Request.Path.StartsWithSegments("/messagehub") && !string.IsNullOrEmpty(token))
        context.Request.Headers.Authorization = $"Bearer {token}";
    return next();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<MessageHub>("/messagehub");

app.Run();

