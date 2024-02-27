using FinancialApp.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialApp.DataBase.DbModels
{
    public class DbHistoryActionsWithUser
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Admin))]
        public Guid IdAdministrator { get; set; }
        public virtual DbAdmin Admin { get; set; }

        public AdministratorActionsWithUser TypeActionsWithUser { get; set; }

        [ForeignKey(nameof(Person))]
        public Guid IdPerson { get; set; }
        public virtual DbPerson Person { get; set; }

        public DateTime DateTime { get; set; }
    }
}
