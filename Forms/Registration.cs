using FinancialApp.DataBase;

namespace FinancialApp.Forms
{
    public partial class Registration : Form
    {
        private readonly DB _db;

        public Registration(DB db)
        {
            InitializeComponent();
            _db = db;
        }

        private void AddButton_Click(object sender, EventArgs e)
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

                switch (ageInt)
                {
                    case <= 0:
                        MessageBox.Show("Возраст не может быть меньше 0");
                        return;
                    case > 200:
                        MessageBox.Show("Возраст не может быть больше 200 лет");
                        return;
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
            
            var personId = Guid.NewGuid();
            var person = new Person
            {
                Name = name.Text,
                Surname = surnameInput.Text,
                City = cityInput.Text,
                Adress = adressInput.Text,
                PhoneNumber = phoneNumberInt,
                EmailAdress = emailInput.Text
            };

            if (_db.Persons.Count != 0)
            {
                foreach (var item in _db.Persons)
                {
                    if (item.Login.Equals(loginInput.Text))
                    {
                        MessageBox.Show("Данный логин уже используется\nПридумайте другой.");
                        return;
                    }
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

        private void BackButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
