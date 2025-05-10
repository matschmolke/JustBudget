using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustBudget.Models
{
    public class Transaction
    {
        public int Id { get; set; } //Primary Key

        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }

        public string TypeText => TransactionType.ToString();
    }
}
