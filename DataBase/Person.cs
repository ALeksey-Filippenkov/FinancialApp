﻿namespace FinancialApp.DataBase
{
    public class Person
    { 
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public string City { get; set; }

        public string Adress { get; set; }

        public int PhoneNumber { get; set; }

        public string EmailAdress { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public bool IsBanned { get; set; }        
    }
}