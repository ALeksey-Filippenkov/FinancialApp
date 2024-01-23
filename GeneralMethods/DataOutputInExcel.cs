﻿using FinancialApp.DataBase;
using FinancialApp.Enum;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace FinancialApp.GeneralMethods
{
    public class DataOutputInExcel
    {
        private DB _db;
        private Guid _Id;

        public DataOutputInExcel(DB db, Guid Id)
        {
            _db = db;
            _Id = Id;
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
                var column = 1;
 
                worksheet.Cells[1, column].Value = HeadlinesTypes.Дата.ToString();
                worksheet.Cells[1, column + 1].Value = HeadlinesTypes.Отправитель.ToString();
                worksheet.Cells[1, column + 2].Value = HeadlinesTypes.Тип_операции.ToString();
                worksheet.Cells[1, column + 3].Value = HeadlinesTypes.Тип_валюты.ToString();
                worksheet.Cells[1, column + 4].Value = HeadlinesTypes.Деньги.ToString();
                worksheet.Cells[1, column + 5].Value = HeadlinesTypes.Получатель.ToString();

                foreach (var transferItem in operationHistory)
                {
                    var personSender = _db.Persons.First(p => p.Id == transferItem.SenderId);
                    var personRecipient = _db.Persons.FirstOrDefault(p => p.Id == transferItem.RecipientId);

                    worksheet.Cells[row, column].Value = transferItem.DateTime;
                    worksheet.Cells[row, column + 1].Value = personSender.Name;
                    worksheet.Cells[row, column + 3].Value = transferItem.Type.ToString();
                    worksheet.Cells[row, column + 4].Value = transferItem.MoneyTransfer;

                    if (transferItem.OperationType == TypeOfOperation.обмен)
                    {
                        worksheet.Cells[row, column + 2].Value = TypeOfOperation.обмен.ToString();
                    }
                    else if (transferItem.OperationType == TypeOfOperation.пополнение)
                    {
                        worksheet.Cells[row, column + 2].Value = TypeOfOperation.пополнение.ToString();
                    }
                    else if (transferItem.OperationType == TypeOfOperation.перевод)
                    {
                        worksheet.Cells[row, column + 2].Value = TypeOfOperation.перевод.ToString();
                        worksheet.Cells[row, column + 5].Value = personRecipient.Name;
                    }
                    row++;
                }

                for (int i = 1; i <= worksheet.UsedRange.Columns.Count; i++)
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

