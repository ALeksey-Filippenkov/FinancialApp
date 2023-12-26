using FinancialApp.DataBase;
using FinancialApp.Enum;

namespace FinancialApp.GeneralMethods
{
    public static class CommonMethod
    {
        public static PersonMoney GetSearchAccountOwner(DB _db, int index, Guid _Id)
        {
            return  _db.Money.FirstOrDefault(m => m.Type == (CurrencyType)index && m.PersonId == _Id);
        }
    }
}
