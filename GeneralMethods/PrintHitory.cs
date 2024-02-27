using FinancialApp.DataBase;
using FinancialApp.DataBase.DbModels;
using FinancialApp.Enum;

namespace FinancialApp.GeneralMethods
{
    public static class PrintHistory
    {
        public static void GetPrintHistory(DB db, List<DbHistoryTransfer> operationHistorySearch,
            FormData dataGrid, DbFinancial context)
        {
            var historyOperationDataGridView = dataGrid.HistoryOperationDataGridView;

            historyOperationDataGridView.Rows.Clear();

            foreach (var transferItem in operationHistorySearch)
            {
                var personSender = context.Persons.First(p => p.Id == transferItem.SenderId);
                var today = DateOnly.FromDateTime(transferItem.DateTime);
                var personRecipient = context.Persons.FirstOrDefault(p => p.Id == transferItem.RecipientId);

                var index = historyOperationDataGridView.Rows.Add();

                historyOperationDataGridView.Rows[index].Cells[0].Value = today;
                historyOperationDataGridView.Rows[index].Cells[3].Value = transferItem.Type;
                historyOperationDataGridView.Rows[index].Cells[4].Value = transferItem.MoneyTransfer;

                switch (transferItem.OperationType)
                {
                    case TypeOfOperation.exchange:
                        historyOperationDataGridView.Rows[index].Cells[1].Value = TypeOperation.GetTypeOfOperation(TypeOfOperation.exchange);
                        historyOperationDataGridView.Rows[index].Cells[2].Value = personSender.Name;
                        break;
                    case TypeOfOperation.refill:
                        historyOperationDataGridView.Rows[index].Cells[1].Value = TypeOperation.GetTypeOfOperation(TypeOfOperation.refill);
                        historyOperationDataGridView.Rows[index].Cells[2].Value = personSender.Name;
                        break;
                    case TypeOfOperation.moneyTransfer:
                        historyOperationDataGridView.Rows[index].Cells[1].Value = TypeOperation.GetTypeOfOperation(TypeOfOperation.moneyTransfer);
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
