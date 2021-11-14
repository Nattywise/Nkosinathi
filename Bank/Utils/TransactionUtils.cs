using Bank.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Utils
{
  public class TransactionUtils
  {
    private BankContext _context;

    public TransactionUtils(BankContext context)
    {
      _context = context;
    }
    public IEnumerable<Transaction> RetrieveAll()
    {
      return _context.Transactions.ToList();
    }

    public Result Validate(Transaction transaction)
    {
      Result result = new Result();
      if (transaction.TransactionID == 0 && transaction.AccountID == 0)
      {
        result.Message += "AccountID can't be blank.\n";
      }

      if (transaction.TransactionID == 0 && transaction.AccountID > 0)
      {
        AccountUtils accountUtils = new AccountUtils(_context);
        Account account = accountUtils.Retrieve(transaction.AccountID);
        if (account == null)
        {
          result.Message += $"Failed to retrieve Account with AccountID: {transaction.AccountID}.\n";
        }
      }

      if (string.IsNullOrEmpty(transaction.Description))
      {
        result.Message += "Transaction Description can't be blank.\n";
      }

      if (transaction.Amount == 0)
      {
        result.Message += "Transaction Amount can't be zero.";
      }
      result.IsSuccess = string.IsNullOrEmpty(result.Message);
      return result;
    }

    public Result ValidateTransfer(TransactionTransfer transactionTransfer)
    {
      Result result = new Result();

      if (transactionTransfer.AccountIDFrom == 0)
      {
        result.Message = "AccountID From can't be blank.\n";
      }

      if (transactionTransfer.AccountIDFrom > 0)
      {
        AccountUtils accountUtils = new AccountUtils(_context);
        Account accountFrom = accountUtils.Retrieve(transactionTransfer.AccountIDFrom);
        if (accountFrom == null)
        {
          result.Message +=$"Failed to retrieve Account From with AccountID: {transactionTransfer.AccountIDFrom}.\n";
        }
        else
        {
          //get balance to ensure there is enough cash to do a transfer...there is no overdraft facility
          decimal accountBalance = accountUtils.RetrieveBalance(transactionTransfer.AccountIDFrom);
          if(accountBalance < transactionTransfer.Amount)
          {
            result.Message += $"Failed to transfer, Account From has insufficient funds.\n";
          }
        }
      }

      //ensures the account being credited exists
      if (transactionTransfer.AccountIDTo == 0)
      {
        result.Message += "AccountID To can't be blank.\n";
      }

      if (transactionTransfer.AccountIDTo > 0)
      {
        AccountUtils accountUtils = new AccountUtils(_context);
        Account accountTo = accountUtils.Retrieve(transactionTransfer.AccountIDTo);
        if (accountTo == null)
        {
          result.Message += $"Failed to retrieve Account To with AccountID: {transactionTransfer.AccountIDTo}.\n";
        }
      }

      if (transactionTransfer.Amount == 0)
      {
        result.Message += "Transfer Amount can't be zero.";
      }
      result.IsSuccess = string.IsNullOrEmpty(result.Message);
      return result;
    }

    public Result Transfer(TransactionTransfer transactionTransfer)
    {
      Result result = new Result();
      try
      {
        //create te new transaction entry and set it to negative value indicating it is a debit txn type
        Transaction transactionFrom = new Transaction()
        {
          AccountID = transactionTransfer.AccountIDFrom,
          Amount = transactionTransfer.Amount * -1,
          Description = $"Transfer to Account:{transactionTransfer.AccountIDTo}",
          IsTransfer = true,
        };

        Save(transactionFrom);
        //create the new acccount with the positive transfer amount indicating it is a credit txn type
        Transaction transactionTo = new Transaction()
        {
          AccountID = transactionTransfer.AccountIDTo,
          Amount = transactionTransfer.Amount,
          Description = $"Transfer from Account:{transactionTransfer.AccountIDFrom}",
          IsTransfer = true,
        };

        Save(transactionTo);
        result.IsSuccess = true;
      }
      catch (Exception exception)
      {
        result.Message = exception.Message;
      }
      return result;
    }

    public Transaction Retrieve(int id)
    {
      Transaction transaction = _context.Transactions.Where(c => c.TransactionID == id).FirstOrDefault();
      return transaction;
    }

    public List<Transaction> RetrieveUsingAccountId(int accountID)
    {
      List<Transaction> transactionList = _context.Transactions.Where(c => c.AccountID == accountID).ToList();
      return transactionList;
    }

    public Result Save(Transaction transaction)
    {
      Result result = new Result();
      try
      {
        if (transaction.TransactionID == 0)
        {
          _context.Transactions.Add(transaction);
        }
        else
        {
          Transaction transactionExisting = Retrieve(transaction.TransactionID);
          transactionExisting.AccountID = transaction.AccountID;
          transactionExisting.TransactionDate = transaction.TransactionDate;
          transactionExisting.Amount = transaction.Amount;
          transactionExisting.Description = transaction.Description;

          _context.Transactions.Update(transactionExisting);
        }

        transaction.TransactionDate = DateTime.Now;

        _context.SaveChangesAsync();
        result.IsSuccess = true;
      }
      catch (Exception exception)
      {
        result.Message = exception.Message;
      }
      return result;
    }

    public Result Delete(int transactionId)
    {
      Result result = new Result();
      try
      {
        Transaction transactionExisting = Retrieve(transactionId);

        if (transactionExisting != null)
        {
          _context.Transactions.Remove(transactionExisting);
          _context.SaveChangesAsync();
          result.IsSuccess = true;
        }
        else
        {
          result.Message = $"Failed to delete Transaction with TransactionId: {transactionId}, Reason, Transaction doesn't exist";
        }
      }
      catch (Exception exception)
      {
        result.Message = exception.Message;
      }
      return result;
    }
  }
}
