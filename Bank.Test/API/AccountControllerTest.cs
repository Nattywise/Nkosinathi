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
  public class AccountControllerTest : BaseTest
  {
    AccountController accountController;
    public AccountControllerTest()
      : base()
    {
      accountController = new AccountController(BankContext);
    }

    [TestMethod]
    public void TestAccountCreatePostive()
    {
      DatabaseInitialize();

      //Arrange
      Account account = new Account()
      {
        CustomerID = 1,
        AccountName = "Savings Account"
      };

      decimal initialDepopsit = 1000;

      //Act
      OkObjectResult okObjectResult = accountController.Post(initialDepopsit, account) as OkObjectResult;

      //Assert
      Assert.IsNotNull(okObjectResult);
      Assert.AreEqual((int)HttpStatusCode.OK, (int)okObjectResult.StatusCode);


    }

    [TestMethod]
    public void TestAccountCreateNegative()
    {

      //Arrange
      Account account = new Account()
      {
        CustomerID = 1,
        AccountName = "Savings Account"
      };

      decimal initialDepopsit = 0;
      //Act
      BadRequestObjectResult badRequestObjectResult = accountController.Post(initialDepopsit, account) as BadRequestObjectResult;

      //Assert
      Assert.IsNotNull(badRequestObjectResult);
      Assert.AreEqual((int)HttpStatusCode.BadRequest, (int)badRequestObjectResult.StatusCode);
    }

    [TestMethod]
    public void TestAccountTestBalancePostive()
    {
      DatabaseInitialize();

      //Arrange
      Account account = new Account()
      {
        CustomerID = 1,
        AccountName = "Savings Account"
      };

      decimal initialDepopsit = 1000;

      //Act
      OkObjectResult okObjectResult = accountController.Post(initialDepopsit, account) as OkObjectResult;

      //Assert
      Assert.IsNotNull(okObjectResult);
      Assert.AreEqual((int)HttpStatusCode.OK, (int)okObjectResult.StatusCode);


      ObjectResult objectResult = accountController.GetAccountBalance(id:1) as ObjectResult;
      //Assert
      Assert.IsNotNull(objectResult);
      Assert.AreEqual(initialDepopsit, (decimal)objectResult.Value);


    }

    
  }
}
