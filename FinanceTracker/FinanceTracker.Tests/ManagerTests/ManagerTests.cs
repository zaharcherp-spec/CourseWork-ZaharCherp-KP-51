using FinanceTracker.Library.Services;
using FinanceTracker.Library.Models;
using FinanceTracker.Library.Data;
using FinanceTracker.Library.Settings;
using FinanceTracker.Library.Enums;

namespace FinanceTracker.Tests;

public class ManagerFinanceTests : IAsyncDisposable
{
    private readonly List<string> _tempFiles = new();

    private async Task<(Manager manager, AuthManager auth)> SetupManagerAsync(string username, decimal initialBalance = 0)
    {
        var tempFile = Path.GetTempFileName();
        _tempFiles.Add(tempFile);

        var repository = new JsonRepository<User>(tempFile);
        var authConstraints = new Constrains.AuthConstraints();
        var auth = new AuthManager(repository, authConstraints);

        await auth.InitAsync();
        await auth.RegisterAsync(username, "password");

        var user = auth.GetUserByUsername(username);
        if (user != null && initialBalance > 0)
        {
            user.AddBalance(initialBalance);
            await auth.SaveChangesAsync();
        }

        var transferConstraints = new Constrains.TransferConstraints
        {
            MinTransferAmount = 10,
            MaxTransferAmount = 10000
        };
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

    // --- Поповнення (Deposit) ---

    [Fact]
    public async Task Deposit_ValidAmount_IncreasesBalanceAndAddsTransaction()
    {
        var (manager, auth) = await SetupManagerAsync("user1", 100m);

        await manager.DepositAsync("user1", 50m, "Salary");

        var user = auth.GetUserByUsername("user1");
        Assert.Equal(150m, user!.Wallet.Balance);
        Assert.Contains(user.Transactions, t => t.FinanceOperationType == FinanceOperationTypes.Deposit && t.Amount == 50m);
    }

    [Fact]
    public async Task Deposit_AmountBelowMinimum_ThrowsArgumentException()
    {
        var (manager, _) = await SetupManagerAsync("user1", 100m);

        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await manager.DepositAsync("user1", 5m, "Salary"));
    }

    // --- Зняття (Withdraw) ---

    [Fact]
    public async Task Withdraw_ValidAmount_DecreasesBalanceAndAddsTransaction()
    {
        var (manager, auth) = await SetupManagerAsync("user1", 200m);

        await manager.WithdrawAsync("user1", 50m, "Groceries");

        var user = auth.GetUserByUsername("user1");
        Assert.Equal(150m, user!.Wallet.Balance);
        Assert.Contains(user.Transactions, t => t.FinanceOperationType == FinanceOperationTypes.Withdrawal && t.Amount == 50m);
    }

    [Fact]
    public async Task Withdraw_AmountBelowMinimum_ThrowsArgumentException()
    {
        var (manager, _) = await SetupManagerAsync("user1", 200m);

        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await manager.WithdrawAsync("user1", 5m, "Groceries"));
    }

    [Fact]
    public async Task Withdraw_AmountAboveMaximum_ThrowsArgumentException()
    {
        var (manager, _) = await SetupManagerAsync("user1", 20000m);

        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await manager.WithdrawAsync("user1", 15000m, "Car"));
    }

    [Fact]
    public async Task Withdraw_InsufficientFunds_ThrowsInvalidOperationException()
    {
        var (manager, _) = await SetupManagerAsync("user1", 50m);

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await manager.WithdrawAsync("user1", 100m, "Groceries"));
    }

    [Fact]
    public async Task Withdraw_ExceedingDailyLimit_ThrowsArgumentException()
    {
        // DailyWithdrawalLimit is 5000 by default
        var (manager, _) = await SetupManagerAsync("user1", 20000m);

        await manager.WithdrawAsync("user1", 4000m, "Cash");

        // 4000 + 2000 = 6000 > 5000 (DailyWithdrawalLimit)
        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await manager.WithdrawAsync("user1", 2000m, "Cash"));
    }

    // --- Перекази (Transfer) ---

    [Fact]
    public async Task Transfer_ValidAmount_DecreasesSenderIncreasesReceiverBalance()
    {
        var (manager, auth) = await SetupManagerAsync("sender", 300m);
        await auth.RegisterAsync("receiver", "password");

        await manager.TransferAsync("sender", "receiver", 100m, "Gift");

        var sender = auth.GetUserByUsername("sender");
        var receiver = auth.GetUserByUsername("receiver");

        Assert.Equal(200m, sender!.Wallet.Balance);
        Assert.Equal(100m, receiver!.Wallet.Balance);

        Assert.Contains(sender.Transactions, t => t.FinanceOperationType == FinanceOperationTypes.Transfer && t.Amount == 100m);
        Assert.Contains(receiver.Transactions, t => t.FinanceOperationType == FinanceOperationTypes.Transfer && t.Amount == 100m);
    }

    [Fact]
    public async Task Transfer_ToSelf_ThrowsInvalidOperationException()
    {
        var (manager, _) = await SetupManagerAsync("user1", 100m);

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await manager.TransferAsync("user1", "user1", 50m, "Gift"));
    }

    [Fact]
    public async Task Transfer_ToNonExistentUser_ThrowsInvalidOperationException()
    {
        var (manager, _) = await SetupManagerAsync("user1", 100m);

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await manager.TransferAsync("user1", "fakeuser", 50m, "Gift"));
    }

    [Fact]
    public async Task Transfer_AmountBelowMinimum_ThrowsArgumentException()
    {
        var (manager, auth) = await SetupManagerAsync("sender", 100m);
        await auth.RegisterAsync("receiver", "password");

        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await manager.TransferAsync("sender", "receiver", 5m, "Gift"));
    }

    [Fact]
    public async Task Transfer_InsufficientFunds_ThrowsInvalidOperationException()
    {
        var (manager, auth) = await SetupManagerAsync("sender", 50m);
        await auth.RegisterAsync("receiver", "password");

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await manager.TransferAsync("sender", "receiver", 100m, "Gift"));
    }

    [Fact]
    public async Task Transfer_ExceedingDailyLimit_ThrowsArgumentException()
    {
        // DailyTransferLimit is 10000 by default
        var (manager, auth) = await SetupManagerAsync("sender", 15000m);
        await auth.RegisterAsync("receiver", "password");

        await manager.TransferAsync("sender", "receiver", 8000m, "Gift");

        // 8000 + 3000 = 11000 > 10000 (DailyTransferLimit)
        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await manager.TransferAsync("sender", "receiver", 3000m, "Gift"));
    }
}
