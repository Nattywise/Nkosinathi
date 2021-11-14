using Bank.Controllers;
using Bank.Model;
using Bank.Test.API;
using Bank.Utils;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;

namespace Bank.Test
{
  [TestClass]
  public class CustomerControllerTest : BaseTest
  {
    CustomerController customerController;
    public CustomerControllerTest()
      : base()
    {
       customerController = new CustomerController(BankContext);
    }

    [TestMethod]
    public void TestCustomerCreatePostive()
    {
      //Arrange
      Customer customer = new Customer()
      {
        Name = "Donald Trump"
      };

      //Act
      OkObjectResult okObjectResult = customerController.Post(customer) as OkObjectResult;

      //Assert
      Assert.IsNotNull(okObjectResult);
      Assert.AreEqual((int)HttpStatusCode.OK, (int)okObjectResult.StatusCode);
    }

    [TestMethod]
    public void TestCustomerCreateNegative()
    {
      //Arrange
      Customer customer = new Customer()
      {
        Name = ""
      };

      //Act
      BadRequestObjectResult badRequestObjectResult = customerController.Post(customer) as BadRequestObjectResult;

      //Assert
      Assert.IsNotNull(badRequestObjectResult);
      Assert.AreEqual((int)HttpStatusCode.BadRequest, (int)badRequestObjectResult.StatusCode);
    }

    [TestMethod]
    public void TestCustomerUpdatePostive()
    {
      //Arrange
      DatabaseInitialize();
      Customer customer = new Customer()
      {
        CustomerID = 1,
        Name = "Arisha Barron-Nee Naidoo"
      };

      //Act
      OkObjectResult okObjectResult = customerController.Put(customer) as OkObjectResult;

      //Assert
      Assert.IsNotNull(okObjectResult);
      Assert.AreEqual((int)HttpStatusCode.OK, (int)okObjectResult.StatusCode);
    }

    [TestMethod]
    public void TestCustomerUpdateNegative()
    {
      //Arrange
      Customer customer = new Customer()
      {
        CustomerID = 9999,
        Name = "Arisha Barron-Nee Naidoo"
      };

      //Act
      BadRequestObjectResult badRequestObjectResult = customerController.Put(customer) as BadRequestObjectResult;

      //Assert
      Assert.IsNotNull(badRequestObjectResult);
      Assert.AreEqual((int)HttpStatusCode.BadRequest, (int)badRequestObjectResult.StatusCode);
    }
  }
}
