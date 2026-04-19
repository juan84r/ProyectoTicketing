using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EventRepository : IEventRepository
{
    private readonly AppDbContext _context;

    public EventRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Event>> GetAllEventsAsync()
    {
        return await _context.Events.ToListAsync();
    }

    public async Task<Event?> GetEventByIdAsync(int id)
    {
        // IMPORTANTE: Cargamos los Sectores y sus Asientos (Eager Loading)
        return await _context.Events
            .Include(e => e.Sectors)
                .ThenInclude(s => s.Seats)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}