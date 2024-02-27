using FinancialApp.DataBase;
using FinancialApp.DataBase.DbModels;
using FinancialApp.Enum;
using FinancialApp.GeneralMethods;

namespace FinancialApp.Forms
{
    public partial class OperationSearch : Form
    {
        private readonly DB _db;
        private readonly Guid _id;
        private List<DbHistoryTransfer> _historyOperation;
        private readonly DbFinancial _context;

        public OperationSearch(DB db, Guid id, DbFinancial context)
        {
            InitializeComponent();
            monthCalendar1.MaxSelectionCount = 31;
            monthCalendar1.TodayDate = DateTime.Now;
            monthCalendar1.ShowToday = true;
            _db = db;
            _id = id;
            _context = context;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            var startingDateSearch = monthCalendar1.SelectionRange.Start.Date;
            var endDateSearch = monthCalendar1.SelectionRange.End.Date;
            var currencyTypeValue = currencyTypeTextBox.Text.ToUpper();
            var personRecipientName = personRecipientNameTextBox.Text;

            var operationSearch = _context.HistoryTransfers.Where(h => h.SenderId == _id || h.RecipientId == _id).ToList();

            if (operationSearch.Count == 0)
            {
                operationHistory.Text = "История операций";
                operationHistory.Text += $"\nУ Вас еще небыло операций!";
                return;
            }
            else
            {
                _historyOperation = EventSearch.GetEventSearch(_db, operationSearch, startingDateSearch, endDateSearch, currencyTypeValue, personRecipientName, _context);
            }

            if (_historyOperation == null)
            {
                operationHistory.Text = "История операций";
                operationHistory.Text += $"\nОпераций с пользователем {personRecipientName} не найдены!";
                return;
            }
            else
            {
                if (_historyOperation.Count == 0)
                {
                    operationHistory.Text = "История операций";
                    operationHistory.Text += $"\n{DateOnly.FromDateTime(startingDateSearch)} небыло произведено операций!";
                    return;
                }

                if (_historyOperation == null) return;
                operationHistory.Text = "История операций";
                PrintEvent(_historyOperation);
            }
        }

        private void PrintEvent(List<DbHistoryTransfer> historyOperation)
        {

            if (historyOperation.Count == 0)
            {
                operationHistory.Text += $"\nЕще небыло произведено операций";
                return;
            }
            else
            {
                foreach (var transferItem in historyOperation)
                {
                    var personSender = _context.Persons.First(p => p.Id == transferItem.SenderId);
                    var personRecipient = _context.Persons.FirstOrDefault(p => p.Id == transferItem.RecipientId);

                    operationHistory.Text += transferItem.OperationType switch
                    {
                        TypeOfOperation.refill => $"\n{personSender.Name} {transferItem.DateTime} произвел {TypeOperation.GetTypeOfOperation(TypeOfOperation.refill)}  {transferItem.Type} на {transferItem.MoneyTransfer}",
                        TypeOfOperation.moneyTransfer => $"\n{personSender.Name} {transferItem.DateTime} произвел {TypeOperation.GetTypeOfOperation(TypeOfOperation.moneyTransfer)}  {transferItem.MoneyTransfer} {transferItem.Type} {personRecipient.Name}",
                        TypeOfOperation.exchange => $"\n{personSender.Name} {transferItem.DateTime} произвел {TypeOperation.GetTypeOfOperation(TypeOfOperation.exchange)}  {transferItem.Type} на {transferItem.MoneyTransfer}",
                        _ => throw new ArgumentOutOfRangeException(),
                    };
                }
            }
        }

        private void HistoryOperationExcel_Click(object sender, EventArgs e)
        {
            var newExcel = new DataOutputInExcel(_db, _context);
            newExcel.GetDataOutputInExcel(_historyOperation);
        }
    }
}
