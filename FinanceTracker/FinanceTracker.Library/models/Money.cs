using FinanceTracker.Library.enums;

namespace FinanceTracker.Library.models;

public struct Money
{
    public decimal Balance { get; private set; }
    public Currency currency { get; private set; }
    public Money(decimal balance)
    {
        Balance = balance;
    }
}