namespace Cached_Repository.Repository;

public class CachedProductRepository : IProductRepository
{
  private readonly IProductRepository _repository;
  private readonly IMemoryCache _cache;
  private readonly TimeSpan _cacheDuration = TimeSpan.FromSeconds(10);

  public CachedProductRepository(IProductRepository repository, IMemoryCache cache)
  {
    _repository = repository;
    _cache = cache;
  }

  public async Task<Product> GetProductByIdAsync(int id)
  {
    string cacheKey = $"Product_{id}";

    if (!_cache.TryGetValue(cacheKey, out Product product))
    {
      product = await _repository.GetProductByIdAsync(id);

      if (product != null)
      {
        _cache.Set(cacheKey, product, _cacheDuration);
      }
    }

    return product;
  }

  public async Task<IEnumerable<Product>> GetAllProductsAsync()
  {
    const string cacheKey = "AllProducts";

    if (!_cache.TryGetValue(cacheKey, out IEnumerable<Product> products))
    {
      products = await _repository.GetAllProductsAsync();
      _cache.Set(cacheKey, products, _cacheDuration);
    }

    return products;
  }

  public async Task AddProductAsync(Product product)
  {
    await _repository.AddProductAsync(product);
    InvalidateCache();
  }

  public async Task UpdateProductAsync(Product product)
  {
    await _repository.UpdateProductAsync(product);
    InvalidateCache();
  }

  public async Task DeleteProductAsync(int id)
  {
    await _repository.DeleteProductAsync(id);
    InvalidateCache();
  }

  private void InvalidateCache()
  {
    _cache.Remove("AllProducts");
  }
}