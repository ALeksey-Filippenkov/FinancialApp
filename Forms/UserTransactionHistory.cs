using FinancialApp.DataBase;
using FinancialApp.DataBase.DbModels;
using FinancialApp.GeneralMethods;

namespace FinancialApp.Forms
{
    public partial class UserTransactionHistory : Form
    {

        private readonly DB _db;
        private FormData _formData;
        private DbPerson _user;
        private readonly DbFinancial _context;

        public UserTransactionHistory(DB db, FormData formData, DbPerson user, DbFinancial context)
        {
            InitializeComponent();
            _db = db;
            _formData = formData;
            _user = user;
            _context = context;
            SearchPerson();
        }

        private void SearchPerson()
        {
            var userOperations = CommonMethod.GetHistoryTransfer(_db, _user.Id, _context);
            _formData.HistoryOperationDataGridView = historyOperationDataGridView;
            PrintHistory.GetPrintHistory(_db, userOperations, _formData, _context);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
