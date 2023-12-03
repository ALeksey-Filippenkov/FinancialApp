using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FinancialApp
{
    public partial class EnterForm : Form
    {
        private Form _form;
        private DB _db;
        private Guid _personId;

        public EnterForm(Guid PersonId, Form Form1, DB db)
        {
            InitializeComponent();
            _personId = PersonId;
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
            var _person = _db.Persons.First(p => p.PersonId == _personId);
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
            var seachTransfer = _db.HistoryTransfers.FirstOrDefault(s => s.SenderId == _personId);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var addmoney = new AddMoney(_db, _personId);
            addmoney.Show();
        }

        private void creatAccount_Click(object sender, EventArgs e)
        {
            var creatingAccount = new CreatingAccount(_db, _personId);
            creatingAccount.Show();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            this.Refresh();
            tabPage2.Update();
            var rub = _db.Money.FirstOrDefault(p => p.PersonId == _personId && p.Type == "RUB");
            var usd = _db.Money.FirstOrDefault(p => p.PersonId == _personId && p.Type == "USD");
            var eur = _db.Money.FirstOrDefault(p => p.PersonId == _personId && p.Type == "EUR");

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

            var recipientId = _db.HistoryTransfers.First().RecipientId;
            var senderId = _db.HistoryTransfers.First().SenderId;
            var personSender = _db.Persons.First(p => p.PersonId == senderId);
            var personRecipient = _db.Persons.First(p => p.PersonId == recipientId);

            foreach (var transferItem in _db.HistoryTransfers)
            {
                historyLebel.Text += $"\n{personSender.Name} перевел {transferItem.MoneyTransfer} {transferItem.Type} {personRecipient.Name}";
            }
        }

        private void moneyTransferButton_Click(object sender, EventArgs e)
        {
            var moneyTransfer = new MoneyTransfer(_db, _personId);
            moneyTransfer.Show();
        }
    }
}
