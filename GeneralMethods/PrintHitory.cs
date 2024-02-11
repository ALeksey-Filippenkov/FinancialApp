using FinancialApp.DataBase;
using FinancialApp.Enum;

namespace FinancialApp.GeneralMethods
{
    public static class PrintHistory
    {
        public static void GetPrintHistory(DB db, List<HistoryTransfer> operationHistorySearch,
            FormData dataGrid)
        {
            var historyOperationDataGridView = dataGrid.HistoryOperationDataGridView;

            historyOperationDataGridView.Rows.Clear();

            foreach (var transferItem in operationHistorySearch)
            {
                var personSender = db.Persons.First(p => p.Id == transferItem.SenderId);
                var today = DateOnly.FromDateTime(transferItem.DateTime);
                var personRecipient = db.Persons.FirstOrDefault(p => p.Id == transferItem.RecipientId);

                var index = historyOperationDataGridView.Rows.Add();

                historyOperationDataGridView.Rows[index].Cells[0].Value = today;
                historyOperationDataGridView.Rows[index].Cells[3].Value = transferItem.Type;
                historyOperationDataGridView.Rows[index].Cells[4].Value = transferItem.MoneyTransfer;

                switch (transferItem.OperationType)
                {
                    case TypeOfOperation.exchange:
                        historyOperationDataGridView.Rows[index].Cells[1].Value = "обмен";
                        historyOperationDataGridView.Rows[index].Cells[2].Value = personSender.Name;
                        break;
                    case TypeOfOperation.refill:
                        historyOperationDataGridView.Rows[index].Cells[1].Value = "пополненеие";
                        historyOperationDataGridView.Rows[index].Cells[2].Value = personSender.Name;
                        break;
                    case TypeOfOperation.moneyTransfer:
                        historyOperationDataGridView.Rows[index].Cells[1].Value = "перевод";
                        historyOperationDataGridView.Rows[index].Cells[2].Value = personSender.Name;
                        historyOperationDataGridView.Rows[index].Cells[5].Value = personRecipient.Name;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

    }
}
