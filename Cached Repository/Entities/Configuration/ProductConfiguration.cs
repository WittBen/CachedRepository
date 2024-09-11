namespace Cached_Repository.Entities.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    builder.HasKey(l => l.Id);
    builder.Property(l => l.Description).IsRequired();
    builder.Property(l => l.Timestamp).IsRowVersion();
  }
}