using FitControl.API.Data;
using Mapster;

using FitControl.API.Data;
using FitControl.Shared.Services;
using Mapster;
using MapsterMapper;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<IFitControlDbContext, FitControlDbContext>();
builder.Services.AddMapster();

builder.Services.AddDbContext<IFitControlDbContext, FitControlDbContext>();

var mapsterConfig = TypeAdapterConfig.GlobalSettings;
mapsterConfig.Scan(System.Reflection.Assembly.GetExecutingAssembly());
builder.Services.AddSingleton(mapsterConfig);
builder.Services.AddScoped<IMapper, Mapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();