using FinanceTracker.App.interfaces;
using FinanceTracker.Library.Services;

namespace FinanceTracker.App.CommandProviders.SubProviders;

public class ProfileCommandProvider : BaseCommandProvider
{
    public ProfileCommandProvider(
        Action<BaseCommandProvider> action,
        AuthManager authManager,
        Menager menager
    )
        : base(action, authManager, menager) { }

    public override Dictionary<string, (string Name, Func<Task> Action)> GetCommands()
    {
        return new Dictionary<string, (string Name, Func<Task> Action)>
        {
            { "1", ("Переглянути інформацію про профіль", ShowProfileInfo) },
            { "2", ("Змінити пароль", ChangePasswordFlow) },
            { "3", ("Змінити ім'я користувача (логін)", ChangeUsernameFlow) },
            { "0", ("Назад до головного меню", GoBack) },
        };
    }

    private Task ShowProfileInfo()
    {
        Console.WriteLine("\n Інформація про ваш профіль ");
        
        
        return Task.CompletedTask;
    }

    private async Task ChangePasswordFlow()
    {
        Console.WriteLine("\n Зміна пароля:");
        Console.Write("Введіть ваш старий пароль: ");
        string oldPassword = Console.ReadLine() ?? "";

        Console.Write("Введіть новий пароль: ");
        string newPassword = Console.ReadLine() ?? "";

        bool success = await _menager.ChangePasswordAsync(oldPassword, newPassword);

        if (success)
        {
            Console.WriteLine("Успіх: Ваш пароль було успішно змінено!");
        }
        else
        {
            Console.WriteLine(
                "Помилка: Неправильний старий пароль або новий пароль не відповідає вимогам безпеки."
            );
        }
    }

    private async Task ChangeUsernameFlow()
    {
        Console.WriteLine("\n Зміна логіна");
        Console.Write("Введіть новий логін: ");
        string newUsername = Console.ReadLine() ?? "";

        bool success = await _menager.ChangeUsernameAsync(newUsername);

        if (success)
        {
            Console.WriteLine($"Успіх: Ваш логін змінено на '{newUsername}'.");
        }
        else
        {
            Console.WriteLine(
                "Помилка: Такий логін вже зайнятий іншим користувачем або містить недопустимі символи."
            );
        }
    }

    private Task GoBack()
    {
        _changer(new UserCommandProvider(_changer, _authManager, _menager));

        return Task.CompletedTask;
    }
}
