using Microsoft.EntityFrameworkCore;
using SpaceReservationSystem.Infrastructure;
using SpaceReservationSystem.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration); // Agrega la infraestructura y la base de datos al contenedor de servicios

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Habilita la autenticación
app.UseAuthorization(); // Habilita la autorización
app.MapControllers();
app.Run();