using FinancialApp.DataBase;
using FinancialApp.Enum;

namespace FinancialApp.GeneralMethods
{
    public static class RebootLabelText
    {
        public static void LabelText (DB db, Guid id, FormData form)
        {
            var rub = db.Money.FirstOrDefault(p => p.PersonId == id && p.Type == CurrencyType.RUB);
            var usd = db.Money.FirstOrDefault(p => p.PersonId == id && p.Type == CurrencyType.USD);
            var eur = db.Money.FirstOrDefault(p => p.PersonId == id && p.Type == CurrencyType.EUR);

           var rubLabel = form.RubLebel;
           var usdLabel = form.UsdLebel;
           var eurLabel = form.EurLebel;

            if (rub != null)
            {
                rubLabel.Text = Math.Round(rub.Balance, 2).ToString();
            }
            if (usd != null)
            {
                usdLabel.Text = Math.Round(usd.Balance, 2).ToString();
            }
            if (eur != null)
            {
                eurLabel.Text = Math.Round(eur.Balance, 2).ToString();
            }
        }
    }
}
