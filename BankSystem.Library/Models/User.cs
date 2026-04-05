namespace BankSystem.Library;

public class User
{
    private Money Money { get; set; }
    private string? Password { get; set; }
    private string? Gmail { get; set; }
    private string UserName { get; set; }
    private readonly string CardName;
    public User()
    {


    }
    public void Transaction(int money, User other)
    {
        this.Money = new Money(Money.Balance - money);
        other.Money = new Money(other.Money.Balance + money);
    }
    public string ShowCurrent() => $"Balance {Money.Balance} --{Money.currency}";
    public void ChangeName(string newUsername) => UserName = newUsername;
    public void ChangePassword(string NewPassword) => Password = NewPassword;
    public void AddMoney(string sum) => this.Money = new Money(int.Parse(sum) + Money.Balance);
}














