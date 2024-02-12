namespace FinancialApp.Enum
{
    public enum AdministratorActionsWithUser
    {
        banUser = 0,
        unbanUser = 1,
        deleteUser = 2
    }

    public static class ActionsWithUser
    {
        public static string GetAdministratorActionsWithUser(AdministratorActionsWithUser type)
        {
            return type switch
            {
                AdministratorActionsWithUser.banUser => "Забанен",
                AdministratorActionsWithUser.unbanUser => "Разбанен",
                AdministratorActionsWithUser.deleteUser => "Пользователь удален",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}
