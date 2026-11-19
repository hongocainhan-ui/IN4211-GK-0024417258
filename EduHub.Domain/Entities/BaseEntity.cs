namespace EduHub.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; private protected set; } = Guid.NewGuid();
}
