using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Nikitin.FederalSubjects.Database;

namespace Nikitin.FederalSubjects.Infrastructure.Tests;

public class InMemoryDbContext : AppDbContext
{
    private readonly string _databaseName = Guid.NewGuid().ToString("N");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseInMemoryDatabase(_databaseName, x => x.EnableNullChecks(false))
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
}
