using FinancialApp.DataBase;
using FinancialApp.Enum;
using FinancialApp.GeneralMethods;

namespace FinancialApp.Forms
{
    public partial class AddMoney : Form
    {
        private readonly DB _db;
        private readonly Guid _id;
        private readonly FormData _formData;

        public AddMoney(DB db, Guid id, FormData formData)
        {
            InitializeComponent();
            _db = db;
            _id = id;
            _formData = formData;
        }

        private void AddMoneyButton_Click(object sender, EventArgs e)
        {
            CheckingTheEnteredData();
        }

        public void CheckingTheEnteredData()
        {
            var moneyValue = double.TryParse(addMoneyBox.Text, out var moneyDouble);
            if (addMoneyBox.Text == string.Empty)
            {
                MessageBox.Show("Вы ничего не ввели");
                return;
            }

            if (!moneyValue)
            {
                MessageBox.Show("Вы ввели не число.");
                return;
            }

            if (moneyDouble < 0)
            {
                MessageBox.Show("Внесение денег не может быть меньше 0");
                return;
            }

            AddingMoneyToTheAccount(moneyDouble);
        }

        public void AddingMoneyToTheAccount(double moneyDouble)
        {
            var moneyAccount = CommonMethod.GetSearchAccountOwner(_db, currencyList.SelectedIndex, _id);
            if (moneyAccount == null)
            {
                MessageBox.Show("Счета с таким видом валюты не найден");
                return;
            }

            moneyAccount.Balance += moneyDouble;
            AddingMoneyToTheHistory(moneyDouble);
            MessageBox.Show("Поздравляем! Вы успешно добавили деньги");
            Thread.Sleep(50);
            _db.SaveDB();
            Close();
        }

        public void AddingMoneyToTheHistory(double moneyDouble)
        {
            var historyTransfer = new HistoryTransfer
            {
                SenderId = _id,
                RecipientId = _id,
                DateTime = DateTime.Now,
                Type = (CurrencyType)currencyList.SelectedIndex,
                MoneyTransfer = moneyDouble,
                OperationType = TypeOfOperation.refill
            };
            _db.HistoryTransfers.Add(historyTransfer);

            PrintHistory.GetPrintHistory(_db, CommonMethod.GetHistoryTransfer(_db, _id), _formData);
            RebootLabelText.LabelText(_db, _id, _formData);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}