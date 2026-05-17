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
}








