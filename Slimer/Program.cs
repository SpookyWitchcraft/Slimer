using Slimer.Infrastructure.Modules.Api;
using Slimer.Infrastructure.Modules.Api.Interfaces;
using Slimer.Services;
using Slimer.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IHttpClientProxy, HttpClientProxy>();
builder.Services.AddSingleton<ITriviaService, TriviaService>();
builder.Services.AddSingleton<IMarvelService, MarvelService>();
builder.Services.AddSingleton<IChatGptService, ChatGptService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
