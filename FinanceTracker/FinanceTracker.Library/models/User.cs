using System.Text.Json.Serialization;
namespace FinanceTracker.Library.Models;

public class User
{
    public Guid Id { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }
    public Money Wallet { get; private set; }
    public List<Transaction> Transactions { get; private set; }

    // Атрибут вказує JSON-десеріалізатору використовувати цей конструктор
    [JsonConstructor]
    public User(Guid id, string userName, string password, Money wallet, List<Transaction> transactions)
    {
        Id = id;
        UserName = userName;
        Password = password;
        Wallet = wallet;
        Transactions = transactions ?? new List<Transaction>();
    }

    // Конструктор для створення нового юзера вручну
    public User(string userName, string password)
    {
        Id = Guid.NewGuid();
        UserName = userName;
        Password = password;
        Wallet = new Money(0);
        Transactions = new List<Transaction>();
    }

    public User() { }

    public void UpdateName(string newName) => UserName = newName;
    public void ChangePassword(string newPassword) => Password = newPassword;
}













