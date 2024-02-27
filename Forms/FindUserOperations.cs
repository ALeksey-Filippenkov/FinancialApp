using FinancialApp.DataBase;
using FinancialApp.DataBase.DbModels;
using FinancialApp.GeneralMethods;

namespace FinancialApp.Forms
{
    public partial class FindUserOperations : Form
    {
        private readonly DB _db;
        private List<DbHistoryTransfer> _personHistoryOperation;
        private DbPerson? _personRecipient;
        private List<DbHistoryTransfer> _operationPersonRecipient;
        private readonly FormData _formData;
        private readonly DbFinancial _context;

        public FindUserOperations(DB db, FormData formData, DbFinancial context)
        {
            InitializeComponent();
            monthCalendar1.MaxSelectionCount = 31;
            monthCalendar1.TodayDate = DateTime.Now;
            monthCalendar1.ShowToday = true;
            _db = db;
            _formData = formData;
            _context = context;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var startingDateSearch = monthCalendar1.SelectionRange.Start.Date;
            var endDateSearch = monthCalendar1.SelectionRange.End.Date;
            var currencyTypeValue = currencyTypeTextBox.Text.ToUpper();
            var personName = personNameTextBox.Text;

            if (personName != string.Empty)
            {
                _personRecipient = _context.Persons.FirstOrDefault(x => x.Name == personName);
                if (_personRecipient == null)
                {
                    historyOperationDataGridView.Rows.Clear();
                    MessageBox.Show($"Операций пользователя с именем {personName} не найдены");
                    return;
                }

                _operationPersonRecipient = CommonMethod.GetHistoryTransfer(_db, _personRecipient.Id,  _context);
            }
            else
            {
                _operationPersonRecipient = _context.HistoryTransfers.ToList();
            }

            _personHistoryOperation = EventSearch.GetEventSearch(_db, _operationPersonRecipient, startingDateSearch, endDateSearch, currencyTypeValue, personName, _context);

            if (_personHistoryOperation.Count != 0)
            {
                _formData.HistoryOperationDataGridView = historyOperationDataGridView;
                PrintHistory.GetPrintHistory(_db, _personHistoryOperation, _formData, _context);
            }
            else 
            {
                historyOperationDataGridView.Rows.Clear();
                MessageBox.Show($"{DateOnly.FromDateTime(startingDateSearch)} небыло произведено операций");
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PrintExcelButton_Click(object sender, EventArgs e)
        {
            var newExcel = new DataOutputInExcel(_db, _context);
            newExcel.GetDataOutputInExcel(_personHistoryOperation);
        }
    }
}
