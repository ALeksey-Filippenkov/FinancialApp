using FinancialApp.DataBase;

namespace FinancialApp.Forms
{
    public partial class Form1 : Form
    {
        private readonly DB _db;

        public Form1()
        {
            InitializeComponent();
            _db = new DB();
            _db = _db.ReadDB();
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            SearchPerson();
        }

        private void RegistrationButton_Click(object sender, EventArgs e)
        {
            var registrationForms = new Registration(_db);
            registrationForms.Show();
        }

        private void SearchPerson()
        {
            var searchAccount = _db.Persons.FirstOrDefault(enterAccount => enterAccount.Login == loginInput.Text && enterAccount.Password == passwordInput.Text);

            switch (searchAccount)
            {
                case { IsBanned: false }:
                    EnterAccount(searchAccount.Id);
                    break;
                case { IsBanned: true }:
                    MessageBox.Show("Пользователь забанен");
                    return;
                default:
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
                        var searchAdmin = _db.Admins.FirstOrDefault(admin => admin.Login == loginInput.Text && admin.Password == passwordInput.Text);
                        if (searchAdmin == null)
                        {
                            MessageBox.Show("Неверный логин или пароль");
                            return;
                        }

                        var administratorsPersonalAccount = new AdministratorsPersonalAccount(false, this, _db, searchAdmin.Id);
                        administratorsPersonalAccount.Show();
                        Hide();
                    }
                    break;
                }
            }
        }

        private void EnterAccount(Guid id)
        {
            var enterForms = new EnterForm(id, this, _db);
            enterForms.Show();
            Hide();
        }
    }
}