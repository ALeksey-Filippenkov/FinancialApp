using FinancialApp.DataBase;
using System.Drawing.Text;

namespace FinancialApp.Forms
{
    public partial class AdministratorsPersonalAccount : Form
    {
        private Form _form;
        private Guid _id;
        private DB _db;
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
            _trueOrFalse = true;
        }

        private void deleteUserButton_Click(object sender, EventArgs e)
        {

        }

        private void findUserOperationsButton_Click(object sender, EventArgs e)
        {

        }

        private void restoreUserButton_Click(object sender, EventArgs e)
        {
            VisibleButtoTrue();
            _trueOrFalse = false;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            BanUnbanUser(_trueOrFalse);
        }

        private void BanUnbanUser(bool item) 
        {
            var sechPerson = _db.Persons.FirstOrDefault(p => p.Name == textBox1.Text);

            if (sechPerson == null)
            {
                MessageBox.Show("Пользователя с таким именем нет");
                return;
            }
            else if (!item)
            {
                sechPerson.IsBanned = false;
                _db.SaveDB();
                MessageBox.Show("Пользователь успешно разбанен");
                VisibleButtoFalse();
            }
            else 
            {
                sechPerson.IsBanned = true;
                _db.SaveDB();
                MessageBox.Show("Пользователь успешно забанен");
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
