namespace Cached_Repository.Helpers;

public static class CUI
{
  public static void LogInBlue(string message)
  {
    var originalColor = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(message);
    Console.ForegroundColor = originalColor;
  }
}
