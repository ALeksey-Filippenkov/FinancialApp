using FinancialApp.DataBase;
using FinancialApp.Enum;

namespace FinancialApp
{
    public partial class CreatingAccount : Form
    {
        private DB _db;
        private Guid _Id;

        public CreatingAccount(DB db, Guid Id)
        {
            InitializeComponent();
            _db = db;
            _Id = Id;
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
                money.PersonId = _Id;
                money.Id = Guid.NewGuid();
                money.Type = (CurrencyType)listBox1.SelectedIndex;
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
