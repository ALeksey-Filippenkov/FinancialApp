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
            monthCalendar1.MaxSelectionCount = 31;
            monthCalendar1.TodayDate = DateTime.Now;
            monthCalendar1.ShowToday = true;

            var startingDateSeach = DateOnly.FromDateTime(monthCalendar1.SelectionRange.Start);
            var endDateSeach = DateOnly.FromDateTime(monthCalendar1.SelectionRange.End);

            var currencyTypeValue = currencyTypeTextBox.Text.ToUpper();
            var personRecipientName = personRecipientTextBox.Text;

            var historyOperation = EventSearch(startingDateSeach, currencyTypeValue, personRecipientName);

            operationHistory.Text = "История операций";

            if (historyOperation.Count == 0)
            {
                operationHistory.Text += $"\n{startingDateSeach} небыло переводов с таким типом валюты или получателем";
            }
            else
            {
                foreach (var transferItem in historyOperation)
                {
                    var personSender = _db.Persons.First(p => p.Id == transferItem.SenderId);
                    var personRecipient = _db.Persons.FirstOrDefault(p => p.Id == transferItem.RecipientId);

                    operationHistory.Text += $"\n{personSender.Name} {startingDateSeach}";
                    if (personRecipient == null)
                        operationHistory.Text += $" пополнил счет {transferItem.Type} на {transferItem.MoneyTransfer}";

                    else
                        operationHistory.Text += $" перевел {transferItem.MoneyTransfer} {transferItem.Type} {personRecipient.Name}";
                }
            }

            List<HistoryTransfer> EventSearch(DateOnly startingDateSeach, string currencyTypeValue, string personRecipientName)
            {
                var personRecipientNameSeach = _db.Persons.FirstOrDefault(p => p.Name == personRecipientName);

                if (startingDateSeach == endDateSeach)
                {
                    if (currencyTypeValue != string.Empty && personRecipientName != string.Empty)
                    {
                        var historyOperationDay = _db.HistoryTransfers.Where(s => DateOnly.FromDateTime(s.DateTime) == startingDateSeach
                                                                                               && s.Type.ToString() == currencyTypeValue
                                                                                                   && s.RecipientId == personRecipientNameSeach.Id).ToList();
                        return historyOperationDay;
                    }
                    else if (currencyTypeValue != string.Empty && personRecipientName == string.Empty)
                    {
                        var historyOperationDay = _db.HistoryTransfers.Where(s => DateOnly.FromDateTime(s.DateTime) == startingDateSeach
                                                                                               && s.Type.ToString() == currencyTypeValue).ToList();
                        return historyOperationDay;
                    }
                    else if (currencyTypeValue == string.Empty && personRecipientName != string.Empty)
                    {
                        var historyOperationDay = _db.HistoryTransfers.Where(s => DateOnly.FromDateTime(s.DateTime) == startingDateSeach
                                                                                                   && s.RecipientId == personRecipientNameSeach.Id).ToList();
                        return historyOperationDay;
                    }
                    else
                    {
                        var historyOperationDay = _db.HistoryTransfers.Where(s => DateOnly.FromDateTime(s.DateTime) == startingDateSeach).ToList();
                        return historyOperationDay;
                    }
                }
                else
                {
                    if (currencyTypeValue != string.Empty && personRecipientName != string.Empty)
                    {
                        var item = _db.HistoryTransfers.Where(s => s.Type.ToString() == currencyTypeValue && s.RecipientId == personRecipientNameSeach.Id).ToList();

                        var historyOperationDay = _db.HistoryTransfers.Where(s => DateOnly.FromDateTime(s.DateTime) >= startingDateSeach
                                                                                && DateOnly.FromDateTime(s.DateTime) <= endDateSeach
                                                                                                && s.Type.ToString() == currencyTypeValue
                                                                                                    && s.RecipientId == personRecipientNameSeach.Id).ToList();
                        return historyOperationDay;
                    }
                    else if (currencyTypeValue != string.Empty && personRecipientName == string.Empty)
                    {

                        var historyOperationDay = _db.HistoryTransfers.Where(s => DateOnly.FromDateTime(s.DateTime) >= startingDateSeach
                                                                               && DateOnly.FromDateTime(s.DateTime) <= endDateSeach
                                                                                               && s.Type.ToString() == currencyTypeValue).ToList();
                        return historyOperationDay;
                    }
                    else if (currencyTypeValue == string.Empty && personRecipientName != string.Empty)
                    {
                        var historyOperationDay = _db.HistoryTransfers.Where(s => DateOnly.FromDateTime(s.DateTime) >= startingDateSeach
                                            && DateOnly.FromDateTime(s.DateTime) <= endDateSeach
                                            && s.RecipientId == personRecipientNameSeach.Id).ToList();
                        return historyOperationDay;
                    }
                    else
                    {
                          var historyOperationDay = _db.HistoryTransfers.Where(s => DateOnly.FromDateTime(s.DateTime) >= startingDateSeach
                                                                                 && DateOnly.FromDateTime(s.DateTime) <= endDateSeach).ToList();
                        return historyOperationDay;

                    } 
                }
            }
        }
    }
}
