using FinancialApp.DataBase;
using FinancialApp.Enum;
using System.Text;
using FinancialApp.DataBase.DbModels;

namespace FinancialApp.GeneralMethods
{
    public static class ActionsWithUsers
    {
        /// <summary>
        /// Действия с пользовотелем
        /// </summary>
        /// <param name="db"></param>
        /// <param name="person"></param>
        /// <param name="userActions"></param>
        /// <param name="id"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void UserActions(DB db, DbPerson person, AdministratorActionsWithUser userActions, Guid adminId, DbFinancial context)
        {
            if (person == null)
            {
                MessageBox.Show("Пользователя с таким именем нет");
                return;
            }

            switch (userActions)
            {
                case AdministratorActionsWithUser.unbanUser:
                    person.IsBanned = false;
                    SaveHistoryActionWithUser(db, person.Id, userActions, adminId, context);
                    MessageBox.Show("Пользователь успешно разбанен");
                    context.SaveChanges();
                    break;
                case AdministratorActionsWithUser.banUser:
                    person.IsBanned = true;
                    SaveHistoryActionWithUser(db, person.Id, userActions, adminId, context);
                    MessageBox.Show("Пользователь успешно забанен");
                    context.SaveChanges();
                    break;
                case AdministratorActionsWithUser.deleteUser:
                    context.Persons.Remove(person);
                    SaveHistoryActionWithUser(db, person.Id, userActions, adminId, context);
                    MessageBox.Show("Пользователь успешно удален");
                    context.SaveChanges();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(userActions), userActions, null);
            }
        }

        /// <summary>
        /// Сохранение истории действий администратора с пользователем
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <param name="userActions"></param>
        /// <param name="_id"></param>
        public static void SaveHistoryActionWithUser(DB db, Guid personId, AdministratorActionsWithUser userActions, Guid adminId, DbFinancial context)
        {
            var historyActionWithUser = new DbHistoryActionsWithUser
            {
                Id = Guid.NewGuid(),
                IdAdministrator = adminId,
                IdPerson = personId,
                TypeActionsWithUser = userActions,
                DateTime = DateTime.Now
            };

            context.HistoryActionsWithUsers.Add(historyActionWithUser);
        }

        /// <summary>
        /// Создание нового администратора
        /// </summary>
        /// <param name="db"></param>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        public static void CreatingAdministrator(DB db, string name, string surname, DbFinancial context)
        {
            var admin = new DbAdmin
            {
                Login = CreatingLogin(),
                Password = СreatePassword(),
                Id = Guid.NewGuid(),
                Name = name,
                Surname = surname
            };

            context.Admins.Add(admin);

            MessageBox.Show("Поздравляем! Вы успешно добавили администратора");
            Thread.Sleep(50);

            context.SaveChanges();
        }

        /// <summary>
        /// Генерация логина для администратора
        /// </summary>
        /// <returns></returns>
        private static string CreatingLogin()
        {
            const int nameLen = 6;
            var vowels = "aeuoyi".ToCharArray();
            var consonants = "qwrtpsdfghjklzxcvbnm".ToCharArray();

            var rand = new Random();

            var newNick = new StringBuilder();

            while (newNick.Length < nameLen)
            {
                var firstVowel = rand.Next(0, 2) == 0;

                if (firstVowel)
                {
                    newNick.Append(vowels[rand.Next(0, vowels.Length)]);
                    newNick.Append(consonants[rand.Next(0, consonants.Length)]);
                }
                else
                {
                    newNick.Append(consonants[rand.Next(0, consonants.Length)]);
                    newNick.Append(vowels[rand.Next(0, vowels.Length)]);
                }
            }

            newNick[0] = char.ToUpper(newNick[0]);
            return newNick.ToString();
        }

        /// <summary>
        /// Генерация пароля для администратора
        /// </summary>
        /// <returns></returns>
        private static string СreatePassword()
        {
            var length = 8;
            const string valid = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var res = new StringBuilder();
            var rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public static void CreatingAdministratorFormUsers(DB db, string name, string surName, DbFinancial context)
        {
            var user = context.Persons.First(u => u.Name == name && u.Surname == surName);
            var admin = new DbAdmin
            {
                Login = user.Login,
                Password = user.Password,
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname
            };

            context.Admins.Add(admin);

            MessageBox.Show("Поздравляем! Вы успешно добавили администратора");
            Thread.Sleep(50);

            context.SaveChanges();
        }
    }
}