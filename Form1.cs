using System.Linq;

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
            var SeachAccount = _db.
                Persons.
                FirstOrDefault(enterAccount => enterAccount.Login == loginInput.Text && enterAccount.Password == passwordInput.Text);
            if (SeachAccount == null)
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }
            EnterAccount(SeachAccount.PersonId);
        }

        private void EnterAccount(Guid PersonId)
        {
            var enterForms = new EnterForm(PersonId, this, _db);
            enterForms.Show();
            Hide();
        }
    }
}