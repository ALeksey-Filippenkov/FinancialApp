using FinancialApp.DataBase;
using FinancialApp.Enum;
using FinancialApp.GeneralMethods;
using System.Data;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Xml.Linq;

namespace FinancialApp
{
    public partial class MoneyTransfer : Form
    {
        private DB _db;
        private Guid _Id;
        private int _phoneNumerTransfer;
        private double _moneyTransfer;
        private double _usd;
        private double _eur;

        public MoneyTransfer(DB db, Guid Id)
        {
            InitializeComponent();
            _db = db;
            _Id = Id;
            ViewrichTextBoxOutData();
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
            var personSender = CommonMethod.GetSearchAccountOwner(_db, currencyList.SelectedIndex, _Id);
            var personRecipient = _db.Persons.FirstOrDefault(p => p.PhoneNumber == _phoneNumerTransfer);
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
            var transfer = CommonMethod.GetSearchAccountOwner(_db, currencyList.SelectedIndex, personRecipient.Id);

            if (transfer == null)
            {
                MessageBox.Show("У пользователя нет счета с такой валютой");
                return;
            }
            else if (personSender.Balance - _moneyTransfer < 0)
            {
                MessageBox.Show("Недостаточно денег на счету");
                return;
            }
            else
                SuccessfulMoneyTransfer(personSender, personRecipient, transfer);
        }

        private void SuccessfulMoneyTransfer(PersonMoney personSender, Person personRecipient, PersonMoney transfer)
        {
            transfer.Balance += _moneyTransfer;
            personSender.Balance -= _moneyTransfer;
            MessageBox.Show("Поздравляем! Вы успешно перевели деньги");

            SavingTheTranslationHistory(personRecipient);

            Thread.Sleep(50);
            _db.SaveDB();
            Close();
        }

        private void SavingTheTranslationHistory(Person personRecipient)
        {
            var historyTransfer = new HistoryTransfer();
            historyTransfer.DateTime = DateTime.Now;
            historyTransfer.SenderId = _Id;
            historyTransfer.Type = (CurrencyType)currencyList.SelectedIndex;
            historyTransfer.RecipientId = personRecipient.Id;
            historyTransfer.MoneyTransfer = _moneyTransfer;
            _db.HistoryTransfers.Add(historyTransfer);
        }

        private void ViewrichTextBoxOutData()
        {
            WebClient client = new WebClient();
            var xml = client.DownloadString("https://www.cbr-xml-daily.ru/daily.xml");
            XDocument xdoc = XDocument.Parse(xml);
            var el = xdoc.Element("ValCurs").Elements("Valute");

            string dollarS = el.Where(x => x.Attribute("ID").Value == "R01235").Select(x => x.Element("Value").Value).FirstOrDefault();
            System.Globalization.CultureInfo dollar = new System.Globalization.CultureInfo("ru-Ru");
            _usd = Convert.ToDouble(dollarS, dollar);

            string eurS = el.Where(x => x.Attribute("ID").Value == "R01239").Select(x => x.Element("Value").Value).FirstOrDefault();
            System.Globalization.CultureInfo euro = new System.Globalization.CultureInfo("ru-Ru");
            _eur = Convert.ToDouble(eurS, euro);

            label7.Text = $"Курс основных валют\nЕвро: {_eur} Доллар: {_usd}";
        }

        private void exchangeButton_Click(object sender, EventArgs e)
        {
            MoneyExchange();
        }

        private void MoneyExchange()
        {
            var debit = debitAccountComboBox.SelectedIndex;
            var replenishment = replenishmentAccountComboBox.SelectedIndex;
            if (debit == -1 || replenishment == -1)
            {
                MessageBox.Show("Вы не выбрали тип валюты");
                return;
            }

            var moneyValue = Double.TryParse(moneyTextBox.Text, out double money);
            if (!moneyValue)
            {
                MessageBox.Show("Вы ввели не число.");
                return;
            }
            else
            {
                var debitAccount = CommonMethod.GetSearchAccountOwner(_db, debit, _Id);
                var replenishmentAccount = CommonMethod.GetSearchAccountOwner(_db, replenishment, _Id);
                var message = string.Empty;
                var flag = true;
                if (debitAccount == null)
                {
                    message = "У вас нет счета с которого вы хотите произвести обмен.\nВы хотите создать счет?";
                    ExchangeError(message, flag);
                    return;
                }
                else if (replenishmentAccount == null)
                {
                    message = "У вас нет счета с которого вы хотите произвести обмен.\nВы хотите создать счет?";
                    ExchangeError(message, flag);
                    return;
                }
                else if (debitAccount.Balance == 0 || debitAccount.Balance - money <= 0)
                {
                    flag = false;
                    message = "У вас недостаточно срадств на счету с которого вы хотите произвести обмен!\n Хотите пополнить счет?";
                    ExchangeError(message, flag);
                    return;
                }

                debitAccount.Balance -= money;
                if ((CurrencyType)debit == CurrencyType.RUB)
                {
                    if ((CurrencyType)replenishmentAccount.Type == CurrencyType.USD)
                    {
                        replenishmentAccount.Balance += Math.Round(money / _usd, 2);
                    }
                    else if ((CurrencyType)replenishmentAccount.Type == CurrencyType.EUR)
                    {
                        replenishmentAccount.Balance += Math.Round(money / _usd, 2);
                    }
                }
                else if ((CurrencyType)debit == CurrencyType.USD)
                {
                    if ((CurrencyType)replenishmentAccount.Type == CurrencyType.RUB)
                    {
                        replenishmentAccount.Balance += Math.Round(money * _usd, 2);
                    }
                    else if ((CurrencyType)replenishmentAccount.Type == CurrencyType.EUR)
                    {
                        replenishmentAccount.Balance += Math.Round(money / _usd, 2);
                    }
                }
                else if ((CurrencyType)debit == CurrencyType.EUR)
                {
                    if ((CurrencyType)replenishmentAccount.Type == CurrencyType.RUB)
                    {
                        replenishmentAccount.Balance += Math.Round(money * _eur, 2);
                    }
                    if ((CurrencyType)replenishmentAccount.Type == CurrencyType.USD)
                    {
                        replenishmentAccount.Balance += Math.Round(money / _usd, 2);
                    }
                }
                MessageBox.Show("Поздравляем! Вы успешно обменяли деньги деньги");

                SaveMoneyExchangeHistory(debit, money);

                Thread.Sleep(50);
                _db.SaveDB();
                Close();
            }
        }

        private void SaveMoneyExchangeHistory(int debit, double money)
        {
            var historyTransfer = new HistoryTransfer();
            historyTransfer.SenderId = _Id;
            historyTransfer.DateTime = DateTime.Now;
            historyTransfer.Type = (CurrencyType)debit;
            historyTransfer.MoneyTransfer = money;
            historyTransfer.RecipientId = _Id;
            _db.HistoryTransfers.Add(historyTransfer);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ExchangeError(string message, bool flag)
        {
            DialogResult result = MessageBox.Show(
            message,
            "Ошибка!",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.Yes)
            {
                if (flag == true)
                {
                    var creatingAccount = new CreatingAccount(_db, _Id);
                    creatingAccount.Show();
                    this.Close();
                }
                else
                {
                    var addMoney = new AddMoney(_db, _Id);
                    addMoney.Show();
                    this.Close();
                }
            }
        }
    }
}

