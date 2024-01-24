using FinancialApp.DataBase;
using FinancialApp.GeneralMethods;
using System.Security.Cryptography;

namespace FinancialApp.Forms
{
    public partial class FindUserOperations : Form
    {
        private DB _db;
        private Person _personId;
        private List<HistoryTransfer> _personHistoryOperation;

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
            var startingDateSeach = monthCalendar1.SelectionRange.Start;
            var endDateSeach = monthCalendar1.SelectionRange.End;
            var currencyTypeValue = currencyTypeTextBox.Text.ToUpper();
            var personName = personNameTextBox.Text;

            _personId = _db.Persons.FirstOrDefault(x => x.Name == personName);

            if (_personId == null)
            {
                MessageBox.Show("Имя пользователя обязательно для заполнения");
                return;
            }

            _personHistoryOperation = EventSearch.GetEventSearch(_db, _personId.Id, startingDateSeach, endDateSeach, currencyTypeValue, personName);
            PrintHitory printHistory = new PrintHitory(_db, _personHistoryOperation);
            printHistory.GetPrintHitory(historyOperationDataGridView);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pritnExcelButton_Click(object sender, EventArgs e)
        {
            DataOutputInExcel newExel = new DataOutputInExcel(_db, _personId.Id);
            newExel.GetDataOutputInExcel(_personHistoryOperation);
        }
    }
}
