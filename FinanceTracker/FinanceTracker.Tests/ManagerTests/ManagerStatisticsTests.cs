using FinanceTracker.Library.Services;
using FinanceTracker.Library.Models;
using FinanceTracker.Library.Data;
using FinanceTracker.Library.Settings;
using FinanceTracker.Library.Enums;

namespace FinanceTracker.Tests.ManagerTests;

public class ManagerStatisticsTests : IAsyncDisposable
{
    private readonly List<string> _tempFiles = new();

    private async Task<(Manager manager, AuthManager auth)> SetupManagerWithLoggedInUserAsync(string username, string password, decimal initialBalance = 0)
    {
        var tempFile = Path.GetTempFileName();
        _tempFiles.Add(tempFile);
        
        var repository = new JsonRepository<User>(tempFile);
        var authConstraints = new Constrains.AuthConstraints();
        var auth = new AuthManager(repository, authConstraints);
        
        await auth.InitAsync();
        await auth.RegisterAsync(username, password);
        
        var user = auth.GetUserByUsername(username);
        if (user != null && initialBalance > 0)
        {
            user.AddBalance(initialBalance);
        }

        await auth.SaveChangesAsync();
        auth.Login(username, password);

        var transferConstraints = new Constrains.TransferConstraints();
        var manager = new Manager(auth, transferConstraints);

        return (manager, auth);
    }

    public ValueTask DisposeAsync()
    {
        foreach (var file in _tempFiles)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }
        return ValueTask.CompletedTask;
    }

    // --- Отримання статистики (GetStatistics) ---

    [Fact]
    public async Task GetStatistics_ReturnsTransactionsSortedByAmount()
    {
        var (manager, auth) = await SetupManagerWithLoggedInUserAsync("user1", "password");
        
        var op1 = new FinanceOperation("user1", "user1", 150m, FinanceOperationTypes.Deposit, "Salary");
        var op2 = new FinanceOperation("user1", "user1", 50m, FinanceOperationTypes.Withdrawal, "Food");
        var op3 = new FinanceOperation("user1", "user1", 300m, FinanceOperationTypes.Deposit, "Bonus");

        auth.CurrentUser!.AddTransaction(op1);
        auth.CurrentUser!.AddTransaction(op2);
        auth.CurrentUser!.AddTransaction(op3);

        var sortedList = await manager.GetStatistics();

        Assert.Equal(3, sortedList.Count);
        Assert.Equal(50m, sortedList[0].Amount);
        Assert.Equal(150m, sortedList[1].Amount);
        Assert.Equal(300m, sortedList[2].Amount);
    }

    // --- Звітність (GetSummaryAsync) ---

    [Fact]
    public async Task GetSummaryAsync_UserNotFound_ThrowsInvalidOperationException()
    {
        var (manager, _) = await SetupManagerWithLoggedInUserAsync("user1", "password");

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await manager.GetSummaryAsync("nonExistentUser", DateTime.MinValue, DateTime.MaxValue));
    }

    [Fact]
    public async Task GetSummaryAsync_FiltersByDateAndCalculatesCorrectly()
    {
        var (manager, auth) = await SetupManagerWithLoggedInUserAsync("mainUser", "password", 1000m);
        var mainUser = auth.CurrentUser!;

        var today = DateTime.Now;
        var pastDate = today.AddDays(-10);
        var futureDate = today.AddDays(10);

        // Операції в межах потрібного діапазону (останні 5 днів - наступні 5 днів)
        // Дохід: Поповнення 200
        var opDeposit = new FinanceOperation("mainUser", "mainUser", 200m, FinanceOperationTypes.Deposit, "Salary");
        
        // Витрата: Зняття 50 (Категорія: Food)
        var opWithdraw = new FinanceOperation("mainUser", "mainUser", 50m, FinanceOperationTypes.Withdrawal, "Food");

        // Дохід: Отримання переказу 100
        var opTransferIn = new FinanceOperation("otherUser", "mainUser", 100m, FinanceOperationTypes.Transfer, "Gift");

        // Витрата: Відправка переказу 80 (Категорія: Gift)
        var opTransferOut = new FinanceOperation("mainUser", "otherUser", 80m, FinanceOperationTypes.Transfer, "Gift");

        // Операція ПОЗА діапазоном (стара)
        var opOldDeposit = new FinanceOperation("mainUser", "mainUser", 500m, FinanceOperationTypes.Deposit, "Salary");
        // Ми не можемо змінити дату операції напряму, тому використовуємо рефлексію або додамо нову операцію з хитрощами. 
        // Але простіше просто змінити поле за допомогою конструктора, хоча дата там генерується як DateTime.Now.
        // Оскільки ми не можемо легко змінити Date у FinanceOperation через відсутність setter-а, 
        // ми протестуємо базову логіку на сьогоднішніх транзакціях, а перевірку дат зробимо побічно, 
        // передаючи діапазон, який НЕ включає сьогодні.

        mainUser.AddTransaction(opDeposit);
        mainUser.AddTransaction(opWithdraw);
        mainUser.AddTransaction(opTransferIn);
        mainUser.AddTransaction(opTransferOut);

        // Перевіряємо в межах сьогоднішнього дня (повинно врахувати все)
        var summary = await manager.GetSummaryAsync("mainUser", today.AddDays(-1), today.AddDays(1));

        Assert.Equal(1000m, summary.CurrentBalance);
        
        // Доходи: 200 (Deposit) + 100 (Transfer In) = 300
        Assert.Equal(300m, summary.TotalIncome);
        
        // Витрати: 50 (Withdrawal) + 80 (Transfer Out) = 130
        Assert.Equal(130m, summary.TotalExpense);
        
        // Категорії витрат
        Assert.Equal(50m, summary.ExpensesByCategory["Food"]);
        Assert.Equal(80m, summary.ExpensesByCategory["Gift"]);

        // Тепер перевіряємо діапазон в минулому (не повинно знайти жодної операції, бо вони всі сьогоднішні)
        var emptySummary = await manager.GetSummaryAsync("mainUser", pastDate.AddDays(-2), pastDate);

        Assert.Equal(1000m, emptySummary.CurrentBalance); // Баланс завжди поточний
        Assert.Equal(0m, emptySummary.TotalIncome);
        Assert.Equal(0m, emptySummary.TotalExpense);
        Assert.Empty(emptySummary.ExpensesByCategory);
    }
}
