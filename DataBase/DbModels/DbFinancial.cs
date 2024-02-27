using Microsoft.EntityFrameworkCore;

namespace FinancialApp.DataBase.DbModels
{
    public class DbFinancial : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DbFinancialApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        public virtual DbSet<DbPerson> Persons { get; set; }

        public virtual DbSet<DbPersonMoney> Money { get; set; }

        public virtual DbSet<DbHistoryTransfer> HistoryTransfers { get; set; }

        public virtual DbSet<DbAdmin> Admins { get; set; }

        public virtual DbSet<DbHistoryActionsWithUser> HistoryActionsWithUsers { get; set; }
    }
}
