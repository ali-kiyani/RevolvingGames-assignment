using RevolvingGamesServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<RevolvingServiceImpl>();
// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<RevolvingServiceImpl>();


app.Run();
