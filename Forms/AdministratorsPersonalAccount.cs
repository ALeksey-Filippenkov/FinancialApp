using FinancialApp.DataBase;
using FinancialApp.Enum;

namespace FinancialApp.Forms
{
    public partial class AdministratorsPersonalAccount : Form
    {
        private Form _form;
        private Guid _id;
        private DB _db;
        private AdministratorActionsWithUser _userActions;
        private bool _trueOrFalse;

        public AdministratorsPersonalAccount(Guid Id, Form form1, DB db)
        {
            InitializeComponent();
            _id = Id;
            _form = form1;
            _db = db;
            dateTime.Text = DateOnly.FromDateTime(DateTime.Now).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
    "Вы действительно хотите выйти?",
    "Подтверждение выхода",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Information,
    MessageBoxDefaultButton.Button1,
    MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                _form.Show();
                Close();
            }
        }

        private void workingWithUserButton_Click(object sender, EventArgs e)
        {
            showUserPersonalDataButton.Visible = true;
            banUserButton.Visible = true;
            deleteUserButton.Visible = true;
            findUserOperationsButton.Visible = true;
            restoreUserButton.Visible = true;
            label1.Visible = true;
        }

        private void showUserPersonalDataButton_Click(object sender, EventArgs e)
        {
            var showUserInformation = new ShowUserInformation(_db);
            showUserInformation.Show();
        }

        private void banUserButton_Click(object sender, EventArgs e)
        {
            VisibleButtoTrue();
            _userActions = AdministratorActionsWithUser.забанить;
        }

        private void deleteUserButton_Click(object sender, EventArgs e)
        {
            VisibleButtoTrue();
            var person = SeachPerson();
            _userActions = AdministratorActionsWithUser.удалить;
        }

        private void findUserOperationsButton_Click(object sender, EventArgs e)
        {
            var findUserOperations = new FindUserOperations(_db);
            findUserOperations.Show();
        }

        private void restoreUserButton_Click(object sender, EventArgs e)
        {
            VisibleButtoTrue();
            _userActions = AdministratorActionsWithUser.разбанить;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var person = SeachPerson();
            UserActions(person, _userActions);
        }

        private Person SeachPerson()
        {
            var sechPerson = _db.Persons.FirstOrDefault(p => p.Name == textBox1.Text);
            return sechPerson;
        }

        private void UserActions(Person person, AdministratorActionsWithUser userActions) 
        {
            if (person == null)
            {
                MessageBox.Show("Пользователя с таким именем нет");
                return;
            }
            else if (userActions == AdministratorActionsWithUser.разбанить)
            {
                person.IsBanned = false;
                _db.SaveDB();
                MessageBox.Show("Пользователь успешно разбанен");
                VisibleButtoFalse();
            }
            else if (userActions == AdministratorActionsWithUser.забанить)
            {
                person.IsBanned = true;
                _db.SaveDB();
                MessageBox.Show("Пользователь успешно забанен");
                VisibleButtoFalse();
            }
            else if (userActions == AdministratorActionsWithUser.удалить)
            {
                _db.Persons.Remove(person);
                _db.SaveDB();
                MessageBox.Show("Пользователь успешно удален");
                VisibleButtoFalse();
            }
        }

        private void VisibleButtoTrue()
        {
            textBox1.Visible = true;
            label2.Visible = true;
            saveButton.Visible = true;
        }

        private void VisibleButtoFalse() 
        {
            textBox1.Visible = false;
            label2.Visible = false;
            saveButton.Visible = false;
        }

    }
}
