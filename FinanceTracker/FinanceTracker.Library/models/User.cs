namespace BankSystem.Library;

public class User
{
    private Money Money { get; set; }
    private string Password { get; set; }
    public List<Transaction> Transactions { get; set; }
    private string UserName { get; set; }
    public Guid Id { get; }

    public User()
    {

    }
    public string ShowCurrentBalance() => $"Balance : {Money.Balance} --{Money.currency}";
    public void ChangeName(string newUsername) => UserName = newUsername;
    public void ChangePassword(string NewPassword) => Password = NewPassword;
}

















