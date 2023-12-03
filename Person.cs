using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FinancialApp
{
    public class Person
    {
        public Guid PersonId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public string City { get; set; }

        public string Adress { get; set; }

        public int PhoneNumber { get; set; }

        public string EmailAdress { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

    }
}
