using FinancialApp.Enum;

namespace FinancialApp.DataBase
{
    public class HistoryTransfer
    {
        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }
        public CurrencyType Type { get; set; }
        public double MoneyTransfer { get; set; }
        public DateTime DateTime { get; set; }
        public TypeOfOperation OperationType { get; set; }
    }
}
