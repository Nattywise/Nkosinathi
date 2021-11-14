using Bank.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Utils
{
  public class DatabaseGenerator
  {
        /// <summary>
        /// this method will populate the customer table with the list of customers provided to test the application and save the changes to the model
        /// </summary>
        /// <param name="serviceProvider"></param>
    public static void Initialize(IServiceProvider serviceProvider)
    {
      using (var context = new BankContext(serviceProvider.GetRequiredService<DbContextOptions<BankContext>>()))
      {
       
        if (context.Customers.Any())
        {
          return;   // Data was already seeded
        }

        context.Customers.AddRange(
            new Customer
            {
              CustomerID = 1,
              Name = "Arisha Barron"

            },
            new Customer
            {
              CustomerID = 2,
              Name = "Branden Gibson"

            },
            new Customer
            {
              CustomerID = 3,
              Name = "Rhonda Church"

            },
            new Customer
            {
              CustomerID = 4,
              Name = "Georgina Hazel"
            }
           );

        context.SaveChanges();
      }
    }
  }
}