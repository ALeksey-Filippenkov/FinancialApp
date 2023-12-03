using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinancialApp
{
    public partial class MoneyTransfer : Form
    {
        private DB _db;
        private Guid _personId;
        private int _listBox1Index;
        private int _phoneNumerTransfer;

        public MoneyTransfer(DB db, Guid personId)
        {
            InitializeComponent();
            _db = db;
            _personId = personId;
        }

        private void transferButton_Click(object sender, EventArgs e)
        {
            var phoneNumerTransferValue = Int32.TryParse(phoneNumerTransferInput.Text, out int phoneNumerTransferInt);
            if (phoneNumerTransferInput.Text == String.Empty)
            {
                MessageBox.Show("Вы ничего не ввели");
                return;
            }
            if (!phoneNumerTransferValue)
            {
                MessageBox.Show("Вы ввели не номер телефона.");
                return;
            }
            else
            {
                _phoneNumerTransfer = phoneNumerTransferInt;
            }

            var flag = true;
            do
            {
                if (currencyList.SelectedIndex == -1)
                {
                    _listBox1Index = -1;
                    MessageBox.Show("Вы не выбрали тип валюты");
                }
                else if (currencyList.SelectedIndex == 0)
                {
                    _listBox1Index = 0;
                    flag = false;
                }
                else if (currencyList.SelectedIndex == 1)
                {
                    _listBox1Index = 1;
                    flag = false;
                }
                else if (currencyList.SelectedIndex == 2)
                {
                    _listBox1Index = 2;
                    flag = false;
                }
            } while (flag);

            var moneyTransferValue = Int32.TryParse(moneyTransferInput.Text, out int moneyTransferDouble);
            if (moneyTransferInput.Text == String.Empty)
            {
                MessageBox.Show("Вы ничего не ввели");
                return;
            }
            if (!moneyTransferValue)
            {
                MessageBox.Show("Номер телефона должен быть представлен ввиде чисел.");
                return;
            }
            else if (moneyTransferDouble < 0)
            {
                MessageBox.Show("Внесение денег не может быть меньше 0");
                return;
            }

            var historyTransfer = new HistoryTransfer();
            historyTransfer.DateTime = DateTime.Now;
            historyTransfer.SenderId = _personId;
            historyTransfer.Type = currencyList.Items[_listBox1Index].ToString();
            var seachPerson = _db.Persons.FirstOrDefault(p => p.PhoneNumber == _phoneNumerTransfer);
            if (seachPerson == null)
            {
                MessageBox.Show("Пользователь с таким номером телефона не найден");
                return;
            }
            else
            {
                historyTransfer.RecipientId = seachPerson.PersonId;
            }
            historyTransfer.MoneyTransfer = moneyTransferDouble;
            _db.HistoryTransfers.Add(historyTransfer);

            var transfer = _db.Money.FirstOrDefault(t => t.Type == currencyList.Items[_listBox1Index].ToString() && t.PersonId == seachPerson.PersonId);
            if (transfer == null)
            {
                MessageBox.Show("У пользователя нет счета с такой валютой");
                return;
            }
            else
            {
                var moneyAccount = _db.Money.FirstOrDefault(t => t.Type == currencyList.Items[_listBox1Index].ToString() && t.PersonId == _personId);
                if (moneyAccount.Balance > 0)
                {
                    transfer.Balance += moneyTransferDouble;
                    moneyAccount.Balance -= moneyTransferDouble;
                }
                else
                {
                    MessageBox.Show("Недостаточно денег на счету");
                    return;
                }
            }
            MessageBox.Show("Поздравляем! Вы успешно перевели деньги");
            Thread.Sleep(50);
            _db.SaveDB();
            Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
