using FinancialApp.DataBase;
using FinancialApp.Enum;
using FinancialApp.GeneralMethods;

namespace FinancialApp.Forms
{
    public partial class AdministratorsPersonalAccount : Form
    {
        private readonly Form _form;
        private readonly DB _db;
        private readonly Guid _id;
        private AdministratorActionsWithUser _userActions;
        private readonly bool _isGeneralAdmin;
        private readonly FormData _formData;

        public AdministratorsPersonalAccount(bool isGeneralAdmin, Form form1, DB db, Guid id)
        {
            InitializeComponent();

            if (isGeneralAdmin)
            {
                workingWithAdministratorButton.Visible = true;
                _isGeneralAdmin = isGeneralAdmin;
                nameAdministration.Text = "Супер администратор";
            }
            else
            {
                _isGeneralAdmin = isGeneralAdmin;
                var nameAdmin = db.Admins.First(n => n.Id == id);
                nameAdministration.Text = string.Join(" ", nameAdmin.Name + nameAdmin.Surname);
            }
            _form = form1;
            _db = db;
            _id = id;
            dateTime.Text = DateOnly.FromDateTime(DateTime.Now).ToString();
            _formData = new FormData();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
    "Вы действительно хотите выйти?",
    "Подтверждение выхода",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Information,
    MessageBoxDefaultButton.Button1,
    MessageBoxOptions.DefaultDesktopOnly);
            if (result != DialogResult.Yes) return;
            _form.Show();
            Close();
        }

        private void WorkingWithUserButton_Click(object sender, EventArgs e)
        {
            showUserPersonalDataButton.Visible = true;
            banUserButton.Visible = true;
            deleteUserButton.Visible = true;
            findUserOperationsButton.Visible = true;
            restoreUserButton.Visible = true;
            label1.Visible = true;

            showAdministratorDataButton.Visible = false;
            addAdministratorButton.Visible = false;
            deleteAdministratorButton.Visible = false;
            searchAdministratorActionsButton.Visible = false;
        }

        private void ShowUserPersonalDataButton_Click(object sender, EventArgs e)
        {
            var showUserInformation = new ShowUserInformation(_db, _id);
            showUserInformation.Show();
        }

        private void BanUserButton_Click(object sender, EventArgs e)
        {
            VisibleButtonTrue();
            _userActions = AdministratorActionsWithUser.banUser;
        }

        private void DeleteUserButton_Click(object sender, EventArgs e)
        {
            VisibleButtonTrue();
            _userActions = AdministratorActionsWithUser.deleteUser;
        }

        private void FindUserOperationsButton_Click(object sender, EventArgs e)
        {
            var findUserOperations = new FindUserOperations(_db, _formData);
            findUserOperations.Show();
        }

        private void RestoreUserButton_Click(object sender, EventArgs e)
        {
            VisibleButtonTrue();
            _userActions = AdministratorActionsWithUser.unbanUser;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.Visible == true)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:

                        var user = _db.Persons.FirstOrDefault(u =>
                            u.Name.ToLower() == nameTextBox.Text.ToLower() && u.Surname.ToLower() == surnameTextBox.Text.ToLower());
                        if (user == null)
                        {
                            MessageBox.Show(
                                $"Пользователь с именем: {nameTextBox.Text} и фамилией {surnameTextBox.Text} не найдены");
                            return;
                        }

                        ActionsWithUsers.CreatingAdministratorFormUsers(_db, nameTextBox.Text, surnameTextBox.Text);
                        break;
                    case 1:
                        ActionsWithUsers.CreatingAdministrator(_db, nameTextBox.Text, surnameTextBox.Text);
                        break;
                }
            }
            else
            {
                var person = SearchPerson();
                UserActions(person);
            }
        }

        private Person SearchPerson()
        {
            var searchPerson = _db.Persons.FirstOrDefault(p => p.Name == nameTextBox.Text);
            return searchPerson;
        }

        private void UserActions(Person person)
        {
            if (person == null)
            {
                MessageBox.Show("Пользователя с таким именем нет");
                return;
            }

            ActionsWithUsers.UserActions(_db, person, _userActions, _id);

            VisibleButtonFalse();
        }

        private void VisibleButtonTrue()
        {
            nameTextBox.Visible = true;
            nameLabel.Visible = true;
            saveButton.Visible = true;
        }

        private void VisibleButtonFalse()
        {
            nameTextBox.Visible = false;
            nameLabel.Visible = false;
            saveButton.Visible = false;
        }

        private void WorkingWithAdministratorButton_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            showAdministratorDataButton.Visible = true;
            addAdministratorButton.Visible = true;
            deleteAdministratorButton.Visible = true;
            searchAdministratorActionsButton.Visible = true;

            showUserPersonalDataButton.Visible = false;
            banUserButton.Visible = false;
            deleteUserButton.Visible = false;
            findUserOperationsButton.Visible = false;
            restoreUserButton.Visible = false;
        }

        private void AddAdministratorButton_Click(object sender, EventArgs e)
        {
            comboBox1.Visible = true;
        }

        private void SearchAdministratorActionsButton_Click(object sender, EventArgs e)
        {
            var administratorActions = new HistoryAdministratorsActions();
            administratorActions.Show();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                nameLabel.Visible = true;
                nameTextBox.Visible = true;
                surnameLabel.Visible = true;
                surnameTextBox.Visible = true;
                saveButton.Visible = true;
            }
            else
            {
                nameLabel.Visible = false;
                nameTextBox.Visible = false;
                surnameLabel.Visible = false;
                surnameTextBox.Visible = false;
                saveButton.Visible = true;
            }
        }
    }
}
