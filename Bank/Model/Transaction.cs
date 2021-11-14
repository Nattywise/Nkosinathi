using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Model
{
  public class Transaction
  {
    public int TransactionID { get; set; }
    public int AccountID { get; set; }

    public DateTime TransactionDate { get; set; } = new DateTime();
    public Decimal Amount { get; set; }
    public string Description  { get; set; }

    public bool IsTransfer { get; set; }

  }


  public class TransactionTransfer
  {
    public int AccountIDFrom { get; set; }
    public int AccountIDTo { get; set; }
    public Decimal Amount { get; set; }

  }
}
