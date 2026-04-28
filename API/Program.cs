using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Application.Interfaces;
using Application.UseCases.Events.Queries;
using Microsoft.EntityFrameworkCore;
using Application.UseCases.Auth;

var builder = WebApplication.CreateBuilder(args);

// ==========================================================================
// 1. CONFIGURACION DE SERVICIOS BASICOS
// ==========================================================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Obligatorio para documentar la API

// ==========================================================================
// 2. CONFIGURACION DE LA BASE DE DATOS (PostgreSQL - Puerto 5433)
// ==========================================================================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// ==========================================================================
// 3. INYECCION DE DEPENDENCIAS (Jerarquia de Capas)
// ==========================================================================

// REPOSITORIOS (Infrastructure)
// "Cuando se pida la interfaz IEventRepository, entregar la implementacion EventRepository"
builder.Services.AddScoped<IEventRepository, EventRepository>();

// CASOS DE USO / HANDLERS (Application)
// Estos contienen la lógica de negocio para las consultas (Queries)
builder.Services.AddScoped<GetEventsHandler>();
builder.Services.AddScoped<GetSeatsBySectorHandler>();

//para el login
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<LoginHandler>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<CreateReservationHandler>();
builder.Services.AddScoped<RegisterHandler>();
builder.Services.AddScoped<LoginHandler>();

//auditoria
builder.Services.AddScoped<IAuditRepository, AuditRepository>();


builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// ==========================================================================
// 4. CONFIGURACIÓN DEL PIPELINE DE LA APP (Middleware)
// ==========================================================================

// Activar Swagger en desarrollo para probar los endpoints desde el navegador
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll");
// Mapea los controladores para que las rutas [Route("api/v1/[controller]")] funcionen
app.MapControllers();

app.Run();