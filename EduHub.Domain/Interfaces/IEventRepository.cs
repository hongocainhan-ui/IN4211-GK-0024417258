using EduHub.Domain.Entities;

namespace EduHub.Domain.Interfaces;

// ===== Câu 3b – Interface IEventRepository =====
// Câu 3a – DIP: EventService phụ thuộc vào interface này thay vì new SqlConnection(...) (dòng 8 của đề).
// Câu 1a – Trừu tượng hoá: chỉ nói "làm được gì", không nói lưu ở đâu.
// Câu 5a – Interface nằm ở tầng Domain, lớp cài đặt nằm ở Infrastructure.
public interface IEventRepository
{
    IReadOnlyList<Event> GetByClubId(Guid clubId);
    Event? GetById(Guid id);
    void Add(Event ev);
    bool Delete(Guid id);
}
