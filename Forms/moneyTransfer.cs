using FinancialApp.DataBase;
using FinancialApp.Enum;
using FinancialApp.GeneralMethods;
using System.Net;
using System.Xml.Linq;

namespace FinancialApp.Forms
{
    public partial class MoneyTransfer : Form
    {
        private readonly DB _db;
        private readonly Guid _id;
        private readonly FormData _formData;
        private int _phoneNumberTransfer;
        private double _moneyTransfer;
        private double _usd;
        private double _eur;

        public MoneyTransfer(DB db, Guid id, FormData formData)
        {
            InitializeComponent();
            _db = db;
            _id = id;
            _formData = formData;
            ViewRichTextBoxOutData();
            linkLabel1.LinkClicked += LinkLabel1_LinkClicked;
        }

        private void TransferButton_Click(object sender, EventArgs e)
        {
            CheckingTheEnteredData(phoneNumerTransferInput.Text, moneyTransferInput.Text);

            CheckingThePossibilityOfTranslation();
        }

        public void CheckingTheEnteredData(string phoneNumber, string moneyTransfer)
        {
            var phoneNumberTransferValue = int.TryParse(phoneNumber, out var phoneNumberTransferInt);
            if (phoneNumerTransferInput.Text == string.Empty)
            {
                MessageBox.Show("Необходимо указать номер телефона получателя в виде числа.");
                return;
            }

            if (!phoneNumberTransferValue)
            {
                MessageBox.Show("Вы ввели не номер телефона.");
                return;
            }

            _phoneNumberTransfer = phoneNumberTransferInt;

            if (currencyList.SelectedIndex == -1)
            {
                MessageBox.Show($"Вы не выбрали валюту для {TypeOperation.GetTypeOfOperation(TypeOfOperation.moneyTransfer).ToLower()}а");
                return;
            }

            var moneyTransferValue = int.TryParse(moneyTransfer, out var moneyTransferDouble);
            if (moneyTransferInput.Text == string.Empty)
            {
                MessageBox.Show($"Вы не ввели сумму для {TypeOperation.GetTypeOfOperation(TypeOfOperation.moneyTransfer).ToLower()}а");
                return;
            }

            if (!moneyTransferValue)
            {
                MessageBox.Show("Деньги могут быть только ввиде чисел");
                return;
            }

            if (moneyTransferDouble < 0)
            {
                MessageBox.Show("Внесение денег не может быть меньше 0");
                return;
            }

            _moneyTransfer = moneyTransferDouble;
        }

        public void CheckingThePossibilityOfTranslation()
        {
            var personSender = CommonMethod.GetSearchAccountOwner(_db, currencyList.SelectedIndex, _id);
            var personRecipient = _db.Persons.FirstOrDefault(p => p.PhoneNumber == _phoneNumberTransfer);
            if (personRecipient == null)
            {
                MessageBox.Show("Пользователь с таким номером телефона не найден");
                return;
            }

            if (personSender == null)
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

            if (personSender.Balance - _moneyTransfer < 0)
            {
                MessageBox.Show("Недостаточно денег на счету");
                return;
            }

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
            var historyTransfer = new HistoryTransfer
            {
                DateTime = DateTime.Now,
                SenderId = _id,
                Type = (CurrencyType)currencyList.SelectedIndex,
                RecipientId = personRecipient.Id,
                MoneyTransfer = _moneyTransfer,
                OperationType = TypeOfOperation.moneyTransfer
            };
            _db.HistoryTransfers.Add(historyTransfer);

            PrintHistory.GetPrintHistory(_db, CommonMethod.GetHistoryTransfer(_db, _id), _formData);
            RebootLabelText.LabelText(_db, _id, _formData);
        }

        private void ViewRichTextBoxOutData()
        {
            var client = new WebClient();
            var xml = client.DownloadString("https://www.cbr-xml-daily.ru/daily.xml");
            var xdoc = XDocument.Parse(xml);
            var el = xdoc.Element("ValCurs").Elements("Valute");

            var dollarS = el.Where(x => x.Attribute("ID").Value == "R01235").Select(x => x.Element("Value").Value).FirstOrDefault();
            var dollar = new System.Globalization.CultureInfo("ru-Ru");
            _usd = Convert.ToDouble(dollarS, dollar);

            var eurS = el.Where(x => x.Attribute("ID").Value == "R01239").Select(x => x.Element("Value").Value).FirstOrDefault();
            var euro = new System.Globalization.CultureInfo("ru-Ru");
            _eur = Convert.ToDouble(eurS, euro);

            label7.Text = $"Курс основных валют\nЕвро: {_eur} Доллар: {_usd}";
        }

        private void ExchangeButton_Click(object sender, EventArgs e)
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

            var moneyValue = double.TryParse(moneyTextBox.Text, out var money);
            if (!moneyValue)
            {
                MessageBox.Show("Вы ввели не число.");
                return;
            }

            var debitAccount = CommonMethod.GetSearchAccountOwner(_db, debit, _id);
            var replenishmentAccount = CommonMethod.GetSearchAccountOwner(_db, replenishment, _id);
            string message;
            var flag = true;
            if (debitAccount == null)
            {
                message = $"У вас нет счета с которого вы хотите произвести {TypeOperation.GetTypeOfOperation(TypeOfOperation.exchange)}.\nВы хотите создать счет?";
                ExchangeError(message, flag);
                return;
            }

            if (replenishmentAccount == null)
            {
                message = $"У вас нет счета с которого вы хотите произвести {TypeOperation.GetTypeOfOperation(TypeOfOperation.exchange)}.\nВы хотите создать счет?";
                ExchangeError(message, flag);
                return;
            }

            if (debitAccount.Balance == 0 || debitAccount.Balance - money <= 0)
            {
                flag = false;
                message =
                    $"У вас недостаточно срадств на счету с которого вы хотите произвести {TypeOperation.GetTypeOfOperation(TypeOfOperation.exchange)}!\n Хотите пополнить счет?";
                ExchangeError(message, flag);
                return;
            }

            debitAccount.Balance -= money;
            if (debit == replenishment)
            {
                MessageBox.Show("Для попоплнения счета перейдите в соответствующий раздел");
                return;
            }

            switch ((CurrencyType)debit)
            {
                case CurrencyType.RUB when replenishmentAccount.Type == CurrencyType.USD:
                    replenishmentAccount.Balance += Math.Round(money / _usd, 2);
                    break;
                case CurrencyType.RUB when replenishmentAccount.Type == CurrencyType.EUR:
                    replenishmentAccount.Balance += Math.Round(money / _eur, 2);
                    break;
                case CurrencyType.USD when replenishmentAccount.Type == CurrencyType.RUB:
                    replenishmentAccount.Balance += Math.Round(money * _usd, 2);
                    break;
                case CurrencyType.USD when replenishmentAccount.Type == CurrencyType.EUR:
                    replenishmentAccount.Balance += Math.Round(money / _usd, 2);
                    break;
                case CurrencyType.EUR when replenishmentAccount.Type == CurrencyType.RUB:
                    replenishmentAccount.Balance += Math.Round(money * _eur, 2);
                    break;
                case CurrencyType.EUR when replenishmentAccount.Type == CurrencyType.USD:
                    replenishmentAccount.Balance += Math.Round(money / _usd, 2);
                    break;
            }
            SaveMoneyExchangeHistory(debit, money);
            
            Close();
        }

        private void SaveMoneyExchangeHistory(int debit, double money)
        {
            MessageBox.Show($"Поздравляем! Вы успешно {TypeOperation.GetTypeOfOperation(TypeOfOperation.exchange)} деньги");
            var historyTransfer = new HistoryTransfer
            {
                SenderId = _id,
                DateTime = DateTime.Now,
                Type = (CurrencyType)debit,
                MoneyTransfer = money,
                RecipientId = _id,
                OperationType = TypeOfOperation.exchange
            };
            _db.HistoryTransfers.Add(historyTransfer);

            _db.SaveDB();

            PrintHistory.GetPrintHistory(_db, CommonMethod.GetHistoryTransfer(_db, _id), _formData);
            RebootLabelText.LabelText(_db, _id, _formData);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ExchangeError(string message, bool flag)
        {
            var result = MessageBox.Show(
            message,
            "Ошибка!",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);

            if (result != DialogResult.Yes) return;
            if (flag)
            {
                var creatingAccount = new CreatingAccount(_db, _id);
                creatingAccount.Show();
                Close();
            }
            else
            {
                var addMoney = new AddMoney(_db, _id, _formData);
                addMoney.Show();
                Close();
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("https://cbr.ru");
        }
    }
}