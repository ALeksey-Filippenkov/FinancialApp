using FinancialApp.DataBase;
using FinancialApp.Forms;

namespace FinancialApp
{
    public partial class Form1 : Form
    {
        private DB _db;

        public Form1()
        {
            InitializeComponent();
            _db = new DB();
            _db = _db.ReadDB();
        }

        private void enterButton_Click(object sender, EventArgs e)
        {
            SeachPerson();
        }

        private void registrationButton_Click(object sender, EventArgs e)
        {
            var registrationForms = new Registration(_db);
            registrationForms.Show();
        }

        private void SeachPerson()
        {
            var seachAccount = _db.Persons.FirstOrDefault(enterAccount => enterAccount.Login == loginInput.Text && enterAccount.Password == passwordInput.Text);

            if (seachAccount != null && !seachAccount.IsBanned)
            {
                EnterAccount(seachAccount.Id);
            }
            else if (seachAccount != null && seachAccount.IsBanned)
            {
                MessageBox.Show("Пользователь забанен");
                return;
            }
            else
            {
                var generalAdmin = new Admin() { Password = "admin", Login = "admin", Id = Guid.NewGuid()};

                if (generalAdmin.Password == passwordInput.Text && generalAdmin.Login == loginInput.Text)
                {
                    var administratorsPersonalAccount = new AdministratorsPersonalAccount(true, this, _db, generalAdmin.Id);
                    administratorsPersonalAccount.Show();
                    Hide();
                }
                else
                {
                    var seachAdmin = _db.Admins.FirstOrDefault(admin => admin.Login == loginInput.Text && admin.Password == passwordInput.Text);
                    if (seachAdmin == null)
                    {
                        MessageBox.Show("Неверный логин или пароль");
                        return;
                    }
                    else 
                    {
                        var administratorsPersonalAccount = new AdministratorsPersonalAccount(false, this, _db, seachAdmin.Id);
                        administratorsPersonalAccount.Show();
                        Hide();
                    }

                }               
            }
        }

        private void EnterAccount(Guid Id)
        {
            var enterForms = new EnterForm(Id, this, _db);
            enterForms.Show();
            Hide();
        }
    }
}