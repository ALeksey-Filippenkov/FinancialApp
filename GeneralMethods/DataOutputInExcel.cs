using FinancialApp.DataBase;
using FinancialApp.Enum;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace FinancialApp.GeneralMethods
{
    public class DataOutputInExcel
    {
        private readonly DB _db;

        public DataOutputInExcel(DB db)
        {
            _db = db;
        }

        public void GetDataOutputInExcel(List<HistoryTransfer> operationHistory)
        {
            Excel.Application application = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                application = new Excel.Application
                {
                    Visible = true
                };

                workbook = application.Workbooks.Add();
                worksheet = workbook.Worksheets.Item[1];

                var row = 2;
                const int column = 1;
 
                worksheet.Cells[1, column].Value = Headlines.GetHeadlinesTypes(HeadlinesTypes.Date);
                worksheet.Cells[1, column + 1].Value = Headlines.GetHeadlinesTypes(HeadlinesTypes.Sender);
                worksheet.Cells[1, column + 2].Value = Headlines.GetHeadlinesTypes(HeadlinesTypes.TypeOfOperation);
                worksheet.Cells[1, column + 3].Value = Headlines.GetHeadlinesTypes(HeadlinesTypes.CurrencyType);
                worksheet.Cells[1, column + 4].Value = Headlines.GetHeadlinesTypes(HeadlinesTypes.Money);
                worksheet.Cells[1, column + 5].Value = Headlines.GetHeadlinesTypes(HeadlinesTypes.Recipient);

                foreach (var transferItem in operationHistory)
                {
                    var personSender = _db.Persons.First(p => p.Id == transferItem.SenderId);
                    var personRecipient = _db.Persons.FirstOrDefault(p => p.Id == transferItem.RecipientId);

                    worksheet.Cells[row, column].Value = transferItem.DateTime;
                    worksheet.Cells[row, column + 1].Value = personSender.Name;
                    worksheet.Cells[row, column + 3].Value = transferItem.Type.ToString();
                    worksheet.Cells[row, column + 4].Value = transferItem.MoneyTransfer;

                    switch (transferItem.OperationType)
                    {
                        case TypeOfOperation.exchange:
                            worksheet.Cells[row, column + 2].Value = TypeOperation.GetTypeOfOperation(TypeOfOperation.exchange);
                            break;
                        case TypeOfOperation.refill:
                            worksheet.Cells[row, column + 2].Value = TypeOperation.GetTypeOfOperation(TypeOfOperation.refill);
                            break;
                        case TypeOfOperation.moneyTransfer:
                            worksheet.Cells[row, column + 2].Value = TypeOperation.GetTypeOfOperation(TypeOfOperation.moneyTransfer);
                            worksheet.Cells[row, column + 5].Value = personRecipient.Name;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    row++;
                }

                for (var i = 1; i <= worksheet.UsedRange.Columns.Count; i++)
                {
                    worksheet.Columns[i].AutoFit();
                }
                worksheet.Protect();
            }

            finally
            {
                Marshal.ReleaseComObject(worksheet);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(application);
            }
        }
    }
}

