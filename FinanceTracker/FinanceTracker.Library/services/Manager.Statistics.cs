using FinanceTracker.Library.Models;
using FinanceTracker.Library.Comparers;
using FinanceTracker.Library.Enums;


namespace FinanceTracker.Library.Services;

public partial class Manager
{
    public Task<List<FinanceOperation>> GetStatistics()
    {
        {
            var list = _authManager.CurrentUser.Transactions;
            list.Sort(new OperationValueComparer());

            return Task.FromResult(list);
        }
    }

    private decimal GetDailySpentAmount(User user, FinanceOperationTypes operationType)
    {
        decimal totalSpent = 0;
        var today = DateTime.Today;

        foreach (var operation in user.Transactions)
        {
            if (operation.FinanceOperationType == operationType && operation.Date.Date == today)
            {
                totalSpent += operation.Amount;
            }
        }
        return totalSpent;
    }

    public Task<FinancialSummary> GetSummaryAsync(string username, DateTime startDate, DateTime endDate)
    {
        var user = _authManager.GetUserByUsername(username);
        if (user == null)
        {
            throw new InvalidOperationException("Користувача не знайдено.");
        }

        var summary = new FinancialSummary
        {
            CurrentBalance = user.Wallet.Balance
        };

        foreach (var op in user.Transactions)
        {

            if (op.Date < startDate || op.Date > endDate) continue;

            bool isIncome = op.FinanceOperationType == FinanceOperationTypes.Deposit ||
                           (op.FinanceOperationType == FinanceOperationTypes.Transfer && op.ReceiverUsername == username);

            bool isExpense = op.FinanceOperationType == FinanceOperationTypes.Withdrawal ||
                            (op.FinanceOperationType == FinanceOperationTypes.Transfer && op.SenderUsername == username);

            if (isIncome)
            {
                summary.TotalIncome += op.Amount;
            }
            else if (isExpense)
            {
                summary.TotalExpense += op.Amount;

                if (!summary.ExpensesByCategory.ContainsKey(op.Category))
                {
                    summary.ExpensesByCategory[op.Category] = 0;
                }

                summary.ExpensesByCategory[op.Category] += op.Amount;
            }
        }

        return Task.FromResult(summary);
    }
}











