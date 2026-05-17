using FinanceTracker.Library.Models;

namespace FinanceTracker.Library.Comparers;

public class OperationValueComparer : IComparer<FinanceOperation>
{
    public int Compare(FinanceOperation? x, FinanceOperation? y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        return x.Amount.CompareTo(y.Amount);
    }
}

