using FinancialApp.DataBase;
using FinancialApp.Enum;
using FinancialApp.GeneralMethods;

namespace FinancialApp
{
    public partial class EnterForm : Form
    {
        private Form _form;
        private DB _db;
        private Guid _id;
        private List<HistoryTransfer>? _operationHistorySeach;
        private PrintHitory _printHistory;

        public EnterForm(Guid id, Form Form1, DB db)
        {
            InitializeComponent();
            _id = id;
            _form = Form1;
            _db = db;
            this.Refresh();
            tabPage2.Update();
            EnterLebelText();
            _operationHistorySeach = _db.HistoryTransfers.Where(h => h.SenderId == _id || h.RecipientId == _id).ToList();
            PrintHitory _printHistory = new PrintHitory(_db, _id, _operationHistorySeach);
            _printHistory.GetPrintHitory(historyOperationDataGridView);
        }

        private void EnterForm_Load(object sender, EventArgs e)
        {
            EnterLebelText();
            
            _printHistory.GetPrintHitory(historyOperationDataGridView);
        }

        private void saveLKButton_Click(object sender, EventArgs e)
        {
            if (age.Text == null || phone.Text == null)
            {
                MessageBox.Show("Поле обязательно для заполнения");
                return;
            }
            else
            {
                var ageValue = Int32.TryParse(age.Text, out int ageInt);
                if (ageValue == false)
                {
                    MessageBox.Show("Возраст должен быть числом");
                    return;
                }
                var phoneNumberValue = Int32.TryParse(phone.Text, out int phoneNumberInt);
                if (phoneNumberValue == false)
                {
                    MessageBox.Show("Номер телефона должен быть в виде числа");
                    return;
                }
            }
            _db.SaveDB();
            MessageBox.Show("Изменения сохранены");
            Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
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

        private void tabPage1_Click(object sender, EventArgs e)
        {
            var _person = _db.Persons.First(p => p.Id == _id);
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
            Refresh();
            tabPage2.Update();
            var seachTransfer = _db.HistoryTransfers.FirstOrDefault(s => s.SenderId == _id);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var addMoney = new AddMoney(_db, _id, this);
            addMoney.Show();
        }

        private void creatAccount_Click(object sender, EventArgs e)
        {
            var creatingAccount = new CreatingAccount(_db, _id);
            creatingAccount.Show();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            EnterLebelText();
            _printHistory.GetPrintHitory(historyOperationDataGridView);
        }

        private void EnterLebelText()
        {
            Refresh();
            tabPage2.Update();
            var rub = _db.Money.FirstOrDefault(p => p.PersonId == _id && p.Type == CurrencyType.RUB);
            var usd = _db.Money.FirstOrDefault(p => p.PersonId == _id && p.Type == CurrencyType.USD);
            var eur = _db.Money.FirstOrDefault(p => p.PersonId == _id && p.Type == CurrencyType.EUR);

            if (rub != null)
            {
                rubLebel.Text = Math.Round(rub.Balance, 2).ToString();
            }
            if (usd != null)
            {
                usdLebel.Text = Math.Round(usd.Balance, 2).ToString();
            }
            if (eur != null)
            {
                eurLebel.Text = Math.Round(eur.Balance, 2).ToString();
            }
        }

        private void moneyTransferButton_Click(object sender, EventArgs e)
        {
            var moneyTransfer = new MoneyTransfer(_db, _id);
            moneyTransfer.Show();
        }

        private void seachButton_Click(object sender, EventArgs e)
        {
            var operationSearch = new OperationSearch(_db, _id);
            operationSearch.Show();
        }

        private void historyOperationExel_Click(object sender, EventArgs e)
        {
            DataOutputInExcel newExel = new DataOutputInExcel(_db, _id);
            newExel.GetDataOutputInExcel(_operationHistorySeach);
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            Panel panel = new Panel();
            panel.Location = new Point(30, 30);
            panel.Visible = true;
            tabPage3.Controls.Add(panel);
        }

    }
}