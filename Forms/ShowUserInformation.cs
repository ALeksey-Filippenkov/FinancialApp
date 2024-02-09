using FinancialApp.DataBase;
using FinancialApp.GeneralMethods;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace FinancialApp.Forms
{
    public partial class ShowUserInformation : Form
    {
        private DB _db;
        private List<Person> _user;
        private List<string> _personData;
        private Person _changedUserData;

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

            try
            {
                application = new Excel.Application
                {
                    Visible = true
                };

                workbook = application.Workbooks.Add();
                worksheet = workbook.Worksheets.Item[1];

                worksheet.Cells[1, 1].Value = "# Пользователя";

                var amountUsers = _db.Persons.Count;

                for (int i = 0, rows = 1, cols = 2; i < _personData.Count; i++, cols++)
                {
                    worksheet.Cells[rows, cols].Value = _personData[i];
                }

                for (int userNumber = 0, rows = 2, cols = 1; rows < amountUsers + 2; userNumber++, rows++)
                {
                    var person = _db.Persons[userNumber];

                    worksheet.Cells[rows, cols].Value = userNumber + 1;
                    worksheet.Cells[rows, cols + 1].Value = person.Name;
                    worksheet.Cells[rows, cols + 2].Value = person.Surname;
                    worksheet.Cells[rows, cols + 3].Value = person.Age;
                    worksheet.Cells[rows, cols + 4].Value = person.City;
                    worksheet.Cells[rows, cols + 5].Value = person.Adress;
                    worksheet.Cells[rows, cols + 6].Value = person.PhoneNumber;
                    worksheet.Cells[rows, cols + 7].Value = person.EmailAdress;
                    worksheet.Cells[rows, cols + 8].Value = person.Login;
                    worksheet.Cells[rows, cols + 9].Value = person.Password;
                    worksheet.Cells[rows, cols + 10].Value = String.Join("-", person.Id);
                    if (person.IsBanned)
                    {
                        worksheet.Cells[rows, cols + 11].Value = "Да";
                    }
                    else
                        worksheet.Cells[rows, cols + 11].Value = "Нет";
                }

                for (int i = 1; i <= worksheet.UsedRange.Columns.Count; i++)
                {
                    worksheet.Columns[i].AutoFit();
                    worksheet.Columns[i].HorizontalAlignment = Excel.Constants.xlCenter;                    
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
