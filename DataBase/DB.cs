﻿using System.Text.Json;

namespace FinancialApp.DataBase
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