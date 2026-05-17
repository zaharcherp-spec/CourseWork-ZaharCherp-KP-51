using FinanceTracker.Library.Models;
using FinanceTracker.Library.Enums;

namespace FinanceTracker.Tests.MoneyTests;
//  Цей файл тестує створення юзерів ,
public class UserTests
{
    //1.Перевіряє створення абсолютно нового користувача "з нуля"
    [Fact]
    public void Constructor_WithUsernameAndPassword_SetsPropertiesAndInitializesCollections()
    {
        var user = new User("testUser", "password123");

        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.Equal("testUser", user.UserName);
        Assert.Equal("password123", user.Password);
        Assert.Equal(0m, user.Wallet.Balance);
        Assert.NotNull(user.Transactions);
        Assert.Empty(user.Transactions);
    }

    //2. Перевіряє створення користувача з усіма параметрами (наприклад, при читанні з JSON)
    [Fact]
    public void JsonConstructor_SetsAllProperties()
    {
        var id = Guid.NewGuid();
        var wallet = new Money(500m);
        var transactions = new List<FinanceOperation>
        {
            new FinanceOperation("sender", "receiver", 100m, FinanceOperationTypes.Transfer, "General")
        };

        var user = new User(id, "jsonUser", "pass", wallet, transactions);

        Assert.Equal(id, user.Id);
        Assert.Equal("jsonUser", user.UserName);
        Assert.Equal("pass", user.Password);
        Assert.Equal(500m, user.Wallet.Balance);
        Assert.Single(user.Transactions);
    }

    // Тестує зміну логіну користувача
    [Fact]
    public void UpdateName_ChangesUserName()
    {
        var user = new User("oldName", "pass");

        user.UpdateName("newName");

        Assert.Equal("newName", user.UserName);
    }

    //Тестує зміну пароля користувача
    [Fact]
    public void ChangePassword_ChangesPassword()
    {
        var user = new User("user", "oldPass");

        user.ChangePassword("newPass");

        Assert.Equal("newPass", user.Password);
    }


    // Перевіряє зв'язок між користувачем та його гаманцем при поповненні.
    [Fact]
    public void AddBalance_IncreasesWalletBalance()
    {
        var user = new User("user", "pass");

        user.AddBalance(200m);

        Assert.Equal(200m, user.Wallet.Balance);
    }

    //Робить теж саме ,що і минулий тест ,але з зменшенням балансу 
    [Fact]
    public void SubtractBalance_DecreasesWalletBalance()
    {
        var user = new User("user", "pass");
        user.AddBalance(500m);

        user.SubtractBalance(150m);

        Assert.Equal(350m, user.Wallet.Balance);
    }

    //Перевіряє роботу логування історії.
    [Fact]
    public void AddTransaction_AddsToTransactionsList()
    {
        var user = new User("user", "pass");
        var operation = new FinanceOperation("user", "other", 100m, FinanceOperationTypes.Transfer, "Cat");

        user.AddTransaction(operation);

        Assert.Single(user.Transactions);
        Assert.Equal(operation, user.Transactions[0]);
    }
}
