using FinancialApp.Enum;

namespace FinancialApp.DataBase
{
    public class HistoryActionsWithUser
    {
        public Guid IdAdministrator { get; set; }

        public AdministratorActionsWithUser TypeActionsWithUser { get; set; }

        public Guid IdPerson { get; set; }

        public DateTime DateTime { get; set; }
    }
}
