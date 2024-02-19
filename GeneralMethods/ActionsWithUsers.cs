using FinancialApp.DataBase;
using FinancialApp.Enum;
using System.Text;

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
        public static void UserActions(DB db, Person person, AdministratorActionsWithUser userActions, Guid id)
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
                    SaveHistoryActionWithUser(db, person.Id, userActions, id);
                    MessageBox.Show("Пользователь успешно разбанен");
                    db.SaveDB();
                    break;
                case AdministratorActionsWithUser.banUser:
                    person.IsBanned = true;
                    SaveHistoryActionWithUser(db, person.Id, userActions, id);
                    MessageBox.Show("Пользователь успешно забанен");
                    db.SaveDB();
                    break;
                case AdministratorActionsWithUser.deleteUser:
                    db.Persons.Remove(person);
                    SaveHistoryActionWithUser(db, person.Id, userActions, id);
                    MessageBox.Show("Пользователь успешно удален");
                    db.SaveDB();
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
        public static void SaveHistoryActionWithUser(DB db, Guid id, AdministratorActionsWithUser userActions, Guid _id)
        {
            var historyActionWithUser = new HistoryActionsWithUser
            {
                IdAdministrator = _id,
                IdPerson = id,
                TypeActionsWithUser = userActions,
                DateTime = DateTime.Now
            };

            db.HistoryActionsWithUsers.Add(historyActionWithUser);
        }

        /// <summary>
        /// Создание нового администратора
        /// </summary>
        /// <param name="db"></param>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        public static void CreatingAdministrator(DB db, string name, string surname)
        {
            var admin = new Admin
            {
                Login = CreatingLogin(),
                Password = СreatePassword(),
                Id = Guid.NewGuid(),
                Name = name,
                Surname = surname
            };

            db.Admins.Add(admin);

            MessageBox.Show("Поздравляем! Вы успешно прошли регистрацию");
            Thread.Sleep(50);

            db.SaveDB();
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

        public static void CreatingAdministratorFormUsers(DB db, string name, string surName)
        {
            var user = db.Persons.First(u => u.Name == name && u.Surname == surName);
            var admin = new Admin
            {
                Login = user.Login,
                Password = user.Password,
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname
            };

            db.Admins.Add(admin);

            MessageBox.Show("Поздравляем! Вы успешно добавили администратора");
            Thread.Sleep(50);

            db.SaveDB();
        }
    }
}