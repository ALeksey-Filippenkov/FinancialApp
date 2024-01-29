using FinancialApp.DataBase;

namespace FinancialApp.GeneralMethods
{
    public static class EventSearch
    {
        public static List<HistoryTransfer> GetEventSearch(DB _db, List<HistoryTransfer> operationSeach, DateTime startingDateSeach, DateTime endDateSeach, string currencyTypeValue, string personRecipientName)
        {
            var seachPersonRecipientName = _db.Persons.FirstOrDefault(p => p.Name == personRecipientName);     
  
            List<HistoryTransfer> result;

            if (startingDateSeach == endDateSeach)
            {
                if (currencyTypeValue != string.Empty && personRecipientName != string.Empty)
                {
                    result = operationSeach.Where(s => s.DateTime.Date == startingDateSeach && s.Type.ToString() == currencyTypeValue
                                                                                            && s.RecipientId == seachPersonRecipientName.Id).ToList();
                }
                else if (currencyTypeValue != string.Empty && personRecipientName == string.Empty)
                {
                    result = operationSeach.Where(s => s.DateTime.Date == startingDateSeach && s.Type.ToString() == currencyTypeValue).ToList();
                }
                else if (currencyTypeValue == string.Empty && personRecipientName != string.Empty)
                {
                    if (seachPersonRecipientName != null)
                    {
                        result = operationSeach.Where(s => s.DateTime.Date == startingDateSeach && s.RecipientId == seachPersonRecipientName.Id).ToList();
                    }
                    else
                    {
                        result = null;
                    }                   
                }
                else
                {
                    result = operationSeach.Where(s => s.DateTime.Date == startingDateSeach).ToList();
                }
            }
            else
            {
                if (currencyTypeValue != string.Empty && personRecipientName != string.Empty)
                {
                    result = operationSeach.Where(s => s.DateTime.Date >= startingDateSeach && s.DateTime <= endDateSeach
                                                                                            && s.Type.ToString() == currencyTypeValue
                                                                                            && s.RecipientId == seachPersonRecipientName.Id).ToList();
                }
                else if (currencyTypeValue != string.Empty && personRecipientName == string.Empty)
                {
                    result = operationSeach.Where(s => s.DateTime.Date >= startingDateSeach && s.DateTime <= endDateSeach
                                                                                            && s.Type.ToString() == currencyTypeValue).ToList();
                }
                else if (currencyTypeValue == string.Empty && personRecipientName != string.Empty)
                {
                    if (seachPersonRecipientName != null)
                    {
                        result = operationSeach.Where(s => s.DateTime.Date >= startingDateSeach && s.DateTime <= endDateSeach
                                                                                                && s.RecipientId == seachPersonRecipientName.Id).ToList();
                    }
                    else 
                    {
                        result = null;
                    }
                }
                else
                {
                    result = operationSeach.Where(s => s.DateTime.Date >= startingDateSeach && s.DateTime <= endDateSeach).ToList();
                }
            }
            return result;
        }
    }
}
