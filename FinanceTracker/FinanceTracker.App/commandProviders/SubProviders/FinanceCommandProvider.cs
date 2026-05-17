using FinanceTracker.Library.Services;

namespace FinanceTracker.App.CommandProviders.SubProviders;

public class FinanceCommandProvider : BaseCommandProvider
{
    public FinanceCommandProvider(
        Action<BaseCommandProvider> changer,
        AuthManager authManager,
        Manager manager
    )
        : base(changer, authManager, manager) { }

    public override Dictionary<string, (string Name, Func<Task> Action)> GetCommands()
    {
        return new Dictionary<string, (string Name, Func<Task> Action)>
        {
            { "1", ("Поповнити рахунок", DepositFlow) },
            { "2", ("Зняти кошти", WithdrawFlow) },
            { "3", ("Переказати кошти", TransferFlow) },
            { "0", ("Назад до головного меню", GoBack) }
        };
    }

    private async Task DepositFlow()
    {
        decimal amount = ReadAmount();
        string category = ReadCategory();

        await _manager.DepositAsync(_authManager.CurrentUser.UserName, amount, category);
        Console.WriteLine("\nРахунок успішно поповнено.");
    }

    private async Task WithdrawFlow()
    {
        decimal amount = ReadAmount();
        string category = ReadCategory();

        await _manager.WithdrawAsync(_authManager.CurrentUser.UserName, amount, category);
        Console.WriteLine("\nКошти успішно знято.");

    }

    private async Task TransferFlow()
    {
        Console.Write("Введіть логін отримувача: ");
        string receiverUsername = Console.ReadLine() ?? string.Empty;

        decimal amount = ReadAmount();
        string category = ReadCategory();

        await _manager.TransferAsync(_authManager.CurrentUser.UserName, receiverUsername, amount, category);
        Console.WriteLine("\nПереказ успішно виконано.");

    }

    private decimal ReadAmount()
    {
        Console.Write("Введіть суму: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
        {
            return amount;
        }
        throw new Exception("Невалідне число для переказу");
    }

    private string ReadCategory()
    {
        Console.Write("Введіть категорію (або натисніть Enter для 'Інше'): ");
        string category = Console.ReadLine() ?? string.Empty;

        return category ?? "Інше";
    }

    private Task GoBack()
    {
        _changer(new UserCommandProvider(_changer, _authManager, _manager));
        return Task.CompletedTask;
    }
}















