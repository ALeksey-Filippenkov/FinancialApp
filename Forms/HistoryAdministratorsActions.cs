using FinancialApp.DataBase;
using FinancialApp.DataBase.DbModels;
using FinancialApp.Enum;
using FinancialApp.GeneralMethods;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace FinancialApp.Forms
{
    public partial class HistoryAdministratorsActions : Form
    {
        private readonly DB _db;
        private List<UserActionInformation> _actionWithUsers;
        private readonly DbFinancial _context;

        public HistoryAdministratorsActions(DB db, DbFinancial context)
        {
            InitializeComponent();
            _db = db;
            _context = context;
            RefreshDataGridView();

        }

        private void RefreshDataGridView()
        {
            historyAdministratorsActionsDataGridView.DataSource = GetFilteredActionsWithUsers();

            historyAdministratorsActionsDataGridView.Columns["NameAdmin"].HeaderText = "Администратор";
            historyAdministratorsActionsDataGridView.Columns["NameAdmin"].Width = 300;
            historyAdministratorsActionsDataGridView.Columns["UserAction"].HeaderText = "Действие";
            historyAdministratorsActionsDataGridView.Columns["UserAction"].Width = 100;
            historyAdministratorsActionsDataGridView.Columns["NameUser"].HeaderText = "Пользователь";
            historyAdministratorsActionsDataGridView.Columns["NameUser"].Width = 300;
            historyAdministratorsActionsDataGridView.Columns["DateEvent"].HeaderText = "Дата";
            historyAdministratorsActionsDataGridView.Columns["DateEvent"].Width = 150;
        }

        private List<UserActionInformation> GetFilteredActionsWithUsers()
        {
            var searchString = nameTextBox.Text.ToLower();

            _actionWithUsers = new List<UserActionInformation>();

            foreach (var item in _context.HistoryActionsWithUsers)
            {
                var searchAdmin = _context.Admins.FirstOrDefault(n => n.Id == item.IdAdministrator);
                var searchUser = _context.Persons.First(n => n.Id == item.IdPerson);


                var nameAdmin = searchAdmin == null ? "Супер админ!" : string.Join(" ", searchAdmin.Name, searchAdmin.Surname);
                var nameUser = string.Join(" ", searchUser.Name, searchUser.Surname);

                _actionWithUsers.Add(new UserActionInformation(nameAdmin, ActionsWithUser.GetAdministratorActionsWithUser(item.TypeActionsWithUser), nameUser, item.DateTime));
            }

            if (searchString.Length != 0)
            {
                return _actionWithUsers.Where(p => p.NameAdmin.ToLower().Contains(searchString) && p.NameAdmin.Length > 0 ||
                                                  p.NameUser.ToLower().Contains(searchString) &&
                                                  p.NameUser.Length > 0 ||
                                                  p.UserAction.ToLower().Contains(searchString) && p.UserAction.Length > 0 ||
                                                  p.DateEvent.ToString().Contains(searchString)).ToList();
            }

            return _actionWithUsers;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void excelButton_Click(object sender, EventArgs e)
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

                var amountUsers = _actionWithUsers.Count;

                var headerText = PersonalData.GetUserData();

                for (int i = 0, rows = 1, cols = 2; i < headerText.Count; i++, cols++)
                {
                    worksheet.Cells[rows, cols].Value = headerText[i];
                }

                for (int userNumber = 0, rows = 2, cols = 1; rows < amountUsers + 2; userNumber++, rows++)
                {
                    var person = _actionWithUsers[userNumber];

                    worksheet.Cells[rows, cols].Value = userNumber + 1;
                    worksheet.Cells[rows, cols + 1].Value = person.NameAdmin;
                    worksheet.Cells[rows, cols + 2].Value = person.UserAction;
                    worksheet.Cells[rows, cols + 3].Value = person.NameUser;
                    worksheet.Cells[rows, cols + 4].Value = person.DateEvent;
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
    }
}
