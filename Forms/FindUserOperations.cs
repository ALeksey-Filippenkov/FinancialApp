using FinancialApp.DataBase;
using FinancialApp.GeneralMethods;

namespace FinancialApp.Forms
{
    public partial class FindUserOperations : Form
    {
        private DB _db;

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
            var startingDateSeach = DateOnly.FromDateTime(monthCalendar1.SelectionRange.Start);
            var endDateSeach = DateOnly.FromDateTime(monthCalendar1.SelectionRange.End);
            var currencyTypeValue = currencyTypeTextBox.Text.ToUpper();
            var personName = personNameTextBox.Text;

            var seachPerson = _db.Persons.FirstOrDefault(x => x.Name == personName);

            if (seachPerson == null)
            {
                var operationSeach = _db.HistoryTransfers;
            }

            var person = EventSearch.GetEventSearch(_db, seachPerson.Id, startingDateSeach, endDateSeach, currencyTypeValue, personName);
            PrintHitory printHistory = new PrintHitory(_db, person);
            printHistory.GetPrintHitory(historyOperationDataGridView);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
