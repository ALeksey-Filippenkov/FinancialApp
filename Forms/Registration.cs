using FinancialApp.DataBase;

namespace FinancialApp
{
    public partial class Registration : Form
    {
        private DB _db;

        public Registration(DB db)
        {
            InitializeComponent();
            _db = db;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            CheckingTheEnteredData();
        }

        public void CheckingTheEnteredData()
        {
            if (ageInput.Text == string.Empty || phoneInput.Text == string.Empty)
            {
                MessageBox.Show("Поле обязательно для заполнения");
                return;
            }
            else
            {
                var ageValue = int.TryParse(ageInput.Text, out int ageInt);
                if (ageValue == false)
                {
                    MessageBox.Show("Возраст должен быть числом");
                    return;
                }
                else
                {
                    if (ageInt <= 0)
                    {
                        MessageBox.Show("Возраст не может быть меньше 0");
                        return;
                    }
                    else if (ageInt > 200)
                    {
                        MessageBox.Show("Возраст не может быть больше 200 лет");
                        return;
                    }
                }

                var phoneNumberValue = int.TryParse(phoneInput.Text, out int phoneNumberInt);
                if (phoneNumberValue == false)
                {
                    MessageBox.Show("Номер телефона должен быть в виде числа");
                    return;
                }
                AddPerson(phoneNumberInt);
            }
        }

        public void AddPerson(int phoneNumberInt)
        {
            var person = new Person();
            var personId = Guid.NewGuid();

            person.Name = name.Text;
            person.Surname = surnameInput.Text;
            person.City = cityInput.Text;
            person.Adress = adressInput.Text;
            person.PhoneNumber = phoneNumberInt;
            person.EmailAdress = emailInput.Text;

            if (_db.Persons.Count != 0)
            {
                foreach (var item in _db.Persons)
                {
                    if (item.Login.Equals(loginInput.Text))
                    {
                        MessageBox.Show("Данный логин уже используется\nПридумайте другой.");
                        return;
                    }
                    else
                        person.Login = loginInput.Text;
                }
            }
            else
            {
                person.Login = loginInput.Text;
            }

            person.Password = passwordInput.Text;
            person.Id = personId;
            person.IsBanned = false;
            _db.Persons.Add(person);

            MessageBox.Show("Поздравляем! Вы успешно прошли регистрацию");
            Thread.Sleep(50);
            _db.SaveDB();
            Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
