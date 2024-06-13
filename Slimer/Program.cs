using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Slimer.Common;
using Slimer.Common.Interfaces;
using Slimer.Extensions;
using Slimer.Features.ChatGpt;
using Slimer.Features.GitHub;
using Slimer.Features.Marvel;
using Slimer.Features.TriviaQuestions;
using Slimer.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddPolicy("calavera", policy =>
{
    policy
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("*");
}));

var secretOptions = new SecretClientOptions()
{
    Retry =
    {
        Delay= TimeSpan.FromSeconds(2),
        MaxDelay = TimeSpan.FromSeconds(16),
        MaxRetries = 5,
        Mode = RetryMode.Exponential
    }
};

Console.WriteLine("test");

var client = new SecretClient(new Uri(Environment.GetEnvironmentVariable("E_VAULT_URL")), new DefaultAzureCredential(), secretOptions);

Console.WriteLine("test2");

Task<Azure.Response<KeyVaultSecret>>[] secretTasks =
    [
        client.GetSecretAsync("SQLConnectionString"),
        client.GetSecretAsync("Auth0Domain"),
        client.GetSecretAsync("Auth0Audience")
    ];

Console.WriteLine("test3");

var connectionString = ((KeyVaultSecret)(await secretTasks[0])).Value;

builder.Services.AddMemoryCache();

builder.Services.AddSingleton(client);
builder.Services.AddSingleton<ISecretsService, SecretsService>();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddHttpClient<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<TriviaQuestionCache>();
builder.Services.AddScoped<IValidator<GetTriviaQuestionById.Request>, GetTriviaQuestionById.Validator>();
builder.Services.AddScoped<IValidator<SearchTriviaQuestionsById.Request>, SearchTriviaQuestionsById.Validator>();
builder.Services.AddScoped<IValidator<UpdateTriviaQuestion.Request>, UpdateTriviaQuestion.Validator>();
builder.Services.AddScoped<IValidator<GetCharacterDetails.Request>, GetCharacterDetails.Validator>();
builder.Services.AddScoped<IValidator<GetQuestionAnswered.Request>, GetQuestionAnswered.Validator>();
builder.Services.AddScoped<IValidator<CreateGitHubIssue.Request>, CreateGitHubIssue.Validator>();

builder.Services.AddEndpoints(typeof(Program).Assembly);

var domain = ((KeyVaultSecret)(await secretTasks[1])).Value;
var audience = ((KeyVaultSecret)(await secretTasks[2])).Value;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = domain;
        options.Audience = audience;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddFallbackPolicy("read-write", p => p.
            RequireAuthenticatedUser().
            RequireClaim("scope", "read-write"));

var app = builder.Build();

app.UseCors("calavera");
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<FaultMiddleware>();

app.MapEndpoints();

app.Run();