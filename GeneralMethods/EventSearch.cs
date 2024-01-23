using FinancialApp.DataBase;

namespace FinancialApp.GeneralMethods
{
    public static class EventSearch
    {
        public static List<HistoryTransfer> GetEventSearch(DB _db, Guid id, DateOnly startingDateSeach, DateOnly endDateSeach, string currencyTypeValue, string personRecipientName)
       {
            var operationSeach = _db.HistoryTransfers.Where(h => h.SenderId == id || h.RecipientId == id).ToList();
            var personRecipientNameSeach = _db.Persons.FirstOrDefault(p => p.Name == personRecipientName);

            if (startingDateSeach == endDateSeach)
            {
                if (currencyTypeValue != string.Empty && personRecipientName != string.Empty)
                {
                    var historyOperationDay = operationSeach.Where(s => DateOnly.FromDateTime(s.DateTime) == startingDateSeach
                                                                                     && s.Type.ToString() == currencyTypeValue
                                                                                         && s.RecipientId == personRecipientNameSeach.Id).ToList();
                    return historyOperationDay;
                }
                else if (currencyTypeValue != string.Empty && personRecipientName == string.Empty)
                {
                    var historyOperationDay = operationSeach.Where(s => DateOnly.FromDateTime(s.DateTime) == startingDateSeach
                                                                                     && s.Type.ToString() == currencyTypeValue).ToList();
                    return historyOperationDay;
                }
                else if (currencyTypeValue == string.Empty && personRecipientName != string.Empty)
                {
                    var historyOperationDay = operationSeach.Where(s => DateOnly.FromDateTime(s.DateTime) == startingDateSeach
                                                                                         && s.RecipientId == personRecipientNameSeach.Id).ToList();
                    return historyOperationDay;
                }
                else
                {
                    var historyOperationDay = operationSeach.Where(s => DateOnly.FromDateTime(s.DateTime) == startingDateSeach).ToList();
                    return historyOperationDay;
                }
            }
            else
            {
                if (currencyTypeValue != string.Empty && personRecipientName != string.Empty)
                {
                    var historyOperationDay = operationSeach.Where(s => DateOnly.FromDateTime(s.DateTime) >= startingDateSeach
                                                                     && DateOnly.FromDateTime(s.DateTime) <= endDateSeach
                                                                                     && s.Type.ToString() == currencyTypeValue
                                                                                         && s.RecipientId == personRecipientNameSeach.Id).ToList();
                    return historyOperationDay;
                }
                else if (currencyTypeValue != string.Empty && personRecipientName == string.Empty)
                {

                    var historyOperationDay = operationSeach.Where(s => DateOnly.FromDateTime(s.DateTime) >= startingDateSeach
                                                                     && DateOnly.FromDateTime(s.DateTime) <= endDateSeach
                                                                                     && s.Type.ToString() == currencyTypeValue).ToList();
                    return historyOperationDay;
                }
                else if (currencyTypeValue == string.Empty && personRecipientName != string.Empty)
                {
                    var historyOperationDay = operationSeach.Where(s => DateOnly.FromDateTime(s.DateTime) >= startingDateSeach
                                                                     && DateOnly.FromDateTime(s.DateTime) <= endDateSeach
                                                                                         && s.RecipientId == personRecipientNameSeach.Id).ToList();
                    return historyOperationDay;
                }
                else
                {
                    var historyOperationDay = operationSeach.Where(s => DateOnly.FromDateTime(s.DateTime) >= startingDateSeach
                                                                     && DateOnly.FromDateTime(s.DateTime) <= endDateSeach).ToList();
                    return historyOperationDay;
                }
            }
        }
    }
}
