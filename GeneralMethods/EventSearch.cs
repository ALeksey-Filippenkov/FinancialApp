using FinancialApp.DataBase;

namespace FinancialApp.GeneralMethods
{
    public static class EventSearch
    {
        public static List<HistoryTransfer> GetEventSearch(DB _db, Guid id, DateTime startingDateSeach, DateTime endDateSeach, string currencyTypeValue, string personRecipientName)
       {
            var operationSeach = _db.HistoryTransfers.Where(h => h.SenderId == id || h.RecipientId == id).ToList();
            var personRecipientNameSeach = _db.Persons.FirstOrDefault(p => p.Name == personRecipientName);

            List<HistoryTransfer> result;

            if (startingDateSeach == endDateSeach)
            {
                if (currencyTypeValue != string.Empty && personRecipientName != string.Empty)
                {
                    result = operationSeach.Where(s => s.DateTime == startingDateSeach && s.Type.ToString() == currencyTypeValue
                }
                else if (currencyTypeValue != string.Empty && personRecipientName == string.Empty)
                {
                    result = operationSeach.Where(s =>s.DateTime == startingDateSeach  && s.Type.ToString() == currencyTypeValue).ToList();
                }
                else if (currencyTypeValue == string.Empty && personRecipientName != string.Empty)
                {
                    result = operationSeach.Where(s => s.DateTime == startingDateSeach && s.RecipientId == personRecipientNameSeach.Id).ToList();
                }
                else
                {
                    result = operationSeach.Where(s => s.DateTime == startingDateSeach).ToList();
                }
            }
            else
            {
                if (currencyTypeValue != string.Empty && personRecipientName != string.Empty)
                {
                    result = operationSeach.Where(s => s.DateTime >= startingDateSeach && s.DateTime <= endDateSeach
                                                                                       && s.Type.ToString() == currencyTypeValue
                                                                                       && s.RecipientId == personRecipientNameSeach.Id).ToList();
                }
                else if (currencyTypeValue != string.Empty && personRecipientName == string.Empty)
                {
                    result = operationSeach.Where(s => s.DateTime >= startingDateSeach && s.DateTime <= endDateSeach
                                                                                       && s.Type.ToString() == currencyTypeValue).ToList();
                }
                else if (currencyTypeValue == string.Empty && personRecipientName != string.Empty)
                {
                   result = operationSeach.Where(s => s.DateTime >= startingDateSeach && s.DateTime <= endDateSeach
                                                                                      && s.RecipientId == personRecipientNameSeach.Id).ToList();
                }
                else
                {
                    result = operationSeach.Where(s => s.DateTime >= startingDateSeach && s.DateTime <= endDateSeach).ToList();
                }
            }
            return result;
        }
    }
}
