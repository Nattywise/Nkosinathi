using Bank.Model;
using Bank.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Controllers
{
  [Route("api")]
  [ApiController]
  public class TransactionController : ControllerBase
  {
    private TransactionUtils transactionUtils;

    public TransactionController(BankContext bankContext)
    {
      transactionUtils = new TransactionUtils(bankContext);
    }

    [HttpGet("transactionhome")]
    public string TransactionHome()
    {
      return "Transaction Controller.";
    }

    [HttpGet("transactionlist")]
    public IEnumerable<Transaction> Get()
    {
      return transactionUtils.RetrieveAll();
    }

    [HttpGet("transaction")]
    public IActionResult  Get(int id)
    {
      Transaction transaction = transactionUtils.Retrieve(id);

      if (transaction != null)
      {
        return Ok(transaction);
      }

      return BadRequest($"No transaction found with TransactionId:{id}");
    }

    [HttpGet("transactionusingaccountid")]
    public IActionResult  GetTransactionUsingAccountId(int accountID)
    {
      List<Transaction> transactionList = transactionUtils.RetrieveUsingAccountId(accountID);

      if (transactionList != null)
      {
        return Ok(transactionList);
      }

      return BadRequest($"No transactions found for AccountID:{accountID}");
    }

    [HttpGet("transfersusingaccountid")]
    public IActionResult  GetTransfersUsingAccountId(int accountID)
    {
      List<Transaction> transactionList = transactionUtils.RetrieveUsingAccountId(accountID)
                                                          .Where(t => t.IsTransfer).ToList();

      if (transactionList != null)
      {
        return Ok(transactionList);
      }

      return BadRequest($"No transactions found for AccountID:{accountID}");
    }

    // POST api/<TransactionController>
    [HttpPost("transaction")]
    public IActionResult  Post([FromBody] Transaction transaction)
    {
      Result resultValidate = transactionUtils.Validate(transaction);
      if (resultValidate.IsSuccess)
      {
        Result resultTransactionSaved = transactionUtils.Save(transaction);
        if (resultTransactionSaved.IsSuccess)
        {
          return Ok(true);
        }
        else
        {
          return BadRequest(resultTransactionSaved.Message);
        }
      }
      else
      {
        return BadRequest(resultValidate.Message);
      }
    }

    // POST api/<TransactionController>
    [HttpPost("transactiontransfer")]
    public IActionResult  Post([FromBody] TransactionTransfer transactionTransfer)
    {
      Result resultValidate = transactionUtils.ValidateTransfer(transactionTransfer);
      if (resultValidate.IsSuccess)
      {
        Result resultTransactionTransferSaved = transactionUtils.Transfer(transactionTransfer);
        if (resultTransactionTransferSaved.IsSuccess)
        {
          return Ok(true);
        }
        else
        {
          return BadRequest(resultTransactionTransferSaved.Message);
        }
      }
      else
      {
        return BadRequest(resultValidate.Message);
      }
    }

    // PUT api/<TransactionController>/5
    [HttpPut("transaction")]
    public IActionResult  Put([FromBody] Transaction transaction)
    {
      Result resultValidate = transactionUtils.Validate(transaction);
      if (resultValidate.IsSuccess)
      {
        Result resultTransactionSaved = transactionUtils.Save(transaction);
        if (resultTransactionSaved.IsSuccess)
        {
          return Ok(true);
        }
        else
        {
          return BadRequest(resultTransactionSaved.Message);
        }
      }
      else
      {
        return BadRequest(resultValidate.Message);
      }
    }

    // DELETE api/<TransactionController>/5
    [HttpDelete("transaction")]
    public IActionResult  Delete(int id)
    {
      Result resultDelete = transactionUtils.Delete(id);
      if (resultDelete.IsSuccess)
      {
        return Ok(true);
      }
      else
      {
        return BadRequest(resultDelete.Message);
      }
    }
  }
}
