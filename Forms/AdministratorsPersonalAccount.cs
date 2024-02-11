using FinancialApp.DataBase;
using FinancialApp.Enum;
using System.Text;

namespace FinancialApp.Forms
{
    public partial class AdministratorsPersonalAccount : Form
    {
        private readonly Form _form;
        private readonly DB _db;
        private readonly Guid _id;
        private AdministratorActionsWithUser _userActions;
        private readonly bool _isGeneralAdmin;
        private FormData _formData;

        public AdministratorsPersonalAccount(bool isGeneralAdmin, Form form1, DB db, Guid id)
        {
            InitializeComponent();

            if (isGeneralAdmin)
            {
                workingWithAdministratorButton.Visible = true;
                _isGeneralAdmin = isGeneralAdmin;
            }
            else
            {
                _isGeneralAdmin = isGeneralAdmin;
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
            var showUserInformation = new ShowUserInformation(_db);
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
            var person = SearchPerson();
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
                        break;
                    case 1:
                        СreateAdministrator();
                        break;
                }
            }
            else
            {
                var person = SearchPerson();
                UserActions(person, _userActions);
            }
        }

        private Person SearchPerson()
        {
            var searchPerson = _db.Persons.FirstOrDefault(p => p.Name == textBox1.Text);
            return searchPerson;
        }

        private void UserActions(Person person, AdministratorActionsWithUser userActions)
        {
            if (person == null)
            {
                MessageBox.Show("Пользователя с таким именем нет");
                return;
            }

            switch (userActions)
            {
                case AdministratorActionsWithUser.unbanUser:
                    person.IsBanned = false;
                    SaveHistoryActionWithUser(person.Id, userActions, _id);
                    MessageBox.Show("Пользователь успешно разбанен");
                    _db.SaveDB();
                    VisibleButtonFalse();
                    break;
                case AdministratorActionsWithUser.banUser:
                    person.IsBanned = true;
                    SaveHistoryActionWithUser(person.Id, userActions, _id);
                    MessageBox.Show("Пользователь успешно забанен");
                    _db.SaveDB();
                    VisibleButtonFalse();
                    break;
                case AdministratorActionsWithUser.deleteUser:
                    _db.Persons.Remove(person);
                    SaveHistoryActionWithUser(person.Id, userActions, _id);
                    MessageBox.Show("Пользователь успешно удален");
                    _db.SaveDB();
                    VisibleButtonFalse();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(userActions), userActions, null);
            }
        }

        private void VisibleButtonTrue()
        {
            textBox1.Visible = true;
            label2.Visible = true;
            saveButton.Visible = true;
        }

        private void VisibleButtonFalse()
        {
            textBox1.Visible = false;
            label2.Visible = false;
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
            saveButton.Visible = true;
        }

        public void СreateAdministrator()
        {
            var admin = new Admin
            {
                Login = CreatingLogin(),
                Password = СreatePassword(),
                Id = Guid.NewGuid()
            };

            _db.Admins.Add(admin);

            MessageBox.Show("Поздравляем! Вы успешно прошли регистрацию");
            Thread.Sleep(50);

            _db.SaveDB();

        }

        public string CreatingLogin()
        {
            const int nameLen = 6;
            var vowels = "aeuoyi".ToCharArray();
            var consonants = "qwrtpsdfghjklzxcvbnm".ToCharArray();

            var rand = new Random();

            var newNick = new StringBuilder();

            while (newNick.Length < nameLen)
            {
                var firstVowel = rand.Next(0, 2) == 0;

                if (firstVowel)
                {
                    newNick.Append(vowels[rand.Next(0, vowels.Length)]);
                    newNick.Append(consonants[rand.Next(0, consonants.Length)]);
                }
                else
                {
                    newNick.Append(consonants[rand.Next(0, consonants.Length)]);
                    newNick.Append(vowels[rand.Next(0, vowels.Length)]);
                }
            }

            newNick[0] = char.ToUpper(newNick[0]);
            return newNick.ToString();
        }

        public string СreatePassword()
        {
            var length = 8;
            const string valid = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var res = new StringBuilder();
            var rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public void SaveHistoryActionWithUser(Guid id, AdministratorActionsWithUser userActions, Guid _id)
        {
            var historyActionWithUser = new HistoryActionsWithUser
            {
                IdAdministrator = _id,
                IdPerson = id,
                TypeActionsWithUser = userActions,
                DateTime = DateTime.Now
            };

            _db.HistoryActionsWithUsers.Add(historyActionWithUser);
        }
    }
}
