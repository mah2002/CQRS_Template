namespace NecoTemplate.Domain.Abstractions;

public class EntityBase
{
    public Guid Id { get; set; }
    public DateTime LastModified { get; set; }=DateTime.UtcNow;
    public bool IsVisible { get; set; } = true;
    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.ToList();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
