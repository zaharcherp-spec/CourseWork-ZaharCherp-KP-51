using FinanceTracker.Library.Enums;

namespace FinanceTracker.Library.Models;

public struct Money
{
    public decimal Balance { get; private set; }
    public CurrencyTypes currency { get; private set; }
    public Money(decimal balance)
    {
        Balance = balance;
    }

}
    