using System.ComponentModel.DataAnnotations;

namespace FinancialApp.DataBase.DbModels
{
    /// <summary>
    /// Данные администратора
    /// </summary>
    public class DbAdmin
    {
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; } = "Супер пупер";

        public string? Surname { get; set; } = "Администратор!";

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
