namespace Cached_Repository.Context;

public class MyDBContext : DbContext
{
  public MyDBContext(DbContextOptions<MyDBContext> options)
  : base(options)
  {
    this.EnsureSeedData();
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.LogTo(message => CUI.LogInBlue(message), new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                         .EnableSensitiveDataLogging();
    base.OnConfiguring(optionsBuilder);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(Product).Assembly);
  }

  public DbSet<Product> Products { get; set; }
}
