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
            AddPerson();
        }

        public void AddPerson()
        {
            if (ageInput.Text == null || phoneInput.Text == null)
            {
                MessageBox.Show("Поле обязательно для заполнения");
                return;
            }
            else
            {
                var ageValue = Int32.TryParse(ageInput.Text, out int ageInt);
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

                var phoneNumberValue = Int32.TryParse(phoneInput.Text, out int phoneNumberInt);
                if (phoneNumberValue == false)
                {
                    MessageBox.Show("Номер телефона должен быть в виде числа");
                    return;
                }

                var person = new Person();
                var personId = Guid.NewGuid();

                person.Id = personId;
                person.Name = name.Text;
                person.Surname = surnameInput.Text;
                person.City = cityInput.Text;
                person.Adress = adressInput.Text;
                person.PhoneNumber = phoneNumberInt;
                person.EmailAdress = emailInput.Text;
                person.Login = loginInput.Text;
                person.Password = passwordInput.Text;

                _db.Persons.Add(person);
            }

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
