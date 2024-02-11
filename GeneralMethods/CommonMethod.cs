using FinancialApp.DataBase;
using FinancialApp.Enum;

namespace FinancialApp.GeneralMethods
{
    public static class CommonMethod
    {
        public static PersonMoney GetSearchAccountOwner(DB db, int index, Guid Id)
        {
            return  db.Money.FirstOrDefault(m => m.Type == (CurrencyType)index && m.PersonId == Id);
        }

        public static List<HistoryTransfer> GetHistoryTransfer(DB db, Guid id)
        {
            return db.HistoryTransfers.Where(h => h.SenderId == id || h.RecipientId == id).ToList();
        }
    }
}
