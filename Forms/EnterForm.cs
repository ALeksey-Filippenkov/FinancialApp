using FinancialApp.DataBase;
using FinancialApp.Enum;
using FinancialApp.GeneralMethods;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Excel = Microsoft.Office.Interop.Excel;

namespace FinancialApp
{
    public partial class EnterForm : Form
    {
        private Form _form;
        private DB _db;
        private Guid _Id;
        private List<HistoryTransfer> _operationHistorySeach;

        public EnterForm(Guid Id, Form Form1, DB db)
        {
            InitializeComponent();
            _Id = Id;
            _form = Form1;
            _db = db;
            this.Refresh();
            tabPage2.Update();
            EnterLebelText();
            PrintHitory();
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
            var _person = _db.Persons.First(p => p.Id == _Id);
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
            var seachTransfer = _db.HistoryTransfers.FirstOrDefault(s => s.SenderId == _Id);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var addMoney = new AddMoney(_db, _Id);
            addMoney.Show();
        }

        private void creatAccount_Click(object sender, EventArgs e)
        {
            var creatingAccount = new CreatingAccount(_db, _Id);
            creatingAccount.Show();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            EnterLebelText();

            PrintHitory();
        }
        private void EnterLebelText()
        {
            this.Refresh();
            tabPage2.Update();
            var rub = _db.Money.FirstOrDefault(p => p.PersonId == _Id && p.Type == CurrencyType.RUB);
            var usd = _db.Money.FirstOrDefault(p => p.PersonId == _Id && p.Type == CurrencyType.USD);
            var eur = _db.Money.FirstOrDefault(p => p.PersonId == _Id && p.Type == CurrencyType.EUR);

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

        public void PrintHitory()
        {
            historyOperationDataGridView.Rows.Clear();

            _operationHistorySeach = _db.HistoryTransfers.Where(h => h.SenderId == _Id || h.RecipientId == _Id).ToList();

            foreach (var transferItem in _operationHistorySeach)
            {
                var personSender = _db.Persons.First(p => p.Id == transferItem.SenderId);
                var today = DateOnly.FromDateTime(transferItem.DateTime);
                var personRecipient = _db.Persons.FirstOrDefault(p => p.Id == transferItem.RecipientId);

                int index = historyOperationDataGridView.Rows.Add();

                historyOperationDataGridView.Rows[index].Cells[0].Value = today;
                historyOperationDataGridView.Rows[index].Cells[3].Value = transferItem.Type;
                historyOperationDataGridView.Rows[index].Cells[4].Value = transferItem.MoneyTransfer;

                if (transferItem.SenderId == transferItem.RecipientId)
                {
                    historyOperationDataGridView.Rows[index].Cells[1].Value = TypeOfOperation.Обмен.ToString();
                    historyOperationDataGridView.Rows[index].Cells[2].Value = personSender.Name;
                }
                else if (personRecipient == null)
                {
                    historyOperationDataGridView.Rows[index].Cells[1].Value = TypeOfOperation.Пополнение.ToString();
                    historyOperationDataGridView.Rows[index].Cells[2].Value = personSender.Name;
                }
                else if (transferItem.RecipientId == _Id)
                {
                    historyOperationDataGridView.Rows[index].Cells[1].Value = TypeOfOperation.Перевод.ToString();
                    historyOperationDataGridView.Rows[index].Cells[2].Value = personSender.Name;
                    historyOperationDataGridView.Rows[index].Cells[5].Value = personRecipient.Name;
                }
                else if (transferItem.SenderId == _Id && personRecipient != null)
                {
                    historyOperationDataGridView.Rows[index].Cells[1].Value = TypeOfOperation.Перевод.ToString();
                    historyOperationDataGridView.Rows[index].Cells[2].Value = personSender.Name;
                    historyOperationDataGridView.Rows[index].Cells[5].Value = personRecipient.Name;
                }
            }
        }

        private void moneyTransferButton_Click(object sender, EventArgs e)
        {
            var moneyTransfer = new MoneyTransfer(_db, _Id);
            moneyTransfer.Show();
        }

        private void seachButton_Click(object sender, EventArgs e)
        {
            var operationSearch = new OperationSearch(_db, _Id);
            operationSearch.Show();
        }

        private void historyOperationExel_Click(object sender, EventArgs e)
        {
            DataOutputInExcel newExel = new DataOutputInExcel(_db, _Id);
            newExel.GetDataOutputInExcel(_operationHistorySeach);
        }
    }
}