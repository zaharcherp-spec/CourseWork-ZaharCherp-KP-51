namespace BankSystem.Library;

public class User
{
    public Money Money { get; private set; }
    private string? password;
    public string? Gmail { get; private set; }
    public string UserName;
    public readonly string CardName;
    public User()
    {

    }
    public void Transaction(int money, User other)
    {
        this.Money.Balance -= money;
    }
}




