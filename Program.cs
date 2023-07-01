using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

public class Program {
  public static void Main(string[] args) {
    new WebHostBuilder()
      .UseKestrel()
      .UseContentRoot(Directory.GetCurrentDirectory())
      .UseStartup<Startup>()
      .Build()
      .Run();
  }
}