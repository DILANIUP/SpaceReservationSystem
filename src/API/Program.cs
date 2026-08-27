using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SpaceReservationSystem.Infrastructure;
using SpaceReservationSystem.Infrastructure.Data;
using SpaceReservationSystem.API.Services.Faculty;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration); // Agrega la infraestructura y la base de datos al contenedor de servicios
builder.Services.AddScoped<FacultyService>(); // Servicios de la API

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT. Ejemplo: Bearer {token}"
    });

    // Configuración Bearer JWT
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();
app.UseMiddleware<SpaceReservationSystem.API.Middlewares.ExceptionHandlingMiddleware>(); // Manejador global de errores en peticiones

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run(); 
