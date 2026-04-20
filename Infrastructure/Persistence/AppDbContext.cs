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

        // --- CONFIGURACIONES TECNICAS ---
        modelBuilder.Entity<Sector>()
            .Property(s => s.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Seat>()
            .Property(s => s.Version)
            .IsConcurrencyToken();

        // RESTRICCION DE UNICIDAD: Evita duplicados fisicos en la BD
        modelBuilder.Entity<Seat>()
            .HasIndex(s => s.SeatNumber)
            .IsUnique();

        // --- PRECARGA DE DATOS (SEEDING) ---
        
        // 1. Crear el Evento
        modelBuilder.Entity<Event>().HasData(new Event 
        { 
            Id = 1, 
            Name = "Concierto de Rock", 
            EventDate = new DateTime(2026, 12, 10, 21, 0, 0, DateTimeKind.Utc), 
            Venue = "Estadio Central", 
            Status = "Active" 
        });

        // 2. Crear los Sectores
        modelBuilder.Entity<Sector>().HasData(
            new Sector { Id = 1, EventId = 1, Name = "Platea Baja", Price = 5000, Capacity = 50 },
            new Sector { Id = 2, EventId = 1, Name = "Platea Alta", Price = 8000, Capacity = 50 }
        );

        // 3. Generacion dinamica de los 100 asientos con IDs DETERMINISTICOS
        var seats = new List<Seat>();
        int[] sectorIds = { 1, 2 };

        foreach (var sId in sectorIds)
        {
            string rowLabel = (sId == 1) ? "Baja" : "Alta";
            int offset = (sId - 1) * 50; 

            for (int i = 1; i <= 50; i++) 
            {
                int seatNumber = i + offset;

                seats.Add(new Seat { 
                    // El ID se genera basado en el numero, asi no cambia nunca
                    Id = new Guid($"00000000-0000-0000-{sId:D4}-0000{seatNumber:D8}"), 
                    SectorId = sId, 
                    RowIdentifier = rowLabel, 
                    SeatNumber = seatNumber, 
                    Status = "Available", 
                    Version = 1 
                });
            }
        }
        modelBuilder.Entity<Seat>().HasData(seats);
    }
}