using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Migrations;

public class Program
{
    public static int Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = config.GetConnectionString("Colleger")
            ?? "Server=localhost;Database=Colleger;Trusted_Connection=True;TrustServerCertificate=True;";

        var services = new ServiceCollection()
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(Program).Assembly).For.Migrations())
            .BuildServiceProvider(false);

        using var scope = services.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

        var command = args.FirstOrDefault()?.ToLowerInvariant();
        switch (command)
        {
            case "down":
                runner.MigrateDown(0);
                break;
            case "list":
                runner.ListMigrations();
                break;
            case "up":
            default:
                runner.MigrateUp();
                break;
        }

        return 0;
    }
}
