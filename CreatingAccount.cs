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
    public partial class CreatingAccount : Form
    {
        private DB _db;
        private Guid _personId;

        private int _listBox1Index;

        public CreatingAccount(DB db, Guid personId)
        {
            InitializeComponent();
            _db = db;
            _personId = personId;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                _listBox1Index = -1;
                MessageBox.Show("Вы не выбрали тип валюты");
            }
            else if (listBox1.SelectedIndex == 0)
            {
                _listBox1Index = 0;
            }
            else if (listBox1.SelectedIndex == 1)
            {
                _listBox1Index = 1;
            }
            else if (listBox1.SelectedIndex == 2)
            {
                _listBox1Index = 2;
            }
        }

        private void creatingCashAccount_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали валюту для счета");
                return;
            }
            else
            {
                var money = new PersonMoney();
                var moneyId = Guid.NewGuid();
                money.PersonId = _personId;
                money.MoneyId = moneyId;
                money.Type = listBox1.Items[_listBox1Index].ToString();
                money.Balance = 0;
                _db.Money.Add(money);
                MessageBox.Show("Поздравляем! Вы успешно создали счет");
                Thread.Sleep(50);
                _db.SaveDB();
                Close();
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
