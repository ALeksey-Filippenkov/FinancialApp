using FinancialApp.DataBase;
using FinancialApp.Enum;
using FinancialApp.GeneralMethods;

namespace FinancialApp.Forms
{
    public partial class OperationSearch : Form
    {
        private readonly DB _db;
        private readonly Guid _id;
        private List<HistoryTransfer> _historyOperation;

        public OperationSearch(DB db, Guid id)
        {
            InitializeComponent();
            monthCalendar1.MaxSelectionCount = 31;
            monthCalendar1.TodayDate = DateTime.Now;
            monthCalendar1.ShowToday = true;
            _db = db;
            _id = id;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            var startingDateSearch = monthCalendar1.SelectionRange.Start.Date;
            var endDateSearch = monthCalendar1.SelectionRange.End.Date;
            var currencyTypeValue = currencyTypeTextBox.Text.ToUpper();
            var personRecipientName = personRecipientNameTextBox.Text;

            var operationSearch = _db.HistoryTransfers.Where(h => h.SenderId == _id || h.RecipientId == _id).ToList();

            if (operationSearch.Count == 0)
            {
                operationHistory.Text = "История операций";
                operationHistory.Text += $"\nУ Вас еще небыло операций!";
                return;
            }
            else
            {
                _historyOperation = EventSearch.GetEventSearch(_db, operationSearch, startingDateSearch, endDateSearch, currencyTypeValue, personRecipientName);
            }

            if (_historyOperation == null)
            {
                operationHistory.Text = "История операций";
                operationHistory.Text += $"\nОпераций с пользователем {personRecipientName} не найдены!";
                return;
            }
            else
            {
                if (_historyOperation.Count == 0)
                {
                    operationHistory.Text = "История операций";
                    operationHistory.Text += $"\n{DateOnly.FromDateTime(startingDateSearch)} небыло произведено операций!";
                    return;
                }

                if (_historyOperation == null) return;
                operationHistory.Text = "История операций";
                PrintEvent(_historyOperation);
            }
        }

        private void PrintEvent(List<HistoryTransfer> historyOperation)
        {

            if (historyOperation.Count == 0)
            {
                operationHistory.Text += $"\nЕще небыло произведено операций";
                return;
            }
            else
            {
                foreach (var transferItem in historyOperation)
                {
                    var personSender = _db.Persons.First(p => p.Id == transferItem.SenderId);
                    var personRecipient = _db.Persons.FirstOrDefault(p => p.Id == transferItem.RecipientId);

                    operationHistory.Text += transferItem.OperationType switch
                    {
                        TypeOfOperation.refill => $"\n{personSender.Name} {transferItem.DateTime} произвел пополнение  {transferItem.Type} на {transferItem.MoneyTransfer}",
                        TypeOfOperation.moneyTransfer => $"\n{personSender.Name} {transferItem.DateTime} произвел перевод  {transferItem.MoneyTransfer} {transferItem.Type} {personRecipient.Name}",
                        TypeOfOperation.exchange => $"\n{personSender.Name} {transferItem.DateTime} произвел обмен  {transferItem.Type} на {transferItem.MoneyTransfer}",
                        _ => throw new ArgumentOutOfRangeException(),
                    };
                }
            }
        }

        private void HistoryOperationExcel_Click(object sender, EventArgs e)
        {
            var newExcel = new DataOutputInExcel(_db);
            newExcel.GetDataOutputInExcel(_historyOperation);
        }
    }
}
