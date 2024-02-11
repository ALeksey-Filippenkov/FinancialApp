using FinancialApp.DataBase;

namespace FinancialApp.GeneralMethods
{
    public static class EventSearch
    {
        public static List<HistoryTransfer> GetEventSearch(DB db, List<HistoryTransfer> operationSearch, DateTime startingDateSearch, DateTime endDateSearch, string currencyTypeValue, string personRecipientName)
        {
            var searchPersonRecipientName = db.Persons.FirstOrDefault(p => p.Name == personRecipientName);

            List<HistoryTransfer>? result = null;

            if (startingDateSearch == endDateSearch)
            {
                if (currencyTypeValue != string.Empty && personRecipientName != string.Empty)
                {
                    result = operationSearch.Where(s => s.DateTime.Date == startingDateSearch && s.Type.ToString() == currencyTypeValue
                                                                                            && s.RecipientId == searchPersonRecipientName.Id).ToList();
                }
                else if (currencyTypeValue != string.Empty && personRecipientName == string.Empty)
                {
                    result = operationSearch.Where(s => s.DateTime.Date == startingDateSearch && s.Type.ToString() == currencyTypeValue).ToList();
                }
                else if (currencyTypeValue == string.Empty && personRecipientName != string.Empty)
                {
                    if (searchPersonRecipientName != null)
                    {
                        result = operationSearch.Where(s => s.DateTime.Date == startingDateSearch && s.RecipientId == searchPersonRecipientName.Id).ToList();
                    }                 
                }
                else
                {
                    result = operationSearch.Where(s => s.DateTime.Date == startingDateSearch).ToList();
                }
            }
            else
            {
                if (currencyTypeValue != string.Empty && personRecipientName != string.Empty)
                {
                    result = operationSearch.Where(s => s.DateTime.Date >= startingDateSearch && s.DateTime <= endDateSearch
                                                                                            && s.Type.ToString() == currencyTypeValue
                                                                                            && s.RecipientId == searchPersonRecipientName.Id).ToList();
                }
                else if (currencyTypeValue != string.Empty && personRecipientName == string.Empty)
                {
                    result = operationSearch.Where(s => s.DateTime.Date >= startingDateSearch && s.DateTime <= endDateSearch
                                                                                            && s.Type.ToString() == currencyTypeValue).ToList();
                }
                else if (currencyTypeValue == string.Empty && personRecipientName != string.Empty)
                {
                    if (searchPersonRecipientName != null)
                    {
                        result = operationSearch.Where(s => s.DateTime.Date >= startingDateSearch && s.DateTime <= endDateSearch
                                                                                                && s.RecipientId == searchPersonRecipientName.Id).ToList();
                    }
                }
                else
                {
                    result = operationSearch.Where(s => s.DateTime.Date >= startingDateSearch && s.DateTime <= endDateSearch).ToList();
                }
            }
            return result;
        }
    }
}
