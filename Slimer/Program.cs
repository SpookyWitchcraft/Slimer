using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Slimer.Infrastructure.Modules.Api;
using Slimer.Infrastructure.Modules.Api.Interfaces;
using Slimer.Infrastructure.Modules.Sql;
using Slimer.Infrastructure.Modules.Sql.Interfaces;
using Slimer.Infrastructure.Repositories.Api;
using Slimer.Infrastructure.Repositories.Api.Interfaces;
using Slimer.Infrastructure.Repositories.Sql;
using Slimer.Infrastructure.Repositories.Sql.Interfaces;
using Slimer.Infrastructure.Services;
using Slimer.Infrastructure.Services.Interfaces;
using Slimer.Services;
using Slimer.Services.Interfaces;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddPolicy("calavera", policy =>
{
    policy
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("*");
}));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var secrets = new SecretsService();

builder.Services.AddSingleton<ISecretsService>(secrets);
builder.Services.AddHttpClient<IHttpClientProxy, HttpClientProxy>();
builder.Services.AddTransient<ISqlCommandProvider, SqlCommandProvider>();
builder.Services.AddTransient<ISqlConnectionProvider, SqlConnectionProvider>();
builder.Services.AddTransient<ISqlExecutor, SqlExecutor>();
builder.Services.AddTransient<ITriviaQuestionRepository, TriviaQuestionRepository>();
builder.Services.AddTransient<IGitHubRepository, GitHubRepository>();
builder.Services.AddSingleton<ITriviaQuestionService, TriviaQuestionService>();
builder.Services.AddSingleton<IGitHubService, GitHubService>();
builder.Services.AddSingleton<IMarvelService, MarvelService>();
builder.Services.AddSingleton<IChatGptService, ChatGptService>();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.Authority = secrets.GetValue("Auth0Domain");
//        options.Audience = secrets.GetValue("Auth0Audience");
//        options.TokenValidationParameters = new TokenValidationParameters { NameClaimType = ClaimTypes.NameIdentifier };
//    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("calavera");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
