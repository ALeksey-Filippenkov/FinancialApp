using FinancialApp.DataBase;

namespace FinancialApp.Forms
{
    public partial class ShowUserInformation : Form
    {
        private DB _db;

        public ShowUserInformation(DB _db)
        {
            InitializeComponent();
            _db = _db;
        }

        private void seachButton_Click(object sender, EventArgs e)
        {
            var person = _db.Persons.Where(p => p.Name == nameTextBox.Text).ToList();
            if (person == null) 
            {
                MessageBox.Show("Пользователь с таким именем не найден");
                return;
            }

            var personData = GeneralMethods.PersonlaData.GetUserData();

            for (int i = 0; i < person.Count; i++)
            {
                for (int j = 0, k = 1; j < personData.Count; j++, k++)
                {
                    userInformationDataGridView.Rows[j].Cells[k].Value = person[j];
                }
            }
        }
    }
}
