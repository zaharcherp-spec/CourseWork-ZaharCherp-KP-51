using FinanceTracker.Library.Models;
using FinanceTracker.Library.Enums;

namespace FinanceTracker.Tests.ModelsTests;

// Цей файл тестує створення фінансових операцій та їх параметри
public class FinanceOperationTests
{
    // Перевіряє, чи конструктор правильно ініціалізує всі властивості фінансової операції
    [Fact]
    public void Constructor_WithSenderReceiver_SetsPropertiesCorrectly()
    {
        string sender = "alice";
        string receiver = "bob";
        decimal amount = 250m;
        var type = FinanceOperationTypes.Transfer;
        string category = "Gift";

        var operation = new FinanceOperation(sender, receiver, amount, type, category);

        Assert.False(string.IsNullOrEmpty(operation.Id));
        Assert.Equal(sender, operation.SenderUsername);
        Assert.Equal(receiver, operation.ReceiverUsername);
        Assert.Equal(amount, operation.Amount);
        Assert.Equal(type, operation.FinanceOperationType);
        Assert.Equal(category, operation.Category);
        Assert.True((DateTime.Now - operation.Date).TotalSeconds < 2);
    }

    // Перевіряє, що якщо категорія не вказана (передано null), то встановлюється порожній рядок замість null
    [Fact]
    public void Constructor_WithNullCategory_SetsEmptyCategory()
    {
        var operation = new FinanceOperation("s", "r", 10, FinanceOperationTypes.Deposit, null);

        Assert.Equal(string.Empty, operation.Category);
    }
}
