using FinancialApp.DataBase;
using FinancialApp.Enum;
using FinancialApp.GeneralMethods;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace FinancialApp.Forms
{
    public partial class ShowUserInformation : Form
    {
        private readonly DB _db;
        private readonly Guid _id;
        private List<Person> _user;
        private readonly List<string> _personData;
        private int _index;
        private bool cancelContextMenu = false;
        private readonly FormData _formData;

        public ShowUserInformation(DB db, Guid id)
        {
            _db = db;
            _id = id;
            _personData = PersonalData.GetUserData();
            InitializeComponent();
            RefreshDataGridView();
            _formData = new FormData();
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _index = userStatusComboBox.SelectedIndex;
            RefreshDataGridView();
        }

        private void ShowUserInformation_Load(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private List<Person> GetFilteredPersons()
        {
            var searchString = nameTextBox.Text.ToLower();

            var searchInfo = _db.Persons.Where(p => p.Name.ToLower().Contains(searchString) && p.Name.Length > 0 ||
                                                    p.Surname.ToLower().Contains(searchString) &&
                                                    p.Surname.Length > 0 ||
                                                    p.City.ToLower().Contains(searchString) && p.City.Length > 0 ||
                                                    p.Adress.ToLower().Contains(searchString) && p.Adress.Length > 0 ||
                                                    p.Age.ToString().Contains(searchString)).ToList();

            if (searchString.Length != 0 && _index == -1 || _index == 0)
            {
                _user = searchInfo;
                return _user;
            }

            if (searchString.Length != 0 && _index == 1)
            {
                _user = searchInfo.Where(i => i.IsBanned == true).ToList();
                return _user;
            }

            if (searchString.Length != 0 && _index == 2)
            {
                _user = searchInfo.Where(i => i.IsBanned == false).ToList();
                return _user;
            }

            if (searchString.Length == 0 && _index == -1 || _index == 0)
            {
                _user = _db.Persons;
                return _user;
            }

            if (searchString.Length == 0 && _index == 1)
            {
                _user = _db.Persons.Where(s => s.IsBanned == true).ToList();
                return _user;
            }

            _user = _db.Persons.Where(s => s.IsBanned == false).ToList();
            return _user;
        }

        private void RefreshDataGridView()
        {
            userInformationDataGridView.DataSource = null;
            userInformationDataGridView.DataSource = GetFilteredPersons();

            userInformationDataGridView.Columns["Name"].HeaderText = "Имя";
            userInformationDataGridView.Columns["Name"].Width = 100;
            userInformationDataGridView.Columns["Surname"].HeaderText = "Фамилия";
            userInformationDataGridView.Columns["Surname"].Width = 200;
            userInformationDataGridView.Columns["Age"].HeaderText = "Возраст";
            userInformationDataGridView.Columns["Age"].Width = 100;
            userInformationDataGridView.Columns["City"].HeaderText = "Город";
            userInformationDataGridView.Columns["City"].Width = 100;
            userInformationDataGridView.Columns["Adress"].HeaderText = "Адрес";
            userInformationDataGridView.Columns["Adress"].Width = 200;
            userInformationDataGridView.Columns["PhoneNumber"].HeaderText = "Номер телефона";
            userInformationDataGridView.Columns["PhoneNumber"].Width = 100;
            userInformationDataGridView.Columns["EmailAdress"].HeaderText = "Электронная почта";
            userInformationDataGridView.Columns["EmailAdress"].Width = 200;
            userInformationDataGridView.Columns["Login"].HeaderText = "Логин";
            userInformationDataGridView.Columns["Login"].Width = 100;
            userInformationDataGridView.Columns["Login"].ReadOnly = true;
            userInformationDataGridView.Columns["Password"].HeaderText = "Пароль";
            userInformationDataGridView.Columns["Password"].Width = 100;
            userInformationDataGridView.Columns["Password"].ReadOnly = true;
            userInformationDataGridView.Columns["Id"].Width = 300;
            userInformationDataGridView.Columns["Id"].ReadOnly = true;
            userInformationDataGridView.Columns["IsBanned"].HeaderText = "Статус бана";
            userInformationDataGridView.Columns["IsBanned"].Width = 100;
            userInformationDataGridView.Columns["IsBanned"].ReadOnly = true;
        }

        private void DataGridViewBooks_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = e.RowIndex;
            var col = e.ColumnIndex;

            var cell = userInformationDataGridView[col, row];

            var cellValue = cell.Value;

            var person = _user[row];
            switch (col)
            {
                case 0:
                    person.Name = (string)cellValue;
                    break;
                case 1:
                    person.Surname = (string)cellValue;
                    break;
                case 2:
                    person.Age = (int)cellValue;
                    break;
                case 3:
                    person.City = (string)cellValue;
                    break;
                case 4:
                    person.Adress = (string)cellValue;
                    break;
                case 5:
                    person.PhoneNumber = (int)cellValue;
                    break;
                case 6:
                    person.EmailAdress = (string)cellValue;
                    break;
                default:
                    MessageBox.Show("Поля нельзя редактировать");
                    break;
            }

            _db.SaveDB();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PrintExcelButton_Click(object sender, EventArgs e)
        {
            PrintExcel();
        }

        private void PrintExcel()
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

                var amountUsers = _user.Count;

                for (int i = 0, rows = 1, cols = 2; i < _personData.Count; i++, cols++)
                {
                    worksheet.Cells[rows, cols].Value = _personData[i];
                }

                for (int userNumber = 0, rows = 2, cols = 1; rows < amountUsers + 2; userNumber++, rows++)
                {
                    var person = _user[userNumber];

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
                    worksheet.Cells[rows, cols + 10].Value = string.Join("-", person.Id);

                    worksheet.Cells[rows, cols + 11].Value = person.IsBanned ? "Да" : "Нет";
                }

                for (var i = 1; i <= worksheet.UsedRange.Columns.Count; i++)
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

        private void UserInformationDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var hitTestInfo = userInformationDataGridView.HitTest(e.X, e.Y);
            if (hitTestInfo.RowIndex >= 0 && hitTestInfo.ColumnIndex >= 0)
            {
                userInformationDataGridView.ClearSelection();
                userInformationDataGridView.Rows[hitTestInfo.RowIndex].Selected = true;
                cancelContextMenu = false;
            }
            else
            {
                cancelContextMenu = true;
            }
        }

        private void ContextMenuStripForGrid_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cancelContextMenu)
            {
                e.Cancel = true;
            }
        }

        private void BanUser_Click(object sender, EventArgs e)
        {
            var selectedRows = userInformationDataGridView.SelectedRows;
            foreach (DataGridViewRow selectedRow in selectedRows)
            {
                var rowIndex = selectedRow.Index;

                if (rowIndex < 0)
                {
                    continue;
                }

                var person = _user[rowIndex];
                var dlgConfirm =
                    MessageBox.Show(
                        "Забанить пользователя?\r\n\r\nИмя: " + person.Name + "\r\n Фамилия: " + person.Surname,
                        "Подтвердите", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dlgConfirm == DialogResult.Yes)
                {
                    ActionsWithUsers.UserActions(_db, person, AdministratorActionsWithUser.banUser, _id);
                }
            }
        }

        private void RestoreUser_Click(object sender, EventArgs e)
        {
            var selectedRows = userInformationDataGridView.SelectedRows;
            foreach (DataGridViewRow selectedRow in selectedRows)
            {
                var rowIndex = selectedRow.Index;

                if (rowIndex < 0)
                {
                    continue;
                }

                var person = _user[rowIndex];
                var dlgConfirm =
                    MessageBox.Show(
                        "Разбанить пользователя?\r\n\r\nИмя: " + person.Name + "\r\n Фамилия: " + person.Surname,
                        "Подтвердите", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dlgConfirm == DialogResult.Yes)
                {
                    ActionsWithUsers.UserActions(_db, person, AdministratorActionsWithUser.unbanUser, _id);
                }
            }
        }

        private void MakeAdministrator_Click(object sender, EventArgs e)
        {
            var selectedRows = userInformationDataGridView.SelectedRows;
            foreach (DataGridViewRow selectedRow in selectedRows)
            {
                var rowIndex = selectedRow.Index;

                if (rowIndex < 0)
                {
                    continue;
                }

                var person = _user[rowIndex];
                var dlgConfirm =
                    MessageBox.Show(
                        "Доавить пользователю права адмнистратора?\r\n\r\nИмя: " + person.Name + "\r\n Фамилия: " +
                        person.Surname, "Подтвердите", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dlgConfirm == DialogResult.Yes)
                {
                    var name = person.Name;
                    var surName = person.Surname;
                    ActionsWithUsers.CreatingAdministrator(_db, name, surName);
                }
            }
        }

        private void ShowHistoryOperations_Click(object sender, EventArgs e)
        {
            var selectedRows = userInformationDataGridView.SelectedRows;
            foreach (DataGridViewRow selectedRow in selectedRows)
            {
                var rowIndex = selectedRow.Index;

                if (rowIndex < 0)
                {
                    continue;
                }

                var person = _user[rowIndex];
                var userTransactionHistory = new UserTransactionHistory(_db, _formData, person);
                userTransactionHistory.Show();
            }
        }
    }
}
