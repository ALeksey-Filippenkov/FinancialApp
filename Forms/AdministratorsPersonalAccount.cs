using FinancialApp.DataBase;
using FinancialApp.Enum;
using System;
using System.Security.Cryptography;
using System.Text;


namespace FinancialApp.Forms
{
    public partial class AdministratorsPersonalAccount : Form
    {
        private Form _form;
        private DB _db;
        private AdministratorActionsWithUser _userActions;
        private bool _isGeneralAdmin;


        public AdministratorsPersonalAccount(bool isGeneralAdmin, Form form1, DB db)
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
            _userActions = AdministratorActionsWithUser.ban_user;
        }

        private void deleteUserButton_Click(object sender, EventArgs e)
        {
            VisibleButtoTrue();
            var person = SeachPerson();
            _userActions = AdministratorActionsWithUser.delete_user;
        }

        private void findUserOperationsButton_Click(object sender, EventArgs e)
        {
            var findUserOperations = new FindUserOperations(_db);
            findUserOperations.Show();
        }

        private void restoreUserButton_Click(object sender, EventArgs e)
        {
            VisibleButtoTrue();
            _userActions = AdministratorActionsWithUser.unban_user;
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
                    MessageBox.Show("Поздравляем! Вы успешно прошли регистрацию");
                    Thread.Sleep(50);
                    _db.SaveDB();
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
            else if (userActions == AdministratorActionsWithUser.unban_user)
            {
                person.IsBanned = false;
                _db.SaveDB();
                MessageBox.Show("Пользователь успешно разбанен");
                VisibleButtoFalse();
            }
            else if (userActions == AdministratorActionsWithUser.ban_user)
            {
                person.IsBanned = true;
                _db.SaveDB();
                MessageBox.Show("Пользователь успешно забанен");
                VisibleButtoFalse();
            }
            else if (userActions == AdministratorActionsWithUser.delete_user)
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
        }

        public string CreatingLogin()
        {
            int nameLen = 6;
            char[] vowels = "aeuoyi".ToCharArray();
            char[] consonants = "qwrtpsdfghjklzxcvbnm".ToCharArray();

            Random rand = new Random();

            StringBuilder newNick = new StringBuilder();

            while (newNick.Length < nameLen)
            {
                bool firstVowel = rand.Next(0, 2) == 0 ? true : false;

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
            int length = 8;
            const string valid = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
