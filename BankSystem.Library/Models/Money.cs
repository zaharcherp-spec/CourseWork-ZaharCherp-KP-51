public struct Money
{
    public decimal Balance { get; set; }
    public Currency currency { get; private set; }
    public Money(decimal balance)
    {
        Balance = balance;
    }
}