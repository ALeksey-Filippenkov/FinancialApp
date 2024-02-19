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
            var searchAdmin = _db.Admins.FirstOrDefault(admin => admin.Login == loginInput.Text && admin.Password == passwordInput.Text);

            switch (searchAccount)
            {
                case { IsBanned: false }:
                    if (searchAdmin != null && searchAccount.Login.Equals(searchAdmin.Login) && searchAccount.Password.Equals(searchAdmin.Password))
                    {
                        var messageBox = new MessageBoxForm();
                        var result = messageBox.ShowDialog();
                        switch (result)
                        {
                            case DialogResult.OK:
                                EnterAccount(searchAccount.Id);
                                break;
                            case DialogResult.No:
                            {
                                var administratorsPersonalAccount = new AdministratorsPersonalAccount(false, this, _db, searchAdmin.Id);
                                administratorsPersonalAccount.Show();
                                Hide();
                                break;
                            }
                            default:
                                messageBox.Close();
                                break;
                        }
                    }
                    else
                    {
                        EnterAccount(searchAccount.Id);
                    }
                    break;
                case { IsBanned: true }:
                    MessageBox.Show("������������ �������");
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
                        if (searchAdmin == null)
                        {
                            MessageBox.Show("�������� ����� ��� ������");
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