using FinancialApp.Enum;

namespace FinancialApp.DataBase
{
    public class HistoryActionsWithUsers
    {
        public Guid IdAdministrator { get; set; }

        public Guid IdPerson { get; set; }

        public AdministratorActionsWithUser TypeActionsWithUser { get; set; }

        public DateTime DateTime { get; set; }
    }
}
