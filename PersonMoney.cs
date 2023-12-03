using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialApp
{
    public class PersonMoney
    {
        public Guid PersonId { get; set; }

        public Guid MoneyId { get; set; }

        public string Type { get; set; }

        public double Balance { get; set; }
    }
}
