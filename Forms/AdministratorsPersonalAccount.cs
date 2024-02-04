using FinancialApp.DataBase;
using FinancialApp.Enum;
using System.Text;

namespace FinancialApp.Forms
{
    public partial class AdministratorsPersonalAccount : Form
    {
        private Form _form;
        private DB _db;
        private Guid _id;
        private AdministratorActionsWithUser _userActions;
        private bool _isGeneralAdmin;

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
            _userActions = AdministratorActionsWithUser.banUser;
        }

        private void deleteUserButton_Click(object sender, EventArgs e)
        {
            VisibleButtoTrue();
            var person = SeachPerson();
            _userActions = AdministratorActionsWithUser.deleteUser;
        }

        private void findUserOperationsButton_Click(object sender, EventArgs e)
        {
            var findUserOperations = new FindUserOperations(_db);
            findUserOperations.Show();
        }

        private void restoreUserButton_Click(object sender, EventArgs e)
        {
            VisibleButtoTrue();
            _userActions = AdministratorActionsWithUser.unbanUser;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.Visible == true)
            {
                if (comboBox1.SelectedIndex == 0)
                {

                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    СreatingAdministrator();
                }
            }
            else
            {
                var person = SeachPerson();
                UserActions(person, _userActions);
            }
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
            else if (userActions == AdministratorActionsWithUser.unbanUser)
            {
                person.IsBanned = false;
                SaveHistoryActionWhithUser(person.Id, userActions, _id);
                MessageBox.Show("Пользователь успешно разбанен");
                _db.SaveDB();
                VisibleButtoFalse();
            }
            else if (userActions == AdministratorActionsWithUser.banUser)
            {
                person.IsBanned = true;
                SaveHistoryActionWhithUser(person.Id, userActions, _id);
                MessageBox.Show("Пользователь успешно забанен");
                _db.SaveDB();
                VisibleButtoFalse();
            }
            else if (userActions == AdministratorActionsWithUser.deleteUser)
            {
                _db.Persons.Remove(person);
                SaveHistoryActionWhithUser(person.Id, userActions, _id);
                MessageBox.Show("Пользователь успешно удален");
                _db.SaveDB();
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

        private void workingWithAdministratorButton_Click(object sender, EventArgs e)
        {
            showAdministratorDataButton.Visible = true;
            addAdministratorButrron.Visible = true;
            deleteAdministratorButton.Visible = true;
            searchAdministratorActionsButton.Visible = true;
        }

        private void addAdministratorButrron_Click(object sender, EventArgs e)
        {
            comboBox1.Visible = true;
            saveButton.Visible = true;
        }

        public void СreatingAdministrator()
        {
            var admin = new Admin();

            admin.Login = CreatingLogin();
            admin.Password = CreatingPasswod();
            admin.Id = Guid.NewGuid();

            _db.Admins.Add(admin);

            MessageBox.Show("Поздравляем! Вы успешно прошли регистрацию");
            Thread.Sleep(50);

            _db.SaveDB();

        }

        public string CreatingLogin()
        {
            int nameLen = 6;
            char[] vowels = "aeuoyi".ToCharArray();
            char[] consonants = "qwrtpsdfghjklzxcvbnm".ToCharArray();

            var rand = new Random();

            var newNick = new StringBuilder();

            while (newNick.Length < nameLen)
            {
                var firstVowel = rand.Next(0, 2) == 0 ? true : false;

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

        public string CreatingPasswod()
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

        public void SaveHistoryActionWhithUser(Guid Id, AdministratorActionsWithUser userActions, Guid _id)
        {
            var historyActionWithUser = new HistoryActionsWithUser();
            historyActionWithUser.IdAdministrator = _id;
            historyActionWithUser.IdPerson = Id;
            historyActionWithUser.TypeActionsWithUser = userActions;
            historyActionWithUser.DateTime = DateTime.Now;

            _db.HistoryActionsWithUsers.Add(historyActionWithUser);
        }
    }
}
