public class Program
{
  private static Timer _timer;
  private static IProductRepository _productRepository;
  private static IMemoryCache _cache;

  static void Main(string[] args)
  {
    var ctx = new Create().CreateDbContext(args);

    _cache = new MemoryCache(new MemoryCacheOptions());
    _productRepository = new CachedProductRepository(new ProductRepository(ctx), _cache);
    _timer = new Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

    // Keep the application running
    Console.Clear();
    Console.WriteLine("Press [Enter] to exit...");
    Console.ReadLine();

    // Dispose of the timer when done
    _timer?.Dispose();
  }

  private static async void TimerCallback(object state)
  {
    int productId = new Random().Next(1, 10);

    try
    {
      var product = await _productRepository.GetProductByIdAsync(productId);

      if (product != null)
      {
        Console.WriteLine($"Product found: Id = {product.Id}, Name = {product.Description}");
      }
      else
      {
        Console.WriteLine($"Product with Id {productId} not found.");
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"An error occurred: {ex.Message}");
    }
  }
}
