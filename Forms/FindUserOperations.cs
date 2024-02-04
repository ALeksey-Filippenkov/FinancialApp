using FinancialApp.DataBase;
using FinancialApp.GeneralMethods;

namespace FinancialApp.Forms
{
    public partial class FindUserOperations : Form
    {
        private DB _db;
        private List<HistoryTransfer> _personHistoryOperation;
        private Person _personRecipient;
        private List<HistoryTransfer> _operationPersonRecipient;

        public FindUserOperations(DB db)
        {
            InitializeComponent();
            monthCalendar1.MaxSelectionCount = 31;
            monthCalendar1.TodayDate = DateTime.Now;
            monthCalendar1.ShowToday = true;
            _db = db;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var startingDateSeach = monthCalendar1.SelectionRange.Start.Date;
            var endDateSeach = monthCalendar1.SelectionRange.End.Date;
            var currencyTypeValue = currencyTypeTextBox.Text.ToUpper();
            var personName = personNameTextBox.Text;

            if (personName != string.Empty)
            {
                _personRecipient = _db.Persons.FirstOrDefault(x => x.Name == personName);
                if (_personRecipient == null)
                {
                    historyOperationDataGridView.Rows.Clear();
                    MessageBox.Show($"Операций пользователя с именем {personName} не найдены");
                    return;
                }
                else
                {
                    _operationPersonRecipient = _db.HistoryTransfers.Where(h => h.SenderId == _personRecipient.Id || h.RecipientId == _personRecipient.Id).ToList();
                }
            }
            else
            {
                _operationPersonRecipient = _db.HistoryTransfers;
            }

            _personHistoryOperation = EventSearch.GetEventSearch(_db, _operationPersonRecipient, startingDateSeach, endDateSeach, currencyTypeValue, personName);

            if (_personHistoryOperation.Count != 0)
            {
                PrintHitory printHistory = new PrintHitory(_db, _personHistoryOperation);
                printHistory.GetPrintHitory(historyOperationDataGridView);
            }
            else 
            {
                historyOperationDataGridView.Rows.Clear();
                MessageBox.Show($"{DateOnly.FromDateTime(startingDateSeach)} небыло произведено операций");
                return;
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pritnExcelButton_Click(object sender, EventArgs e)
        {
            DataOutputInExcel newExel = new DataOutputInExcel(_db, _personRecipient.Id);
            newExel.GetDataOutputInExcel(_personHistoryOperation);
        }
    }
}
