using NecoTemplate.Domain.Models.Examples;
using NecoTemplate.Infrastructure.Database;

namespace NecoTemplate.Infrastructure.Repositories;

public sealed class ExampleRepository : Repository<Example>, IExampleRepository
{
    public ExampleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public bool Delete(Guid exampleId)
    {
        var dbExample = DbContext.Examples.Find(exampleId);
        if (dbExample is null)
        {
            return false;
        }
        dbExample.IsVisible=false;
        DbContext.Update(dbExample);
        return true;
    }
}
