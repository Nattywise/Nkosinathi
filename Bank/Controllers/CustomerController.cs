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
  public class CustomerController : ControllerBase
  {
    private CustomerUtils customerUtils;

    public CustomerController(BankContext bankContext)
    {
      customerUtils = new CustomerUtils(bankContext);
    }

    [HttpGet("customerhome")]
    public string CustomerHome()
    {
      return "Customer Controller.";
    }

    [HttpGet("customerlist")]
    public IEnumerable<Customer> Get()
    {
      return customerUtils.RetrieveAll();
    }

    [HttpGet("customer")]
    public IActionResult  Get(int id)
    {
      Customer customer = customerUtils.Retrieve(id);

      if (customer != null)
      {
        return Ok(customer);
      }

      return BadRequest($"No customer found with CustomerId:{id}");
    }

    // POST api/<CustomerController>
    [HttpPost("customer")]
    public IActionResult Post([FromBody] Customer customer)
    {
      Result resultValidate = customerUtils.Validate(customer);
      if (resultValidate.IsSuccess)
      {
        Result resultCustomerSaved = customerUtils.Save(customer);
        if (resultCustomerSaved.IsSuccess)
        {
          return Ok(true);
        }
        else
        {
          return BadRequest(resultCustomerSaved.Message);
        }
      }
      else
      {
        return BadRequest(resultValidate.Message);
      }
    }

    // PUT api/<CustomerController>/5
    [HttpPut("customer")]
    public IActionResult Put([FromBody] Customer customer)
    {
      Result resultValidate = customerUtils.Validate(customer);
      if (resultValidate.IsSuccess)
      {
        Result resultCustomerSaved = customerUtils.Save(customer);
        if (resultCustomerSaved.IsSuccess)
        {
          return Ok(true);
        }
        else
        {
          return BadRequest(resultCustomerSaved.Message);
        }
      }
      else
      {
        return BadRequest(resultValidate.Message);
      }
    }

    // DELETE api/<CustomerController>/5
    [HttpDelete("customer")]
    public IActionResult  Delete(int id)
    {
      Result resultDelete = customerUtils.Delete(id);
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
