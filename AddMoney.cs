using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinancialApp
{
    public partial class AddMoney : Form
    {
        private DB _db;
        private Guid _personId;
        private int _listBox1Index;
        private double _money;


        public AddMoney(DB db, Guid personId)
        {
            InitializeComponent();
            _db = db;
            _personId = personId;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
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
        }

        private void addMoneyButton_Click(object sender, EventArgs e)
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
                _money = moneyDouble;

            var moneyAccount = _db.Money.FirstOrDefault(t => t.Type == currencyList.Items[_listBox1Index].ToString() && t.PersonId == _personId);

            if (moneyAccount == null)
            {
                MessageBox.Show("Счета с таким видом валюты не найден");
            }
            else
            {
                moneyAccount.Balance += _money;
            }

            MessageBox.Show("Поздравляем! Вы успешно добавили деньги");
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
