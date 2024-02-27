using FinancialApp.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialApp.DataBase.DbModels
{
    /// <summary>
    /// История финансовых операций пользователя
    /// </summary>
    public class DbHistoryTransfer
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(SenderId))]
        public Guid SenderId { get; set; }
        //[ForeignKey("SenderId")]
        public virtual DbPerson Sender { get; set; }

        [ForeignKey(nameof(RecipientId))]
        public Guid RecipientId { get; set; }
        //[ForeignKey("RecipientId")]
        public virtual DbPerson Recipient { get; set; }

        public CurrencyType Type { get; set; }

        public double MoneyTransfer { get; set; }

        public DateTime DateTime { get; set; }

        public TypeOfOperation OperationType { get; set; }
    }
}
