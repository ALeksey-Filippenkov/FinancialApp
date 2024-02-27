using FinancialApp.DataBase;
using FinancialApp.DataBase.DbModels;
using FinancialApp.Enum;

namespace FinancialApp.GeneralMethods
{
    public static class CommonMethod
    {
        public static DbPersonMoney GetSearchAccountOwner(DB db, int index, Guid Id, DbFinancial context)
        {
            return  context.Money.FirstOrDefault(m => m.Type == (CurrencyType)index && m.PersonId == Id);
        }

        public static List<DbHistoryTransfer> GetHistoryTransfer(DB db, Guid id, DbFinancial context)
        {
            return context.HistoryTransfers.Where(h => h.SenderId == id || h.RecipientId == id).ToList();
        }
    }
}
