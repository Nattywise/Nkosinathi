using Bank.Utils;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Test.API
{
  public class BaseTest
  {
    public BankContext BankContext { get; set; }

    public BaseTest()
    {
      BankContext = new BankContext(new DbContextOptions<BankContext>());
    }

    public void DatabaseInitialize()
    {
      string[] args = null;

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
    }

    private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
      WebHost.CreateDefaultBuilder(args)
          .UseStartup<Startup>();
  }
}
