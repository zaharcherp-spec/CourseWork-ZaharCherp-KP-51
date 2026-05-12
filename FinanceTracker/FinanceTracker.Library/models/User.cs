using System.Text.Json.Serialization;

namespace FinanceTracker.Library.Models;

public class User
{
    public Guid Id { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }
    public Money Wallet { get; private set; }
    public List<FinanceOperation> Transactions { get; private set; }

    [JsonConstructor]
    public User(
        Guid id,
        string userName,
        string password,
        Money wallet,
        List<FinanceOperation> transactions
    )
    {
        Id = id;
        UserName = userName;
        Password = password;
        Wallet = wallet;
        Transactions = transactions ?? new List<FinanceOperation>();
    }

    public User(string userName, string password)
    {
        Id = Guid.NewGuid();
        UserName = userName;
        Password = password;
        Wallet = new Money(0);
        Transactions = new List<FinanceOperation>();
    }

    public User() { }

    public override string ToString()
    {
        return $"Name : {UserName}  Wallet :({Wallet})";
    }

    public void UpdateName(string newName) => UserName = newName;

    public void ChangePassword(string newPassword) => Password = newPassword;

    public void AddBalance(decimal amount)
    {
        Wallet = Wallet.Add(amount);
    }

    public void SubtractBalance(decimal amount)
    {
        Wallet = Wallet.Subtract(amount);
    }

    public void AddTransaction(FinanceOperation operation)
    {
        Transactions.Add(operation);
    }
}
