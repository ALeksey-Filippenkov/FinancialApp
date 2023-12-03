using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinancialApp
{
    public class DB
    {
        public DB()
        {
            Persons = new List<Person>();

            Money = new List<PersonMoney>();

            HistoryTransfers = new List<HistoryTransfer>();
        }

        public List<Person> Persons { get; set; }

        public List<PersonMoney> Money { get; set; }

        public List<HistoryTransfer> HistoryTransfers { get; set; }

        public void SaveDB()
        {
            File.WriteAllText("G:\\DB", JsonSerializer.Serialize(this));
        }

        public DB ReadDB()
        {
            var readDB = JsonSerializer.Deserialize<DB>(File.ReadAllText("G:\\DB"));
            return readDB;
        }
    }
}

