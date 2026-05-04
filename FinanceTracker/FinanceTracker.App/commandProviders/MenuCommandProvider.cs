using FinanceTracker.App.interfaces;
using FinanceTracker.Library.Services;

namespace FinanceTracker.App.CommandProviders;

public class MenuCommandProvider : ICommandProvider
{
    private readonly Action<ICommandProvider> _changer;
    private readonly AuthManager _authManager;

    public MenuCommandProvider(Action<ICommandProvider> changer, AuthManager authManager)
    {
        _changer = changer;
        _authManager = authManager;
    }

    public Dictionary<string, (string, Action)> GetCommands()
    {
        var dictionary = new Dictionary<string, (string, Action)>
        {
            { "1", ("Увійти", LoginFlow) },
            { "2", ("Зареєструватись", RegisterFlow) },
            { "0", ("Вийти з застосунку", () => Environment.Exit(0)) }
        };
        return dictionary;
    }

    private void LoginFlow()
    {
        Console.Write("Введіть логін: ");
        string username = Console.ReadLine() ?? " ";

        Console.Write("Введіть пароль: ");
        string password = Console.ReadLine() ?? " ";

        if (_authManager.Login(username, password))
        {
            Console.WriteLine($"Успішний вхід! Вітаємо, {_authManager.CurrentUser?.UserName}.");
            
            _changer(new UserCommandProvider(_changer, _authManager));
        }
        else
        {
            Console.WriteLine("Помилка: Неправильний логін або пароль.");
        }
    }

    private void RegisterFlow()
    {
        Console.Write("Придумайте логін: ");
        string username = Console.ReadLine() ?? " ";

        Console.Write("Придумайте пароль: ");
        string password = Console.ReadLine() ?? " ";

        bool success = _authManager.RegisterAsync(username, password).Result;

        if (success)
        {
            Console.WriteLine("Реєстрація успішна! Тепер ви можете увійти.");
        }

        else
            Console.WriteLine("Помилка спробуйте ввести інший пароль , або логін");
    }
}
























