using FinancialApp.DataBase;
using FinancialApp.GeneralMethods;

namespace FinancialApp.Forms
{
    public partial class EnterForm : Form
    {
        private readonly Form _form;
        private readonly DB _db;
        private readonly Guid _id;
        private FormData _formDataData;

        public EnterForm(Guid id, Form form, DB db)
        {
            InitializeComponent();
            _id = id;
            _form = form;
            _db = db;
            _formDataData = new FormData
            {
                HistoryOperationDataGridView = historyOperationDataGridView,
                RubLebel = rubLebel,
                UsdLebel = usdLebel,
                EurLebel = eurLebel
            };
        }

        private void EnterForm_Load(object sender, EventArgs e)
        {
            var person = _db.Persons.First(p => p.Id == _id);
            name.Text = person.Name;
            surname.Text = person.Surname;
            age.Text = person.Age.ToString();
            city.Text = person.City;
            adress.Text = person.Adress;
            phone.Text = person.PhoneNumber.ToString();
            email.Text = person.EmailAdress;
            login.Text = person.Login;
            password.Text = person.Password;
            CommonMethod.GetHistoryTransfer(_db, _id);
            RefreshDataGridView();
        }

        private void SaveLKButton_Click(object sender, EventArgs e)
        {
            if (age.Text == null || phone.Text == null)
            {
                MessageBox.Show("Поле обязательно для заполнения");
                return;
            }
            else
            {
                var ageValue = int.TryParse(age.Text, out var ageInt);
                if (ageValue == false)
                {
                    MessageBox.Show("Возраст должен быть числом");
                    return;
                }
                var phoneNumberValue = int.TryParse(phone.Text, out var phoneNumberInt);
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

        private void ExitButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Вы действительно хотите выйти?",
                "Подтверждение выхода",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            if (result != DialogResult.Yes) return;
            _form.Show();
            Close();
        }

        private void TabPage2_Click(object sender, EventArgs e)
        {
            EnterLabelText();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var addMoney = new AddMoney(_db, _id, _formDataData);
            addMoney.Show();
        }

        private void CreateAccount_Click(object sender, EventArgs e)
        {
            var creatingAccount = new CreatingAccount(_db, _id);
            creatingAccount.Show();
        }
        
        private void EnterLabelText()
        {
            RebootLabelText.LabelText(_db, _id, _formDataData);
        }

        private void MoneyTransferButton_Click(object sender, EventArgs e)
        {
            var moneyTransfer = new MoneyTransfer(_db, _id, _formDataData);
            moneyTransfer.Show();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            var operationSearch = new OperationSearch(_db, _id);
            operationSearch.Show();
        }

        private void HistoryOperationExcel_Click(object sender, EventArgs e)
        {
            var newExcel = new DataOutputInExcel(_db);
            newExcel.GetDataOutputInExcel(CommonMethod.GetHistoryTransfer(_db, _id));
        }

        private void TabPage3_Click(object sender, EventArgs e)
        {
            CommonMethod.GetHistoryTransfer(_db, _id);
            EnterLabelText();
            RefreshDataGridView();
            var panel = new Panel
            {
                Location = new Point(30, 30),
                Visible = true
            };
            tabPage3.Controls.Add(panel);
        }

        private void RefreshDataGridView()
        {
            PrintHistory.GetPrintHistory(_db, CommonMethod.GetHistoryTransfer(_db, _id), _formDataData);
        }

    }
}