using FinancialApp.DataBase;
using FinancialApp.Enum;

namespace FinancialApp.GeneralMethods
{
    public class PrintHitory
    {
        private DB _db;
        private List<HistoryTransfer> _operationHistorySeach;

        public PrintHitory(DB db, List<HistoryTransfer> operationHistorySeach)
        {
            _db = db;
            _operationHistorySeach = operationHistorySeach;
        }

        public PrintHitory(DB db, Guid id, List<HistoryTransfer> operationHistorySeach)
        {
            _db = db;
            _operationHistorySeach = operationHistorySeach;
        }

        public void GetPrintHitory(DataGridView historyOperationDataGridView)
        {
            historyOperationDataGridView.Rows.Clear();

            foreach (var transferItem in _operationHistorySeach)
            {
                var personSender = _db.Persons.First(p => p.Id == transferItem.SenderId);
                var today = DateOnly.FromDateTime(transferItem.DateTime);
                var personRecipient = _db.Persons.FirstOrDefault(p => p.Id == transferItem.RecipientId);

                int index = historyOperationDataGridView.Rows.Add();

                historyOperationDataGridView.Rows[index].Cells[0].Value = today;
                historyOperationDataGridView.Rows[index].Cells[3].Value = transferItem.Type;
                historyOperationDataGridView.Rows[index].Cells[4].Value = transferItem.MoneyTransfer;

                if (transferItem.OperationType == TypeOfOperation.exchange)
                {
                    historyOperationDataGridView.Rows[index].Cells[1].Value = "обмен";
                    historyOperationDataGridView.Rows[index].Cells[2].Value = personSender.Name;
                }
                else if (transferItem.OperationType == TypeOfOperation.refill)
                {
                    historyOperationDataGridView.Rows[index].Cells[1].Value = "пополненеие";
                    historyOperationDataGridView.Rows[index].Cells[2].Value = personSender.Name;
                }
                else if (transferItem.OperationType == TypeOfOperation.moneyTransfer)
                {
                    historyOperationDataGridView.Rows[index].Cells[1].Value = "перевод";
                    historyOperationDataGridView.Rows[index].Cells[2].Value = personSender.Name;
                    historyOperationDataGridView.Rows[index].Cells[5].Value = personRecipient.Name;
                }
            }
        }

    }
}
