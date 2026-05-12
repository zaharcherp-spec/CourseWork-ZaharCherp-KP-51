using FinanceTracker.App.interfaces;
using FinanceTracker.Library.Services;

namespace FinanceTracker.App.CommandProviders.SubProviders;

public class FinanceCommandProvider : BaseCommandProvider
{
    public FinanceCommandProvider(
        Action<BaseCommandProvider> changer,
        AuthManager authManager,
        Menager menager
    )
        : base(changer, authManager, menager) { }

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
        Console.WriteLine("\n--- Поповнення рахунку ---");
        Console.Write("Введіть суму поповнення: ");
        
        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            bool success = await _menager.DepositAsync(amount);
            if (success)
            {
                Console.WriteLine("Успіх: Рахунок поповнено.");
            }
            else
            {
                Console.WriteLine("Помилка: Неправильна сума.");
            }
        }
        else
        {
            Console.WriteLine("Помилка: Введено некоректне число.");
        }
    }

    private async Task WithdrawFlow()
    {
        Console.WriteLine("\n--- Зняття коштів ---");
        Console.Write("Введіть суму для зняття: ");

        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            bool success = await _menager.WithdrawAsync(amount);
            if (success)
            {
                Console.WriteLine("Успіх: Кошти знято.");
            }
            else
            {
                Console.WriteLine("Помилка: Недостатньо коштів або неправильна сума.");
            }
        }
        else
        {
            Console.WriteLine("Помилка: Введено некоректне число.");
        }
    }

    private async Task TransferFlow()
    {
        Console.WriteLine("\n Переказ коштів");
        Console.Write("Введіть логін отримувача: ");
        string receiver = Console.ReadLine() ?? string.Empty;

        Console.Write("Введіть суму переказу: ");
        
        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            bool success = await _menager.TransferAsync(receiver, amount);
            if (success)
            {
                Console.WriteLine("Успіх: Кошти успішно переказано.");
            }
            else
            {
                Console.WriteLine("Помилка: Перевірте баланс, суму або правильність логіна отримувача.");
            }
        }
        else
        {
            Console.WriteLine("Помилка: Введено некоректне число.");
        }
    }

    private Task GoBack()
    {
        _changer(new UserCommandProvider(_changer, _authManager, _menager));
        
        return Task.CompletedTask;
    }
}

    
