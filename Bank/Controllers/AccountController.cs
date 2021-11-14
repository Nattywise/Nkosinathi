using Bank.Model;
using Bank.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bank.Controllers
{
  [Route("api")]
  [ApiController]
  public class AccountController : ControllerBase
  {
    private AccountUtils accountUtils;

    public AccountController(BankContext bankContext)
    {
      accountUtils = new AccountUtils(bankContext);
    }

    [HttpGet("accounthome")]
    public string AccountHome()
    {
      return "Account Controller.";
    }

    [HttpGet("accountlist")]
    public IEnumerable<Account> Get()
    {
      return accountUtils.RetrieveAll();
    }

    [HttpGet("account")]
    public IActionResult  Get(int id)
    {
      Account account = accountUtils.Retrieve(id);

      if (account != null)
      {
        return Ok(account);
      }

      return BadRequest($"No account found with AccountId:{id}");
    }

    [HttpGet("accountbalance")]
    public IActionResult  GetAccountBalance(int id)
    {
      decimal accountBalance = accountUtils.RetrieveBalance(id);
      return Ok(accountBalance);
    }

    // POST api/<AccountController>
    [HttpPost("account")]
    public IActionResult  Post(decimal initialDeposit, [FromBody] Account account)
    {
      Result resultValidate = accountUtils.Validate(account, initialDeposit);
      if (resultValidate.IsSuccess)
      {
        Result resultAccountSaved = accountUtils.Save(account, initialDeposit);
        if (resultAccountSaved.IsSuccess)
        {
          return Ok(true);
        }
        else
        {
          return BadRequest(resultAccountSaved.Message);
        }
      }
      else
      {
        return BadRequest(resultValidate.Message);
      }
    }

    // PUT api/<AccountController>/5
    [HttpPut("account")]
    public IActionResult  Put([FromBody] Account account)
    {
      Result resultValidate = accountUtils.Validate(account);
      if (resultValidate.IsSuccess)
      {
        Result resultAccountSaved = accountUtils.Save(account);
        if (resultAccountSaved.IsSuccess)
        {
          return Ok(true);
        }
        else
        {
          return BadRequest(resultAccountSaved.Message);
        }
      }
      else
      {
        return BadRequest(resultValidate.Message);
      }
    }

    // DELETE api/<AccountController>/5
    [HttpDelete("account")]
    public IActionResult  Delete(int id)
    {
      Result resultDelete = accountUtils.Delete(id);
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
