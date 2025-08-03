namespace Losev.Domain.Abstractions;

public abstract class Entity
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; } = false; // Soft delete için eklenmiştir.
    protected Entity()
    {
        Id = Guid.NewGuid();
    }
}
