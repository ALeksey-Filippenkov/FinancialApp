using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialApp
{
    public class HistoryTransfer
    {
        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }
        public string Type { get; set; }
        public double MoneyTransfer {  get; set; }
        public DateTime DateTime {  get; set; } 
    }
}
