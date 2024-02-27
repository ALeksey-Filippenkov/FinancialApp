using FinancialApp.DataBase;
using FinancialApp.DataBase.DbModels;

namespace FinancialApp.Forms
{
    public partial class Registration : Form
    {
        private readonly DB _db;
        private readonly DbFinancial _context;

        public Registration(DB db, DbFinancial context)
        {
            InitializeComponent();
            _db = db;
            _context = context;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            CheckingTheEnteredData();
        }

        /// <summary>
        /// Проверка вводимых пользователем данных
        /// </summary>
        public void CheckingTheEnteredData()
        {
            if (ageInput.Text == string.Empty || phoneInput.Text == string.Empty)
            {
                MessageBox.Show("Поле обязательно для заполнения");
                return;
            }
            else
            {
                var ageValue = int.TryParse(ageInput.Text, out var ageInt);
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

                var phoneNumberValue = int.TryParse(phoneInput.Text, out var phoneNumberInt);
                if (phoneNumberValue == false)
                {
                    MessageBox.Show("Номер телефона должен быть в виде числа");
                    return;
                }
                AddPerson(ageInt, phoneNumberInt);
            }
        }

        /// <summary>
        /// Добавление нового пользователя в базу данных
        /// </summary>
        /// <param name="age"></param>
        /// <param name="phoneNumberInt"></param>
        public void AddPerson(int age, int phoneNumberInt)
        {
            var person = new DbPerson
            {
                Name = name.Text,
                Surname = surnameInput.Text,
                Age = age,
                City = cityInput.Text,
                Adress = adressInput.Text,
                EmailAdress = emailInput.Text,
                Id = Guid.NewGuid(),
                Password = passwordInput.Text,
                IsBanned = false
        };
            
            if (_context.Persons.Count() != 0)
            {
                foreach (var item in _context.Persons)
                {
                    if (item.Login.Equals(loginInput.Text))
                    {
                        MessageBox.Show("Данный логин уже используется\nПридумайте другой.");
                        return;
                    }

                    if (item.PhoneNumber.Equals(phoneNumberInt))
                    {
                        MessageBox.Show("Данный телефон уже используется.");
                        return;
                    }

                    person.Login = loginInput.Text;
                    person.PhoneNumber = phoneNumberInt;
                }
            }
            else
            {
                person.Login = loginInput.Text;
            }

            _context.Persons.Add(person);

            MessageBox.Show("Поздравляем! Вы успешно прошли регистрацию");
            _context.SaveChanges();
            Close();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
