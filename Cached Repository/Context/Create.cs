namespace Cached_Repository.Context;

public class Create : IDesignTimeDbContextFactory<MyDBContext>
{
  public MyDBContext CreateDbContext(string[] args)
  {
    var configurationBuilder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("..\Context\\JsonFile\\appsettings.json", optional: true, reloadOnChange: true);

    IConfigurationRoot configuration = configurationBuilder.Build();
    string connectionString = configuration.GetConnectionString("db")!;

    DbContextOptionsBuilder<MyDBContext> optionsBuilder = new DbContextOptionsBuilder<MyDBContext>()
        .UseSqlServer(connectionString, x => x.MigrationsAssembly("Cached Repository"));

    if (!optionsBuilder.IsConfigured)
      throw new Exception("context doesn't configured");
    else
      return new MyDBContext(optionsBuilder.Options);
  }
}
