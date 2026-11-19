using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Infrastructure.Repositories;

public class InMemoryEventRepository : IEventRepository
{
    private readonly List<Event> _events = new();

    // Registered as a singleton, so concurrent requests share this list.
    private readonly object _lock = new();

    public IReadOnlyList<Event> GetByClubId(Guid clubId)
    {
        lock (_lock)
        {
            return _events.Where(e => e.ClubId == clubId).ToList();
        }
    }

    public Event? GetById(Guid id)
    {
        lock (_lock)
        {
            return _events.FirstOrDefault(e => e.Id == id);
        }
    }

    public void Add(Event ev)
    {
        lock (_lock)
        {
            _events.Add(ev);
        }
    }

    public bool Delete(Guid id)
    {
        lock (_lock)
        {
            return _events.RemoveAll(e => e.Id == id) > 0;
        }
    }
}
