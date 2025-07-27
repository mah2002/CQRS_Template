using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace NecoTemplate.Infrastructure.Database;

public static class ApplyMigration
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        IServiceScope scope = app.ApplicationServices.CreateScope();
        ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    }
}
