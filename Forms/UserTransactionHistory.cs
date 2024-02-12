using FinancialApp.DataBase;
using FinancialApp.GeneralMethods;

namespace FinancialApp.Forms
{
    public partial class UserTransactionHistory : Form
    {

        private readonly DB _db;
        private FormData _formData;
        private Person _user;

        public UserTransactionHistory(DB db, FormData formData, Person user)
        {
            InitializeComponent();
            _db = db;
            _formData = formData;
            _user = user;
            SearchPerson();
        }

        private void SearchPerson()
        {
            var userOperations = CommonMethod.GetHistoryTransfer(_db, _user.Id);
            _formData.HistoryOperationDataGridView = historyOperationDataGridView;
            PrintHistory.GetPrintHistory(_db, userOperations, _formData);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
