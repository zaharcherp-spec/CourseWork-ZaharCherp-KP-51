using FinanceTracker.Library.Models;
using FinanceTracker.Library.Enums;

namespace FinanceTracker.Library.Services;

public partial class Manager
{
    private decimal GetUserBalance(string username)
    {
        var user = _authManager.GetUserByUsername(username);
        return user.Wallet.Balance;
    }

    private bool UserExists(string username)
    {
        var user = _authManager.GetUserByUsername(username);
        return true;
    }

    private async Task AddTransactionAsync(FinanceOperation operation)
    {
        var sender = _authManager.GetUserByUsername(operation.SenderUsername);
        var receiver = _authManager.GetUserByUsername(operation.ReceiverUsername);


        if (operation.TransactionType == FinanceOperationTypes.Deposit)
        {
            sender.AddBalance(operation.Amount);
            sender.AddTransaction(operation);
        }
        else if (operation.TransactionType == FinanceOperationTypes.Withdrawal)
        {
            sender.SubtractBalance(operation.Amount);
            sender.AddTransaction(operation);
        }
        else if (operation.TransactionType == FinanceOperationTypes.Transfer && receiver != sender)
        {
            sender.SubtractBalance(operation.Amount);
            sender.AddTransaction(operation);

            receiver.AddBalance(operation.Amount);
            receiver.AddTransaction(operation);
        }

        await _authManager.SaveChangesAsync();
    }

    public async Task DepositAsync(string username, decimal amount, string category)
    {
        if (amount <= _constraints.MinTransferAmount)
        {
            throw new ArgumentException("Сума поповнення має бути більшою за мінімальну суму для поповнення.");
        }

        var operation = new FinanceOperation(username, username, amount, FinanceOperationTypes.Deposit, category);
        await AddTransactionAsync(operation);
    }

    public async Task WithdrawAsync(string username, decimal amount, string category)
    {


        if (amount > _constraints.MaxTransferAmount)
        {
            throw new ArgumentException("Сума переказу не може перевищувати максимальну суму для переказу.");
        }

        if (amount < _constraints.MinTransferAmount)
        {
            throw new ArgumentException("Сума переказу не може бути меншою за мінімальну суму для переказу.");
        }

        decimal currentBalance = GetUserBalance(username);
        if (currentBalance < amount)
        {
            throw new InvalidOperationException("Недостатньо коштів на рахунку для зняття.");
        }
        var operation = new FinanceOperation(username, username, amount, FinanceOperationTypes.Withdrawal, category);
        await AddTransactionAsync(operation);
    }

    public async Task TransferAsync(string senderUsername, string receiverUsername, decimal amount, string category)
    {
        if (amount <= _constraints.MinTransferAmount)
        {
            throw new ArgumentException("Сума переказу має бути більшою за нуль.");
        }

        if (string.Equals(senderUsername, receiverUsername, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Неможливо здійснити переказ самому собі.");
        }

        bool receiverExists = UserExists(receiverUsername);
        if (!receiverExists)
        {
            throw new InvalidOperationException("Користувача з таким логіном не знайдено.");
        }

        decimal currentBalance = GetUserBalance(senderUsername);
        if (currentBalance < amount)
        {
            throw new InvalidOperationException("Недостатньо коштів на рахунку для переказу.");
        }

        var operation = new FinanceOperation(senderUsername, receiverUsername, amount, FinanceOperationTypes.Transfer, category);
        await AddTransactionAsync(operation);
    }
}







