using Microsoft.Office.Interop.Excel;

namespace FinancialApp.DataBase
{
    public class UserActionInformation
    {
        public string NameAdmin { get; set; }

        public string UserAction { get; set; }

        public string NameUser { get; set; }

        public DateTime DateEvent { get; set; }


        public UserActionInformation(string nameAdmin, string userAction, string nameUser, DateTime dateTime)
        {
            NameAdmin = nameAdmin;
            UserAction = userAction;
            NameUser = nameUser;
            DateEvent = dateTime;
        }
    }
}
