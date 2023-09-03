using Slimer.Infrastructure.Modules.Api;
using Slimer.Infrastructure.Modules.Api.Interfaces;
using Slimer.Infrastructure.Modules.Sql;
using Slimer.Infrastructure.Modules.Sql.Interfaces;
using Slimer.Infrastructure.Repositories.Sql;
using Slimer.Infrastructure.Repositories.Sql.Interfaces;
using Slimer.Infrastructure.Services;
using Slimer.Infrastructure.Services.Interfaces;
using Slimer.Services;
using Slimer.Services.Interfaces;

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

builder.Services.AddSingleton<ISecretsService, SecretsService>();
builder.Services.AddHttpClient<IHttpClientProxy, HttpClientProxy>();
builder.Services.AddTransient<ISqlCommandProvider, SqlCommandProvider>();
builder.Services.AddTransient<ISqlConnectionProvider, SqlConnectionProvider>();
builder.Services.AddTransient<ISqlExecutor, SqlExecutor>();
builder.Services.AddTransient<ITriviaQuestionRepository, TriviaQuestionRepository>();
builder.Services.AddSingleton<ITriviaQuestionService, TriviaQuestionService>();
builder.Services.AddSingleton<IMarvelService, MarvelService>();
builder.Services.AddSingleton<IChatGptService, ChatGptService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("calavera");
app.UseAuthorization();

app.MapControllers();

app.Run();
