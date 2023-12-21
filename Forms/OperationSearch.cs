using FinancialApp.DataBase;
using System.Data;

namespace FinancialApp
{
    public partial class OperationSearch : Form
    {
        private DB _db;

        public OperationSearch(DB db)
        {
            InitializeComponent();
            _db = db;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void seachButton_Click(object sender, EventArgs e)
        {
            var seachDay = DateOnly.FromDateTime(dateTimePicker1.Value);
            var historyOperationDya = _db.HistoryTransfers.Where(h => DateOnly.FromDateTime(h.DateTime) == seachDay).ToList();

            operationHistory.Text = "История переводов";

            if (historyOperationDya.Count == 0)
            {
                operationHistory.Text += $"\n{seachDay} небыло переводов";
            }
            else
            {
                foreach (var transferItem in historyOperationDya)
                {
                    var personSender = _db.Persons.First(p => p.Id == transferItem.SenderId);
                    var personRecipient = _db.Persons.FirstOrDefault(p => p.Id == transferItem.RecipientId);

                    operationHistory.Text += $"\n{personSender.Name} {seachDay}";
                    if (personRecipient == null)
                        operationHistory.Text += $" пополнил счет {transferItem.Type} на {transferItem.MoneyTransfer}";

                    else
                        operationHistory.Text += $" перевел {transferItem.MoneyTransfer} {transferItem.Type} {personRecipient.Name}";
                }
            }
        }
    }
}
