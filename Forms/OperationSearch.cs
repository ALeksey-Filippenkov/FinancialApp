using FinancialApp.DataBase;
using FinancialApp.Enum;
using System.Data;
using System.Drawing.Text;

namespace FinancialApp
{
    public partial class OperationSearch : Form
    {
        private DB _db;
        private Guid _Id;

        public OperationSearch(DB db, Guid Id)
        {
            InitializeComponent();
            _db = db;
            _Id = Id;
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

            var operationSeach = _db.HistoryTransfers.Where(h => h.SenderId == _Id || h.RecipientId == _Id).ToList();

            var historyOperation = EventSearch(operationSeach, startingDateSeach, currencyTypeValue, personRecipientName);

            PrintEvet(historyOperation);


            List<HistoryTransfer> EventSearch(List<HistoryTransfer> operationSeach, DateOnly startingDateSeach, string currencyTypeValue, string personRecipientName)
            {
                var personRecipientNameSeach = _db.Persons.FirstOrDefault(p => p.Name == personRecipientName);

                if (startingDateSeach == endDateSeach)
                {
                    if (currencyTypeValue != string.Empty && personRecipientName != string.Empty)
                    {
                        var historyOperationDay = operationSeach.Where(s => DateOnly.FromDateTime(s.DateTime) == startingDateSeach
                                                                                         && s.Type.ToString() == currencyTypeValue
                                                                                             && s.RecipientId == personRecipientNameSeach.Id).ToList();
                        return historyOperationDay;
                    }
                    else if (currencyTypeValue != string.Empty && personRecipientName == string.Empty)
                    {
                        var historyOperationDay = operationSeach.Where(s => DateOnly.FromDateTime(s.DateTime) == startingDateSeach
                                                                                         && s.Type.ToString() == currencyTypeValue).ToList();
                        return historyOperationDay;
                    }
                    else if (currencyTypeValue == string.Empty && personRecipientName != string.Empty)
                    {
                        var historyOperationDay = operationSeach.Where(s => DateOnly.FromDateTime(s.DateTime) == startingDateSeach
                                                                                             && s.RecipientId == personRecipientNameSeach.Id).ToList();
                        return historyOperationDay;
                    }
                    else
                    {
                        var historyOperationDay = operationSeach.Where(s => DateOnly.FromDateTime(s.DateTime) == startingDateSeach).ToList();
                        return historyOperationDay;
                    }
                }
                else
                {
                    if (currencyTypeValue != string.Empty && personRecipientName != string.Empty)
                    {
                        var historyOperationDay = operationSeach.Where(s => DateOnly.FromDateTime(s.DateTime) >= startingDateSeach
                                                                         && DateOnly.FromDateTime(s.DateTime) <= endDateSeach
                                                                                         && s.Type.ToString() == currencyTypeValue
                                                                                             && s.RecipientId == personRecipientNameSeach.Id).ToList();
                        return historyOperationDay;
                    }
                    else if (currencyTypeValue != string.Empty && personRecipientName == string.Empty)
                    {

                        var historyOperationDay = operationSeach.Where(s => DateOnly.FromDateTime(s.DateTime) >= startingDateSeach
                                                                         && DateOnly.FromDateTime(s.DateTime) <= endDateSeach
                                                                                         && s.Type.ToString() == currencyTypeValue).ToList();
                        return historyOperationDay;
                    }
                    else if (currencyTypeValue == string.Empty && personRecipientName != string.Empty)
                    {
                        var historyOperationDay = operationSeach.Where(s => DateOnly.FromDateTime(s.DateTime) >= startingDateSeach
                                                                         && DateOnly.FromDateTime(s.DateTime) <= endDateSeach
                                                                                             && s.RecipientId == personRecipientNameSeach.Id).ToList();
                        return historyOperationDay;
                    }
                    else
                    {
                        var historyOperationDay = operationSeach.Where(s => DateOnly.FromDateTime(s.DateTime) >= startingDateSeach
                                                                         && DateOnly.FromDateTime(s.DateTime) <= endDateSeach).ToList();
                        return historyOperationDay;
                    }
                }
            }

            void PrintEvet(List<HistoryTransfer> historyOperation)
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

                        if (transferItem.SenderId == transferItem.RecipientId)
                        {
                            operationHistory.Text += $"\n{personSender.Name} {transferItem.DateTime} произвел {TypeOfOperation.Обмен.ToString()}  {transferItem.Type} на {transferItem.MoneyTransfer}";
                        }
                        else if (personRecipient == null)
                        {
                            operationHistory.Text += $"\n{personSender.Name} {transferItem.DateTime} произвел {TypeOfOperation.Пополнение.ToString()}  {transferItem.Type} на {transferItem.MoneyTransfer}";
                        }
                        else if (transferItem.RecipientId == _Id)
                        {
                            operationHistory.Text += $"\n{personSender.Name} {transferItem.DateTime} произвел {TypeOfOperation.Перевод.ToString()}  {transferItem.MoneyTransfer} {transferItem.Type} {personRecipient.Name}";

                        }
                        else if (transferItem.SenderId == _Id && personRecipient != null)
                        {
                            operationHistory.Text += $"\n{personSender.Name} {transferItem.DateTime} произвел {TypeOfOperation.Перевод.ToString()}  {transferItem.MoneyTransfer} {transferItem.Type} {personRecipient.Name}"; 
                        }



                        //    if (personSender.Id == _Id)
                        //{
                        //    operationHistory.Text += $"\n{personSender.Name} {transferItem.DateTime}";
                        //    if (personRecipient == null)
                        //        operationHistory.Text += $" пополнил счет {transferItem.Type} на {transferItem.MoneyTransfer}";

                        //    else
                        //        operationHistory.Text += $" перевел {transferItem.MoneyTransfer} {transferItem.Type} {personRecipient.Name}";
                        //}
                    }
                }
            }
        }
    }
}
