using Bank.Utils;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bank
{
  public class Program
  {
    public static void Main(string[] args)
    {
      //1. Get the IWebHost which will host this application.
      var host = CreateWebHostBuilder(args).Build();

      //2. Find the service layer within our scope.
      using (var scope = host.Services.CreateScope())
      {
        //3. Get the instance of BoardGamesDBContext in our services layer
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<BankContext>();

        //4. Call the DatabaseGenerator to create sample data
        DatabaseGenerator.Initialize(services);
      }

      //Continue to run the application
      host.Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
