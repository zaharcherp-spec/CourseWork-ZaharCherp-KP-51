using FinanceTracker.App.interfaces;
using FinanceTracker.Library.Services;

namespace FinanceTracker.App.CommandProviders;

public class MenuCommandProvider : BaseCommandProvider
{
    public MenuCommandProvider(
        Action<BaseCommandProvider> changer,
        AuthManager authManager,
        Menager menager
    )
        : base(changer, authManager, menager) { }

    public override Dictionary<string, (string Name, Func<Task> Action)> GetCommands()
    {
        return new Dictionary<string, (string Name, Func<Task> Action)>
        {
            { "1", ("Увійти", LoginFlow) },
            { "2", ("Зареєструватись", RegisterFlow) },
            {
                "0",
                (
                    "Вийти з застосунку",
                    () =>
                    {
                        Environment.Exit(0);
                        return Task.CompletedTask;
                    }
                )
            },
        };
    }

    private Task LoginFlow()
    {
        Console.Write("Введіть логін: ");
        string username = Console.ReadLine() ?? string.Empty;

        Console.Write("Введіть пароль: ");
        string password = Console.ReadLine() ?? string.Empty;

        if (_authManager.Login(username, password))
        {
            Console.WriteLine($"Успішний вхід! Вітаємо, {_authManager.CurrentUser?.UserName}.");
            _changer(new UserCommandProvider(_changer, _authManager, _menager));
        }
        else
        {
            Console.WriteLine("Помилка: Неправильний логін або пароль.");
        }

        return Task.CompletedTask;
    }

    private async Task RegisterFlow()
    {
        Console.Write("Придумайте логін: ");
        string username = Console.ReadLine() ?? string.Empty;

        Console.Write("Придумайте пароль: ");
        string password = Console.ReadLine() ?? string.Empty;

        bool success = await _authManager.RegisterAsync(username, password);

        if (success)
            Console.WriteLine("Реєстрація успішна! Тепер ви можете увійти.");
        else
            Console.WriteLine("Помилка: спробуйте ввести інший пароль або логін.");
    }
}
