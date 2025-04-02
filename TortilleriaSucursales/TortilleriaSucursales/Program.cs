using Microsoft.EntityFrameworkCore;
using TortilleriaSucursales.Data;
using TortilleriaSucursales.Repositories;
using TortilleriaSucursales.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar la conexión a MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySQLConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySQLConnection"))
    ));

// Inyección de dependencias
builder.Services.AddScoped<ISucursalRepository, SucursalRepository>();
builder.Services.AddScoped<ISucursalService, SucursalService>();

// Configuración de CORS para permitir acceso desde cualquier origen
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()  // Permite cualquier origen
              .AllowAnyHeader()  // Permite cualquier cabecera
              .AllowAnyMethod(); // Permite cualquier método
    });
});

builder.Services.AddControllers();
// Aprende más sobre configurar Swagger/OpenAPI en https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el pipeline de la solicitud HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usar CORS antes de la autorización
app.UseCors();  // Habilitar CORS

// Habilitar HTTPS y redirigir tráfico HTTP a HTTPS (opcional, puedes deshabilitar esto si no lo necesitas)
app.UseHttpsRedirection();

// Configuración de autorización
app.UseAuthorization();

// Configurar la ruta para los controladores
app.MapControllers();

// Escuchar en todas las interfaces de red, no solo en localhost (dirección 0.0.0.0)
// Cambié el puerto a 9000, puedes poner el que necesites
app.Run("http://0.0.0.0:9000"); // Cambié el puerto a 9000

