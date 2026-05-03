using FinanceTracker.Library.Enums;

namespace FinanceTracker.Library.Models;

public struct Money
{
    public decimal Balance { get; private set; }
    public Currency currency { get; private set; }
    public Money(decimal balance)
    {
        Balance = balance;
    }
}