using Bank.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Utils
{
  public class AccountUtils
  {
    private BankContext _context;

    public AccountUtils(BankContext context)
    {
      _context = context;

    }
    public IEnumerable<Account> RetrieveAll()
    {
      return _context.Accounts.ToList();
    }

    public Result Validate(Account account, decimal initialDeposit = 0)
    {
      Result result = new Result();
      if (account.AccountID == 0 && account.CustomerID == 0)
      {
        result.Message = "Customer can't be blank.\n";
      }

      if (account.AccountID == 0 && account.CustomerID > 0)
      {
        CustomerUtils customerUtils = new CustomerUtils(_context);
        Customer customer = customerUtils.Retrieve(account.CustomerID);
        if (customer == null)
        {
          result.Message = $"Failed to retrieve customer with customerId: {account.CustomerID}.";
        }
      }

      if (string.IsNullOrEmpty(account.AccountName))
      {
        result.Message += "Account name can't be blank.\n";
      }

      if (account.AccountID == 0 && initialDeposit == 0)
      {
        result.Message += "Initial Deposit can't be zero.\n";
      }

      result.IsSuccess = string.IsNullOrEmpty(result.Message);

      return result;
    }

    public decimal RetrieveBalance(int id)
    {
      TransactionUtils transactionUtils = new TransactionUtils(_context);

      decimal accountBalance = transactionUtils.RetrieveUsingAccountId(id).Sum(t => t.Amount);

      return accountBalance;
    }

    public Account Retrieve(int id)
    {
      Account account = _context.Accounts.Where(c => c.AccountID == id).FirstOrDefault();
      return account;
    }

    public List<Account> RetrieveUsingCustomerId(int customerID)
    {
      List<Account> accountList = _context.Accounts.Where(c => c.CustomerID == customerID).ToList();
      return accountList;
    }

    public Result Save(Account account, decimal initialDeposit = 0)
    {
      Result result = new Result();

      bool isNewAccount = false;
      try
      {
        if (account.AccountID == 0)
        {
          _context.Accounts.Add(account);
          isNewAccount = true;
        }
        else
        {
          Account accountExisting = Retrieve(account.AccountID);
          accountExisting.AccountName = account.AccountName;
          _context.Accounts.Update(accountExisting);
        }
        _context.SaveChangesAsync();

        if (isNewAccount)
        {
          TransactionCreateInitialDeposit(account, initialDeposit);
        }

        result.IsSuccess = true;
      }
      catch (Exception exception)
      {
        result.Message = exception.Message;
      }
      return result;
    }

    public Result Delete(int accountId)
    {
      Result result = new Result();

      try
      {
        Account accountExisting = Retrieve(accountId);

        if (accountExisting != null)
        {
          TransactionUtils transactionUtils = new TransactionUtils(_context);
          int transactionCount = transactionUtils.RetrieveUsingAccountId(accountId).Count();
          if (transactionCount == 0)
          {
            _context.Accounts.Remove(accountExisting);
            _context.SaveChangesAsync();
            result.IsSuccess = true;
          }
          else
          {
            result.Message = $"Failed to delete account with accountId: {accountId}, Reason, Account has active transactions.";
          }
        }
        else
        {
          result.Message = $"Failed to delete account with accountId: {accountId}, Reason, Account doesn't exist";
        }
      }
      catch (Exception exception)
      {
        result.Message = exception.Message;
      }
      return result;
    }

    private void TransactionCreateInitialDeposit(Account account, decimal initialDeposit)
    {
      Transaction transaction = new Transaction()
      {
        AccountID = account.AccountID,
        Description = "Initial Deposit",
        Amount = initialDeposit
      };
      TransactionUtils transactionUtils = new TransactionUtils(_context);
      transactionUtils.Save(transaction);
    }
  }
}
