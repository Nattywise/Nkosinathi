using Bank.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Utils
{
  public class CustomerUtils
  {
    private BankContext _context;
//pass the database context to the current class
    public CustomerUtils(BankContext context)
    {
      _context = context;

    }

 /// <summary>
 /// retrieve the list of customers
 /// </summary>
 /// <returns></returns>
    public IEnumerable<Customer> RetrieveAll()
    {
      return _context.Customers.ToList();
    }
//perform basic validation on customer record
    public Result Validate(Customer customer)
    {
      Result result = new Result();
      if (string.IsNullOrEmpty(customer.Name))
      {
        result.Message = "Customer Name can't be blank.";
      }
      else
      {
        result.IsSuccess = true;
      }

      return result;
    }
        /// <summary>
        /// retrieve the customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
    public Customer Retrieve(int id)
    {
      Customer customer = _context.Customers.Where(c => c.CustomerID == id).FirstOrDefault();
      return customer;
    }
        /// <summary>
        /// add new or update customer details and save changes to the model 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
    public Result Save(Customer customer)
    {
      Result result = new Result();
      try
      {
        bool canSave = true;
        if (customer.CustomerID == 0)
        {
          _context.Customers.Add(customer);
        }
        else
        {
          Customer customerExisting = Retrieve(customer.CustomerID);
          if (customerExisting != null)
          {
            customerExisting.Name = customer.Name;
            _context.Customers.Update(customerExisting);
          }
          else
          {
            result.Message = $"Customer can't be updated. Reason: Customer with CustomerID:{customer.CustomerID} doesn't exist.";
            canSave = false;
          }
        }

        if (canSave)
        {
          _context.SaveChangesAsync();
          result.IsSuccess = true;
        }
      }
      catch (Exception exception)
      {
        result.Message = exception.Message;
      }
      return result;
    }
        /// <summary>
        /// delete the customer using customer id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
    public Result Delete(int customerId)
    {
      Result result = new Result();
      try
      {
        Customer customerExisting = Retrieve(customerId);

        if (customerExisting != null)
        {

          AccountUtils accountUtils = new AccountUtils(_context);
          int accountsCount = accountUtils.RetrieveUsingCustomerId(customerId).Count();
          if (accountsCount == 0)
          {
            _context.Customers.Remove(customerExisting);
            _context.SaveChangesAsync();
            result.IsSuccess = true;
          }
          else
          {
            result.Message = $"Failed to delete customer with customerId: {customerId}, Reason, Customer has active accounts.";
          }
        }
        else
        {
          result.Message = $"Failed to delete customer with customerId: {customerId}, Reason, Customer doesn't exist";
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
