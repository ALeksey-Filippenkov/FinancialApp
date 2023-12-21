using FinancialApp.DataBase;
using FinancialApp.Enum;

namespace FinancialApp
{
    public partial class EnterForm : Form
    {
        private Form _form;
        private DB _db;
        private Guid _Id;

        public EnterForm(Guid Id, Form Form1, DB db)
        {
            InitializeComponent();
            _Id = Id;
            _form = Form1;
            _db = db;
        }

        private void saveLKButton_Click(object sender, EventArgs e)
        {
            var ageValue = Int32.TryParse(age.Text, out int ageInt);

            if (ageInt == null)
            {
                MessageBox.Show("Поле обязательно для заполнения");
                return;
            }
            else if (ageValue == false)
            {
                MessageBox.Show("Возраст должен быть числом");
                return;
            }

            var phoneNumberValue = Int32.TryParse(phone.Text, out int phoneNumberInt);
            if (phoneNumberInt == null)
            {
                MessageBox.Show("Поле обязательно для заполнения");
                return;
            }
            else if (phoneNumberValue == false)
            {
                MessageBox.Show("Номер телефона должен быть в виде числа");
                return;
            }

            _db.SaveDB();
            MessageBox.Show("Изменения сохранены");
            Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Вы действительно хотите выйти?",
                "Подтверждение выхода",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            _form.Show();
            Close();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            var _person = _db.Persons.First(p => p.Id == _Id);
            name.Text = _person.Name;
            surname.Text = _person.Surname;
            age.Text = _person.Age.ToString();
            city.Text = _person.City;
            adress.Text = _person.Adress;
            phone.Text = _person.PhoneNumber.ToString();
            email.Text = _person.EmailAdress;
            login.Text = _person.Login;
            password.Text = _person.Password;
        }
        private void tabPage2_Click(object sender, EventArgs e)
        {
            var seachTransfer = _db.HistoryTransfers.FirstOrDefault(s => s.SenderId == _Id);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var addmoney = new AddMoney(_db, _Id);
            addmoney.Show();
        }

        private void creatAccount_Click(object sender, EventArgs e)
        {
            var creatingAccount = new CreatingAccount(_db, _Id);
            creatingAccount.Show();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            this.Refresh();
            tabPage2.Update();
            var rub = _db.Money.FirstOrDefault(p => p.PersonId == _Id && p.Type == CurrencyType.RUB);
            var usd = _db.Money.FirstOrDefault(p => p.PersonId == _Id && p.Type == CurrencyType.USD);
            var eur = _db.Money.FirstOrDefault(p => p.PersonId == _Id && p.Type == CurrencyType.EUR);

            if (rub != null)
            {
                rubLebel.Text = rub.Balance.ToString();
            }
            if (usd != null)
            {
                usdLebel.Text = usd.Balance.ToString();
            }
            if (eur != null)
            {
                eurLebel.Text = eur.Balance.ToString();
            }

            PrintHitory();
        }

        public void PrintHitory()
        {
            historyLebel.Text = "История переводов";

            foreach (var transferItem in _db.HistoryTransfers)
            {
                var personSender = _db.Persons.First(p => p.Id == transferItem.SenderId);
                var today = DateOnly.FromDateTime(transferItem.DateTime);
                var personRecipient = _db.Persons.FirstOrDefault(p => p.Id == transferItem.RecipientId);

                int index = historyOperationDataGridView.Rows.Add();
                historyOperationDataGridView.Rows[index].Cells[0].Value = today;
                historyOperationDataGridView.Rows[index].Cells[1].Value = personSender.Name;
                historyOperationDataGridView.Rows[index].Cells[2].Value = transferItem.Type;
                historyOperationDataGridView.Rows[index].Cells[3].Value = transferItem.MoneyTransfer;
                historyOperationDataGridView.Rows[index].Cells[4].Value = personRecipient.Name;

                historyLebel.Text += $"\n{personSender.Name} {today}";
                if (personRecipient == null )
                    historyLebel.Text += $" пополнил счет {transferItem.Type} на  {transferItem.MoneyTransfer}";

                else 
                    historyLebel.Text += $" перевел {transferItem.MoneyTransfer} {transferItem.Type} {personRecipient.Name}";
            }
        }
        private void moneyTransferButton_Click(object sender, EventArgs e)
        {
            var moneyTransfer = new MoneyTransfer(_db, _Id);
            moneyTransfer.Show();
        }

        private void seachButton_Click(object sender, EventArgs e)
        {
            var operationSearch = new OperationSearch(_db);
            operationSearch.Show();
        }
    }
}