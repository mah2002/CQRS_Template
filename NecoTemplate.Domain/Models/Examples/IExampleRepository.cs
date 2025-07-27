namespace NecoTemplate.Domain.Models.Examples;

public interface IExampleRepository
{
    Task<Example?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Example example);
    bool Delete(Guid exampleId);
}
