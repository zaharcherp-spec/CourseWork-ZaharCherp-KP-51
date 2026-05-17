using System.Text.Json.Serialization;
using FinanceTracker.Library.Enums;

namespace FinanceTracker.Library.Models;

public struct Money
{
    public decimal Balance { get; private set; }
    public CurrencyTypes Currency { get; private set; } = CurrencyTypes.UAH;
    public decimal DailyWithdrawalLimit { get; set; } = 5000m;
    public decimal DailyTransferLimit { get; set; } = 10000m;
    public override string ToString()
    {
        return $"Баланс : {Balance} Валюта: {Currency}";
    }

    public Money(decimal balance)
    {
        Balance = balance;
    }

    [JsonConstructor]
    public Money(decimal balance, CurrencyTypes currency)
    {
        Balance = balance;
        Currency = currency;
    }

    public Money Add(decimal amount)
    {
        return new Money(Balance + amount, Currency);
    }

    public Money Subtract(decimal amount)
    {
        return new Money(Balance - amount, Currency);
    }
}


