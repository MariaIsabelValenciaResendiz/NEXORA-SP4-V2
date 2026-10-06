using System.Text.Json.Serialization;
using NEXORA.API.Middleware;
using NEXORA.Application;
using NEXORA.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(opciones =>
        opciones.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddCors(opciones =>
    opciones.AddPolicy("Frontend", politica =>
        politica.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()));

var app = builder.Build();

app.UseMiddleware<ManejoErroresMiddleware>();
app.UseCors("Frontend");
app.MapControllers();

app.Run();