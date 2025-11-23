using Commercify.Core.Shared;
using Commercify.Infrastructure.Database;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Commercify.UnitTests.TestSetup;

public class DatabaseBuilder : IDisposable
{
    private SqliteConnection? _connection;
    public IDbContext CreateDbContext()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(_connection)
            .Options;
        var context = new AppDbContext(options);
        return context; 
    }

    public void Dispose()
    {
        _connection?.Close();
        _connection?.Dispose();
    }
}
