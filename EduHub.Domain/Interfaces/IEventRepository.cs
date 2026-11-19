using EduHub.Domain.Entities;

namespace EduHub.Domain.Interfaces;

public interface IEventRepository
{
    IReadOnlyList<Event> GetByClubId(Guid clubId);
    Event? GetById(Guid id);
    void Add(Event ev);
    bool Delete(Guid id);
}
