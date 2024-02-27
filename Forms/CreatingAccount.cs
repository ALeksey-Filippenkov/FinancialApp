using FinancialApp.DataBase;
using FinancialApp.DataBase.DbModels;
using FinancialApp.Enum;

namespace FinancialApp.Forms
{
    public partial class CreatingAccount : Form
    {
        private readonly DB _db;
        private readonly Guid _id;
        private readonly DbFinancial _context;

        public CreatingAccount(DB db, Guid id, DbFinancial context)
        {
            InitializeComponent();
            _db = db;
            _id = id;
            _context = context;
        }

        private void CreatingCashAccount_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали валюту для счета");
                return;
            }
            else
            {
                var money = new DbPersonMoney
                {
                    PersonId = _id,
                    Id = Guid.NewGuid(),
                    Type = (CurrencyType)listBox1.SelectedIndex,
                    Balance = 0
                };
                _context.Money.Add(money);
                MessageBox.Show("Поздравляем! Вы успешно создали счет");
                Thread.Sleep(50);
                _context.SaveChanges();
                Close();
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
