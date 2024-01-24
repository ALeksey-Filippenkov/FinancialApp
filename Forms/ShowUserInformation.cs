using FinancialApp.DataBase;
using FinancialApp.GeneralMethods;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing;
using OfficeOpenXml.Style;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace FinancialApp.Forms
{
    public partial class ShowUserInformation : Form
    {
        private DB _db;
        private List<Person> _user;
        private List<string> _personData;

        public ShowUserInformation(DB db)
        {
            InitializeComponent();
            _db = db;
            _personData = PersonalData.GetUserData();
        }

        private void seachButton_Click(object sender, EventArgs e)
        {
            userInformationDataGridView.Visible = true;

            var person = _db.Persons.Where(p => p.Name == nameTextBox.Text).ToList();
            if (person == null)
            {
                MessageBox.Show("Пользователь с таким именем не найден");
                return;
            }
            else
                printExcelButton.Visible = true;

            userInformationDataGridView.Rows.Clear();

            if (person.Count == 0)
            {
                _user = _db.Persons;
                PrintUsers(_personData, _user, userInformationDataGridView);
            }
            else
            {
                _user = person;
                PrintUsers(_personData, _user, userInformationDataGridView);
            }
        }
        public void PrintUsers(List<string> personData, List<Person> users, DataGridView userInformationDataGridView)
        {
            var index = personData.Count;
            var rows = users.Count * index;

            userInformationDataGridView.Rows.Add(rows);

            int r = 0;

            for (int i = 0, j = 0; i < users.Count; i++)
            {
                userInformationDataGridView.Rows[j].Cells[0].Value = $"Пользователь #{i + 1}";

                for (int k = 0; k < index; k++)
                {
                    userInformationDataGridView.Rows[r].Cells[1].Value = personData[k];
                    r++;
                }
                j += index;
            }

            int l = 0;

            foreach (var item in users)
            {
                userInformationDataGridView.Rows[0 + l].Cells[2].Value = item.Name;
                userInformationDataGridView.Rows[1 + l].Cells[2].Value = item.Surname;
                userInformationDataGridView.Rows[2 + l].Cells[2].Value = item.Age;
                userInformationDataGridView.Rows[3 + l].Cells[2].Value = item.City;
                userInformationDataGridView.Rows[4 + l].Cells[2].Value = item.Adress;
                userInformationDataGridView.Rows[5 + l].Cells[2].Value = item.PhoneNumber;
                userInformationDataGridView.Rows[6 + l].Cells[2].Value = item.EmailAdress;
                userInformationDataGridView.Rows[7 + l].Cells[2].Value = item.Login;
                userInformationDataGridView.Rows[8 + l].Cells[2].Value = item.Password;
                userInformationDataGridView.Rows[9 + l].Cells[2].Value = item.Id;
                userInformationDataGridView.Rows[10 + l].Cells[2].Value = item.IsBanned.ToString();
                l += index;
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void printExcelButton_Click(object sender, EventArgs e)
        {
            PrintExcel(_user);
        }

        private void PrintExcel(List<Person> users) 
        {
            Excel.Application application = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            var index = _personData.Count;

            try
            {
                application = new Excel.Application
                {
                    Visible = true
                };

                workbook = application.Workbooks.Add();
                worksheet = workbook.Worksheets.Item[1];

                worksheet.Cells[1, 1].Value = "# Пользователя";
                worksheet.Cells[1, 2].Value = "Данные";
                worksheet.Cells[1, 3].Value = "Значение";

                int rowsPersonData = 2;
                for (int i = 0, j = 2; i < users.Count; i++)
                {
                    worksheet.Cells[j, 1] = $"Пользователь #{i + 1}";

                    for (int k = 0; k < index; k++)
                    {
                        worksheet.Cells[rowsPersonData, 2].Value = _personData[k];
                        rowsPersonData++;
                    }
                    j += index;
                }

                int rowsUser = 2, columnUser = 3;
                foreach (var item in users)
                {
                    worksheet.Cells[rowsUser, columnUser].Value = item.Name;
                    worksheet.Cells[rowsUser+1, columnUser].Value = item.Surname;
                    worksheet.Cells[rowsUser+2, columnUser].Value = item.Age;
                    worksheet.Cells[rowsUser+3, columnUser].Value = item.City;
                    worksheet.Cells[rowsUser+4, columnUser].Value = item.Adress;
                    worksheet.Cells[rowsUser+5, columnUser].Value = item.PhoneNumber;
                    worksheet.Cells[rowsUser+6, columnUser].Value = item.EmailAdress;
                    worksheet.Cells[rowsUser+7, columnUser].Value = item.Login;
                    worksheet.Cells[rowsUser+8, columnUser].Value = item.Password;
                    worksheet.Cells[rowsUser+9, columnUser].Value = String.Join("-", item.Id);
                    if (item.IsBanned)
                    {
                        worksheet.Cells[rowsUser + 10, columnUser].Value = "Да";
                    }
                    else
                        worksheet.Cells[rowsUser+10, columnUser].Value = "Нет";

                    rowsUser += index;                   
                }

                for (int i = 1; i <= worksheet.UsedRange.Columns.Count; i++)
                {
                    worksheet.Columns[i].AutoFit();
                    worksheet.Columns[i].HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
