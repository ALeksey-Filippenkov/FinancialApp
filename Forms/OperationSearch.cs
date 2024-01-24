using FinancialApp.DataBase;
using FinancialApp.Enum;
using FinancialApp.GeneralMethods;
using System.Data;

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
            var startingDateSeach = monthCalendar1.SelectionRange.Start;
            var endDateSeach = monthCalendar1.SelectionRange.End;

            var currencyTypeValue = currencyTypeTextBox.Text.ToUpper();
            var personRecipientName = personRecipientNameTextBox.Text;
           
            _historyOperation = EventSearch.GetEventSearch(_db, _id, startingDateSeach, endDateSeach, currencyTypeValue, personRecipientName);

            PrintEvet(_historyOperation);
        }

        private void PrintEvet(List<HistoryTransfer> historyOperation)
        {
            operationHistory.Text = "История операций";

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
                        case TypeOfOperation.пополнение:
                            operationHistory.Text += $"\n{personSender.Name} {transferItem.DateTime} произвел {TypeOfOperation.пополнение.ToString()}  {transferItem.Type} на {transferItem.MoneyTransfer}";
                            break;
                        case TypeOfOperation.перевод:
                            operationHistory.Text += $"\n{personSender.Name} {transferItem.DateTime} произвел {TypeOfOperation.перевод.ToString()}  {transferItem.MoneyTransfer} {transferItem.Type} {personRecipient.Name}";
                            break;
                        case TypeOfOperation.обмен:
                            operationHistory.Text += $"\n{personSender.Name} {transferItem.DateTime} произвел {TypeOfOperation.обмен.ToString()}  {transferItem.Type} на {transferItem.MoneyTransfer}";
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
