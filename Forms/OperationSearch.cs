using FinancialApp.DataBase;
using FinancialApp.Enum;
using FinancialApp.GeneralMethods;

namespace FinancialApp
{
    public partial class OperationSearch : Form
    {
        private DB _db;
        private Guid _id;
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

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void seachButton_Click(object sender, EventArgs e)
        {
            var startingDateSeach = monthCalendar1.SelectionRange.Start.Date;
            var endDateSeach = monthCalendar1.SelectionRange.End.Date;
            var currencyTypeValue = currencyTypeTextBox.Text.ToUpper();
            var personRecipientName = personRecipientNameTextBox.Text;

            var operationSeach = _db.HistoryTransfers.Where(h => h.SenderId == _id || h.RecipientId == _id).ToList();

            if (operationSeach.Count == 0)
            {
                operationHistory.Text = "История операций";
                operationHistory.Text += $"\nУ Вас еще небыло операций";
                return;
            }
            else
            {
                _historyOperation = EventSearch.GetEventSearch(_db, operationSeach, startingDateSeach, endDateSeach, currencyTypeValue, personRecipientName);
            }

            if (_historyOperation != null)
            {
                operationHistory.Text = "История операций";
                PrintEvet(_historyOperation);
            }
            else
            {
                operationHistory.Text = "История операций";
                operationHistory.Text += $"\nОпераций с пользователем {personRecipientName} не найдены";
            }
        }

        private void PrintEvet(List<HistoryTransfer> historyOperation)
        {

            if (historyOperation.Count == 0)
            {
                operationHistory.Text += $"\nЕще небыло произведено операций";
            }
            else
            {
                foreach (var transferItem in historyOperation)
                {
                    var personSender = _db.Persons.First(p => p.Id == transferItem.SenderId);
                    var personRecipient = _db.Persons.FirstOrDefault(p => p.Id == transferItem.RecipientId);

                    switch (transferItem.OperationType)
                    {
                        case TypeOfOperation.refill:
                            operationHistory.Text += $"\n{personSender.Name} {transferItem.DateTime} произвел пополнение  {transferItem.Type} на {transferItem.MoneyTransfer}";
                            break;
                        case TypeOfOperation.money_transfer:
                            operationHistory.Text += $"\n{personSender.Name} {transferItem.DateTime} произвел перевод  {transferItem.MoneyTransfer} {transferItem.Type} {personRecipient.Name}";
                            break;
                        case TypeOfOperation.exchange:
                            operationHistory.Text += $"\n{personSender.Name} {transferItem.DateTime} произвел обмен  {transferItem.Type} на {transferItem.MoneyTransfer}";
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void historyOperationExel_Click(object sender, EventArgs e)
        {
            DataOutputInExcel newExel = new DataOutputInExcel(_db, _id);
            newExel.GetDataOutputInExcel(_historyOperation);
        }
    }
}
