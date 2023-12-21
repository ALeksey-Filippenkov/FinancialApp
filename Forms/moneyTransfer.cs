using FinancialApp.DataBase;
using FinancialApp.Enum;

namespace FinancialApp
{
    public partial class MoneyTransfer : Form
    {
        private DB _db;
        private Guid _Id;
        private int _phoneNumerTransfer;
        private double _moneyTransfer;

        public MoneyTransfer(DB db, Guid Id)
        {
            InitializeComponent();
            _db = db;
            _Id = Id;
        }

        private void transferButton_Click(object sender, EventArgs e)
        {
            CheckingTheEnteredData(phoneNumerTransferInput.Text, moneyTransferInput.Text);

            CheckingThePossibilityOfTranslation();
        }

        public void CheckingTheEnteredData(string phoneNumber, string moneyTransfer)
        {
            var phoneNumerTransferValue = Int32.TryParse(phoneNumber, out int phoneNumerTransferInt);
            if (phoneNumerTransferInput.Text == String.Empty)
            {
                MessageBox.Show("Необходимо указать номер телефона получателя в виде числа.");
                return;
            }
            else if (!phoneNumerTransferValue)
            {
                MessageBox.Show("Вы ввели не номер телефона.");
                return;
            }
            else
            {
                _phoneNumerTransfer = phoneNumerTransferInt;
            }

            if (currencyList.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали валюту для перевода");
                return;
            }

            var moneyTransferValue = Int32.TryParse(moneyTransfer, out int moneyTransferDouble);
            if (moneyTransferInput.Text == String.Empty)
            {
                MessageBox.Show("Вы не ввели сумму для перевода");
                return;
            }
            else if (!moneyTransferValue)
            {
                MessageBox.Show("Деньги могут быть только ввиде чисел");
                return;
            }
            else if (moneyTransferDouble < 0)
            {
                MessageBox.Show("Внесение денег не может быть меньше 0");
                return;
            }
            else
                _moneyTransfer = moneyTransferDouble;
        }

        public void CheckingThePossibilityOfTranslation()
        {
            var personSender = _db.Money.FirstOrDefault(m => m.Type == (CurrencyType)currencyList.SelectedIndex && m.PersonId == _Id);
            var personRecipient = _db.Persons.FirstOrDefault(p => p.PhoneNumber == _phoneNumerTransfer);
            var transfer = _db.Money.FirstOrDefault(t => t.Type == (CurrencyType)currencyList.SelectedIndex && t.PersonId == personRecipient.Id);

            if (personRecipient == null)
            {
                MessageBox.Show("Пользователь с таким номером телефона не найден");
                return;
            }
            else if (personSender == null)
            {
                MessageBox.Show("У Вас нет счета с таким типом валюты");
                return;
            }
            else if (personSender.Balance - _moneyTransfer < 0)
            {
                MessageBox.Show("Недостаточно денег на счету");
                return;
            }
            else if (transfer == null)
            {
                MessageBox.Show("У пользователя нет счета с такой валютой");
                return;
            }
            else
                SuccessfulMoneyTransfer(personSender, personRecipient, transfer);
        }

        public void SuccessfulMoneyTransfer(PersonMoney personSender, Person personRecipient, PersonMoney transfer)
        {

            transfer.Balance += _moneyTransfer;
            personSender.Balance -= _moneyTransfer;
            MessageBox.Show("Поздравляем! Вы успешно перевели деньги");

            SavingTheTranslationHistory(personRecipient);

            Thread.Sleep(50);
            _db.SaveDB();
            Close();
        }

        public void SavingTheTranslationHistory(Person personRecipient)
        {
            var historyTransfer = new HistoryTransfer();
            historyTransfer.DateTime = DateTime.Now;
            historyTransfer.SenderId = _Id;
            historyTransfer.Type = (CurrencyType)currencyList.SelectedIndex;
            historyTransfer.RecipientId = personRecipient.Id;
            historyTransfer.MoneyTransfer = _moneyTransfer;
            _db.HistoryTransfers.Add(historyTransfer);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
