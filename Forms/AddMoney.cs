using FinancialApp.DataBase;
using FinancialApp.Enum;
using FinancialApp.GeneralMethods;
using System;

namespace FinancialApp
{
    public partial class AddMoney : Form
    {
        private DB _db;
        private Guid _Id;

        public AddMoney(DB db, Guid Id)
        {
            InitializeComponent();
            _db = db;
            _Id = Id;
        }

        private void addMoneyButton_Click(object sender, EventArgs e)
        {
            CheckingTheEnteredData();
        }

        public void CheckingTheEnteredData()
        {
            var moneyValue = Double.TryParse(addMoneyBox.Text, out double moneyDouble);
            if (addMoneyBox.Text == String.Empty)
            {
                MessageBox.Show("Вы ничего не ввели");
                return;
            }
            if (!moneyValue)
            {
                MessageBox.Show("Вы ввели не число.");
                return;
            }
            else if (moneyDouble < 0)
            {
                MessageBox.Show("Внесение денег не может быть меньше 0");
                return;
            }
            else
                AddingMoneyToTheAccount(moneyDouble);
        }

        public void AddingMoneyToTheAccount(double moneyDouble)
        {
            var moneyAccount = CommonMethod.GetSearchAccountOwner(_db, currencyList.SelectedIndex, _Id);
            if (moneyAccount == null)
            {
                MessageBox.Show("Счета с таким видом валюты не найден");
                return;
            }
            else
            {
                moneyAccount.Balance += moneyDouble;
                AddingMoneyToTheHistory(moneyDouble);
                MessageBox.Show("Поздравляем! Вы успешно добавили деньги");
                Thread.Sleep(50);
                _db.SaveDB();
                Close();
            }
        }

        public void AddingMoneyToTheHistory(double moneyDouble) 
        {
            var historyTransfer = new HistoryTransfer();
            historyTransfer.SenderId = _Id;
            historyTransfer.DateTime = DateTime.Now;
            historyTransfer.Type = (CurrencyType)currencyList.SelectedIndex;
            historyTransfer.MoneyTransfer = moneyDouble;
            _db.HistoryTransfers.Add(historyTransfer);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}