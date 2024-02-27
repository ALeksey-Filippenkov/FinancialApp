using FinancialApp.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialApp.DataBase.DbModels
{
    public class DbPersonMoney
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Person))]
        public Guid PersonId { get; set; }
        public DbPerson Person { get; set; }

        public CurrencyType Type { get; set; }


        public double Balance { get; set; }
    }
}
