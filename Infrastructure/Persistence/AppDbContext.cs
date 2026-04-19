using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Event> Events { get; set; }
    public DbSet<Sector> Sectors { get; set; }
    public DbSet<Seat> Seats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // --- TUS CONFIGURACIONES TÉCNICAS ---
    modelBuilder.Entity<Sector>()
        .Property(s => s.Price)
        .HasPrecision(18, 2);

    modelBuilder.Entity<Seat>()
        .Property(s => s.Version)
        .IsConcurrencyToken();

    // --- PRECARGA DE DATOS (SEEDING) ---
    
    // 1. Crear el Evento (Requerido: 1 Evento)
    modelBuilder.Entity<Event>().HasData(new Event 
    { 
        Id = 1, 
        Name = "Concierto de Rock", 
        EventDate = new DateTime(2026, 12, 10, 21, 0, 0, DateTimeKind.Utc), 
        Venue = "Estadio Central", 
        Status = "Active" 
    });

    // 2. Crear los Sectores (Requerido: 2 Sectores)
    modelBuilder.Entity<Sector>().HasData(
        new Sector { Id = 1, EventId = 1, Name = "Platea Alta", Price = 5000, Capacity = 50 },
        new Sector { Id = 2, EventId = 1, Name = "Campo VIP", Price = 8000, Capacity = 50 }
    );

    // 3. Crear las Butacas (Requerido: 50 por sector = 100 en total)
    var seats = new List<Seat>();
    
    // Generamos 50 para el Sector 1
    for (int i = 1; i <= 50; i++) {
        seats.Add(new Seat { 
            Id = Guid.NewGuid(), 
            SectorId = 1, 
            RowIdentifier = "A", 
            SeatNumber = i, 
            Status = "Available", 
            Version = 1 
        });
    }
    
    // Generamos 50 para el Sector 2
    for (int i = 1; i <= 50; i++) {
        seats.Add(new Seat { 
            Id = Guid.NewGuid(), 
            SectorId = 2, 
            RowIdentifier = "VIP", 
            SeatNumber = i, 
            Status = "Available", 
            Version = 1 
        });
    }

    modelBuilder.Entity<Seat>().HasData(seats);
}
}