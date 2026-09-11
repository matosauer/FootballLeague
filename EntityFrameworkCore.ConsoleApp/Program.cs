using EntityFrameworkCore.ConsoleApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddData(builder.Configuration);
builder.Services.AddScoped<TeamExplorer>();
builder.Services.AddScoped<IConsoleApp, ConsoleApp>();

using var host = builder.Build();

using var scope = host.Services.CreateScope();

//var reader = scope.ServiceProvider.GetRequiredService<TeamExplorer>();
//await reader.PrintAllTeamsAsync();

var app = scope.ServiceProvider.GetRequiredService<IConsoleApp>();

await app.RunAsync();
