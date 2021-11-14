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
using System.Collections.Generic;
using System.Net;

namespace Bank.Test
{
  [TestClass]
  public class TransactionControllerTest : BaseTest
  {
    AccountController accountController;
    TransactionController transactionController;

    public TransactionControllerTest()
      : base()
    {
      accountController = new AccountController(BankContext);
      transactionController = new TransactionController(BankContext);
    }

    [TestMethod]
    public void TestTransactionListPositive()
    {
      DatabaseInitialize();

      //Arrange
      #region Create Account
      Account account = new Account()
      {
        CustomerID = 1,
        AccountName = "Savings Account"
      };

      decimal initialDepopsitAccount = 1000;

      //Act
      OkObjectResult okObjectResultAccount = accountController.Post(initialDepopsitAccount, account) as OkObjectResult;

      //Assert
      Assert.IsNotNull(okObjectResultAccount);
      Assert.AreEqual((int)HttpStatusCode.OK, (int)okObjectResultAccount.StatusCode);
      #endregion

      #region Transaction List
      //Arrange
      TransactionTransfer transactionTransfer = new TransactionTransfer()
      {
        AccountIDFrom = 1,
        AccountIDTo = 2,
        Amount = 250
      };

      //Act
      ObjectResult objectResult = transactionController.GetTransactionUsingAccountId(accountID: 1) as ObjectResult;
      //Assert
      Assert.IsNotNull(objectResult);

      //There should be only one Transaction in the list
      Assert.AreEqual(1, ((List<Transaction>)objectResult.Value).Count);
      #endregion
    }

    [TestMethod]
    public void TestTransferPositive()
    {
      DatabaseInitialize();

      //Arrange
      #region Create Account 1
      Account account1 = new Account()
      {
        CustomerID = 1,
        AccountName = "Savings Account"
      };

      decimal initialDepopsitAccount1 = 1000;

      //Act
      OkObjectResult okObjectResultAccount1 = accountController.Post(initialDepopsitAccount1, account1) as OkObjectResult;

      //Assert
      Assert.IsNotNull(okObjectResultAccount1);
      Assert.AreEqual((int)HttpStatusCode.OK, (int)okObjectResultAccount1.StatusCode);
      #endregion

      #region Create Account 2
      Account account2 = new Account()
      {
        CustomerID = 1,
        AccountName = "Cheque Account"
      };

      decimal initialDepopsitAccount2 = 1000;

      //Act
      OkObjectResult okObjectResultAccount2 = accountController.Post(initialDepopsitAccount2, account2) as OkObjectResult;

      //Assert
      Assert.IsNotNull(okObjectResultAccount2);
      Assert.AreEqual((int)HttpStatusCode.OK, (int)okObjectResultAccount2.StatusCode);
      #endregion

      #region Transfer
      //Arrange
      TransactionTransfer transactionTransfer = new TransactionTransfer()
      {
        AccountIDFrom = 1,
        AccountIDTo = 2,
        Amount = 250
      };

      //Act
      OkObjectResult okObjectResultTransfer = transactionController.Post(transactionTransfer) as OkObjectResult;
      //Assert
      Assert.IsNotNull(okObjectResultTransfer);
      Assert.AreEqual((int)HttpStatusCode.OK, (int)okObjectResultTransfer.StatusCode);

      #endregion
    }

    [TestMethod]
    public void TestTransferNegative()
    {
      DatabaseInitialize();

      //Arrange
      #region Create Account 1
      Account account1 = new Account()
      {
        CustomerID = 1,
        AccountName = "Savings Account"
      };

      decimal initialDepopsitAccount1 = 1000;

      //Act
      OkObjectResult okObjectResultAccount1 = accountController.Post(initialDepopsitAccount1, account1) as OkObjectResult;

      //Assert
      Assert.IsNotNull(okObjectResultAccount1);
      Assert.AreEqual((int)HttpStatusCode.OK, (int)okObjectResultAccount1.StatusCode);
      #endregion

      #region Create Account 2
      Account account2 = new Account()
      {
        CustomerID = 1,
        AccountName = "Cheque Account"
      };

      decimal initialDepopsitAccount2 = 1000;

      //Act
      OkObjectResult okObjectResultAccount2 = accountController.Post(initialDepopsitAccount2, account2) as OkObjectResult;

      //Assert
      Assert.IsNotNull(okObjectResultAccount2);
      Assert.AreEqual((int)HttpStatusCode.OK, (int)okObjectResultAccount2.StatusCode);
      #endregion

      #region Transfer
      //Arrange
      TransactionTransfer transactionTransfer = new TransactionTransfer()
      {
        AccountIDFrom = 1,
        AccountIDTo = 2,
        Amount = 1250    //this is more than the available balance in AccountFrom, this should fail the test
      };

      //Act
      BadRequestObjectResult badRequestObjectResult = transactionController.Post(transactionTransfer) as BadRequestObjectResult;

      //Assert
      Assert.IsNotNull(badRequestObjectResult);
      Assert.AreEqual((int)HttpStatusCode.BadRequest, (int)badRequestObjectResult.StatusCode);

      #endregion
    }
  }
}
