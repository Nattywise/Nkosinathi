using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bank.Model
{
    public class Account
    {
        public int AccountID { get; set; }
        [Required]
        public int CustomerID { get; set; }
        [Required]
        public string AccountName { get; set; }

    }
}
