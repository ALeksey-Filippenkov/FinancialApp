﻿namespace FinancialApp.GeneralMethods
{
    public static class PersonalData
    {
        public static List<string> GetUserData()
        { 
            var userData = new List<string>() { "Имя", "Фамилия", "Возраст", "Город", "Адрес", "Телефон", "Электронная почта", "Логин", "Пароль", "Id", "Бан" };
            return userData;
        }
    }
}
