using FinancialApp.DataBase;
using FinancialApp.GeneralMethods;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace FinancialApp.Forms
{
    public partial class ShowUserInformation : Form
    {
        private DB _db;

        public ShowUserInformation(DB db)
        {
            InitializeComponent();
            _db = db;
        }

        private void seachButton_Click(object sender, EventArgs e)
        {
            userInformationDataGridView.Visible = true;

            var person = _db.Persons.Where(p => p.Name == nameTextBox.Text).ToList();
            if (person == null)
            {
                MessageBox.Show("Пользователь с таким именем не найден");
                return;
            }

            userInformationDataGridView.Rows.Clear();

            var personData = PersonalData.GetUserData();

            if (person.Count == 0)
            {
                PrintUsers(personData, _db.Persons, userInformationDataGridView);
            }
            else
            {
                PrintUsers(personData, person, userInformationDataGridView);
            }
        }
        public void PrintUsers(List<string> personData, List<Person> users, DataGridView userInformationDataGridView)
        {
            var index = personData.Count;
            var rows = users.Count * index;

            userInformationDataGridView.Rows.Add(rows);

            int r = 0;

            for (int i = 0, j = 0; i < users.Count; i++)
            {
                userInformationDataGridView.Rows[j].Cells[0].Value = $"Пользователь #{i + 1}";

                for (int k = 0; k < index; k++)
                {
                    userInformationDataGridView.Rows[r].Cells[1].Value = personData[k];
                    r++;
                }

                j += index;
            }

            int l = 0;

            foreach (var item in users)
            {
                userInformationDataGridView.Rows[0 + l].Cells[2].Value = item.Name;
                userInformationDataGridView.Rows[1 + l].Cells[2].Value = item.Surname;
                userInformationDataGridView.Rows[2 + l].Cells[2].Value = item.Age;
                userInformationDataGridView.Rows[3 + l].Cells[2].Value = item.City;
                userInformationDataGridView.Rows[4 + l].Cells[2].Value = item.Adress;
                userInformationDataGridView.Rows[5 + l].Cells[2].Value = item.PhoneNumber;
                userInformationDataGridView.Rows[6 + l].Cells[2].Value = item.EmailAdress;
                userInformationDataGridView.Rows[7 + l].Cells[2].Value = item.Login;
                userInformationDataGridView.Rows[8 + l].Cells[2].Value = item.Password;
                userInformationDataGridView.Rows[9 + l].Cells[2].Value = item.Id;
                userInformationDataGridView.Rows[10 + l].Cells[2].Value = item.IsBanned.ToString();
                l += index;
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
