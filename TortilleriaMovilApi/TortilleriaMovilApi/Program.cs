using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TortilleriaMovilApi.Data;
using TortilleriaMovilApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar Kestrel para usar solo IPv4 en el puerto 5050
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(System.Net.IPAddress.Any, 5050); // Solo IPv4
});

builder.WebHost.UseUrls("http://*:5050", "https://*:5050");

// Configuraci�n de CORS para permitir acceso desde cualquier origen
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configuraci�n de la base de datos MySQL
var connectionString = builder.Configuration.GetConnectionString("MySQLConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Clave secreta para JWT - Debe ser la misma que en JwtHelper.cs
const string secretKey = "Esta_Clave_Secreta_Es_Suficientemente_Larga_Para_HMAC_SHA256";
var key = Encoding.ASCII.GetBytes(secretKey);

// Configurar autenticaci�n con JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Agregar servicios al contenedor
builder.Services.AddScoped<AuthService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuraci�n del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Comentada la redirecci�n HTTPS para permitir acceso por HTTP
// app.UseHttpsRedirection();

// Habilitar CORS antes de la autenticaci�n y autorizaci�n
app.UseCors();

app.UseAuthentication();  // Primero autenticaci�n
app.UseAuthorization();   // Luego autorizaci�n

app.MapControllers();

Console.WriteLine($"Aplicaci�n ejecut�ndose en: http://localhost:5050");
Console.WriteLine($"IP local: http://{System.Net.Dns.GetHostName()}:5002");
Console.WriteLine($"Swagger disponible en: http://localhost:5050/swagger");

app.Run();