using Microsoft.EntityFrameworkCore;
using SpaceReservationSystem.Infrastructure;
using SpaceReservationSystem.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration); // Agrega la infraestructura y la base de datos al contenedor de servicios

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// builder.Services.AddSwaggerGen(options =>
// {
//     options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//     {
//         Name = "Authorization",
//         Type = SecuritySchemeType.Http,
//         Scheme = "bearer",
//         BearerFormat = "JWT",
//         In = ParameterLocation.Header,
//         Description = "Ingrese el token JWT. Ejemplo: Bearer {token}"
//     });

//      Configuración Bearer JWT
//     options.AddSecurityRequirement(new OpenApiSecurityRequirement
//     {
//         {
//             new OpenApiSecurityScheme
//             {
//                 Reference = new OpenApiReference
//                 {
//                     Type = ReferenceType.SecurityScheme,
//                     Id = "Bearer"
//                 }
//             },
//             Array.Empty<string>()
//         }
//     });
// });
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